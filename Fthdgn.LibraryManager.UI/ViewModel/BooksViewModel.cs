using AutoMapper;
using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Extensions;
using Fthdgn.LibraryManager.Managers;
using Fthdgn.LibraryManager.UI.Extensions;
using Fthdgn.LibraryManager.UI.Models;
using Fthdgn.LibraryManager.UI.Pages;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public abstract class DetailedItemsViewModel<T, TItemViewModel, TItemDetailViewModel> : ItemsViewModel<T>
        where T : class, new()
        where TItemViewModel : EntityViewModel, new()
        where TItemDetailViewModel : ItemDetailViewModel<TItemViewModel>
    {
        TItemDetailViewModel detailViewModel;
        public TItemDetailViewModel DetailViewModel {
            get => detailViewModel;
            set => Set(ref detailViewModel, value);
        }

        public DetailedItemsViewModel(ViewModelLocator locator, TItemDetailViewModel detailViewModel) : base(locator)
        {
            DetailViewModel = detailViewModel;
        }


        protected virtual async Task OnItemViewing(Options<T> item)
        {
            await Task.Yield();
        }
        protected virtual async Task OnItemViewed(Options<T> item)
        {
            await Task.Yield();
        }
        protected override async Task OnItemViewAsync(Options<T> item)
        {
            await base.OnItemViewAsync(item);
            Locator.Main.GoTo(DetailViewModel);
            await OnItemViewing(item);
            var result = await DetailViewModel.ViewItemAsync(item.MapTo<T, TItemViewModel>());
            await OnItemViewed(item);
        }
    }

    public class BooksViewModel : ItemsViewModel<Book>
    {
        public LibraryManagerManagers Managers { get; set; }

        public BooksViewModel(ViewModelLocator locator, LibraryManagerManagers managers) : base(locator)
        {
            Name = nameof(BooksViewModel);
            DisplayName = "Kitaplar";
            CreateText = "Yeni Kitap";
            CanSearch = true;
            CanSelect = false;
            AutoSelect = false;

            Managers = managers;
            Messenger.Default.Register<PropertyChangedMessage<Library>>(this, pcm => OnNavigating());
        }

        protected override IEnumerable<Book> ProvideItems() => Managers.Repositories.Books.Query().Where(x => x.Library.Id == Locator.Main.Library.Id).OrderBy(x => x.Name);
        protected override Options<Book> ProvideOptions(Book item) => Locator.Main.Scopes.As(s => new Options<Book>(item, s.Book_Read, s.Book_All, s.Book_All, CanSelect));
        protected override bool FilterItem(string search, Book item) => item.Name.ToLowerInvariant().Contains(search.ToLowerInvariant());

        protected override async Task OnItemViewAsync(Options<Book> item)
        {
            await base.OnItemViewAsync(item);
            Locator.Main.GoTo<BookDetail>();
            await Locator.BookDetail.ViewItemAsync(item.MapTo<Book, BookViewModel>());
        }

        protected override async Task OnItemCreateAsync()
        {
            await base.OnItemCreateAsync();
            Locator.Main.GoTo<BookDetail>();
            var result = await Locator.BookDetail.CreateItemAsync();
            if (result?.IsChanged ?? false)
            {
                var map = Mapper.Map(result.Value, new Book());
                map.Library = Locator.Main.Library;
                Managers.Repositories.Books.Add(map);
                Managers.Save();
                FetchItems();
            }
        }

        protected override async Task OnItemEditAsync(Options<Book> item)
        {
            await base.OnItemEditAsync(item);
            Locator.Main.GoTo<BookDetail>();
            var result = await Locator.BookDetail.EditItemAsync(item.MapTo<Book, BookViewModel>());
            if (result?.IsChanged ?? false)
            {
                Managers.Repositories.Books.Update(Mapper.Map(result.Value, item.Value));
                Managers.Save();
                FetchItems();
            }
        }

        protected override async Task OnItemDeleteAsync(Options<Book> item)
        {
            await base.OnItemDeleteAsync(item);
            Locator.Main.GoTo<BookDetail>();
            var result = await Locator.BookDetail.DeleteItemAsync(item.MapTo<Book, BookViewModel>());
            if (result?.IsChanged ?? false)
            {
                Managers.Repositories.Books.Remove(item.Value);
                Managers.Save();
                FetchItems();
            }
        }

        public override void OnNavigating()
        {
            if (Locator.Main.Library != null)
            {
                CanCreate = Locator.Main.Scopes.Book_All;
                FetchItems();
            }
        }
    }
}
