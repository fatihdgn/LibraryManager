using AutoMapper;
using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Extensions;
using Fthdgn.LibraryManager.Managers;
using Fthdgn.LibraryManager.Repositories;
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
        where T : Entity, new()
        where TItemViewModel : EntityViewModel, new()
        where TItemDetailViewModel : ItemDetailViewModel<TItemViewModel>
    {

        LibraryManagerManagers managers;
        public LibraryManagerManagers Managers { get => managers; set => Set(ref managers, value); }

        LibraryManagerRepository<T> repository;
        public LibraryManagerRepository<T> Repository { get => repository; set => Set(ref repository, value); }
       
        TItemDetailViewModel detailViewModel;
        public TItemDetailViewModel DetailViewModel
        {
            get => detailViewModel;
            set => Set(ref detailViewModel, value);
        }

        public DetailedItemsViewModel(ViewModelLocator locator, TItemDetailViewModel detailViewModel, LibraryManagerManagers managers) : base(locator)
        {
            DetailViewModel = detailViewModel;
            Managers = managers;
            Repository = Managers.Repositories.Repository<T>();
        }

        protected virtual Options<TItemViewModel> Map(Options<T> options) => options?.MapTo<T, TItemViewModel>();
        protected virtual Options<T> Map(Options<TItemViewModel> options) => options?.MapTo<TItemViewModel, T>();
        protected virtual TItemViewModel Map(T item, TItemViewModel model = null) => model == null ? Mapper.Map<T, TItemViewModel>(item) : Mapper.Map(item, model);
        protected virtual T Map(TItemViewModel item, T model = null) => model == null ? Mapper.Map<TItemViewModel, T>(item) : Mapper.Map(item, model);

        
        protected override async Task OnItemViewAsync(Options<T> item)
        {
            await base.OnItemViewAsync(item);
            Locator.Main.GoTo(DetailViewModel);
            var result = await DetailViewModel.ViewItemAsync(item.MapTo<T, TItemViewModel>());
        }

        protected override async Task OnItemCreateAsync()
        {
            await base.OnItemCreateAsync();
            Locator.Main.GoTo(DetailViewModel);
            var result = await DetailViewModel.CreateItemAsync();
            if (result?.IsChanged ?? false)
            {
                var map = Map(result.Value, new T());
                Repository.Add(map);
                Repository.Save();
                FetchItems();
            }
        }

        protected override async Task OnItemEditAsync(Options<T> item)
        {
            await base.OnItemEditAsync(item);
            Locator.Main.GoTo(DetailViewModel);
            var result = await DetailViewModel.EditItemAsync(Map(item));
            if (result?.IsChanged ?? false)
            {
                Repository.Update(Map(result.Value, item.Value));
                Repository.Save();
                FetchItems();
            }
        }

        protected override async Task OnItemDeleteAsync(Options<T> item)
        {
            await base.OnItemDeleteAsync(item);
            Locator.Main.GoTo(DetailViewModel);
            var result = await DetailViewModel.DeleteItemAsync(Map(item));
            if (result?.IsChanged ?? false)
            {
                Repository.Remove(item.Value);
                Repository.Save();
                FetchItems();
            }
        }
    }

    public class BooksViewModel : DetailedItemsViewModel<Book, BookViewModel, BookDetailViewModel>
    {
        public BooksViewModel(ViewModelLocator locator, BookDetailViewModel detailViewModel, LibraryManagerManagers managers) : base(locator, detailViewModel, managers)
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

        protected override Book Map(BookViewModel item, Book model = null)
        {
            var map = base.Map(item, model);
            if (map.Library == null) map.Library = Locator.Main.Library;
            return map;
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
