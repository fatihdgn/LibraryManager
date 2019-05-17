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
            if (item.Author?.Id != null)
                map.Author = Managers.Repositories.Authors.Get(item.Author.Id);
            else
                map.Author = null;
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
