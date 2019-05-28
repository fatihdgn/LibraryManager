using AutoMapper;
using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Extensions;
using Fthdgn.LibraryManager.Managers;
using Fthdgn.LibraryManager.Repositories;
using Fthdgn.LibraryManager.UI.Extensions;
using Fthdgn.LibraryManager.UI.Models;
using Fthdgn.LibraryManager.UI.Pages;
using GalaSoft.MvvmLight.Command;
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
            ViewLoansCommand = new RelayCommand<Options<Book>>(ViewLoans, CanViewLoans);
            SelectAuthorCommand = new RelayCommand(async () => await SelectAuthorAsync(), CanSelectAuthor, true);
            ClearAuthorCommand = new RelayCommand(ClearAuthor, CanClearAuthor);
        }

        RelayCommand<Options<Book>> viewLoansCommand;
        public RelayCommand<Options<Book>> ViewLoansCommand { get => viewLoansCommand; set => Set(ref viewLoansCommand, value); }

        public bool CanViewLoans(Options<Book> item) => !CanSelect && Locator.Main.Scopes.Loan_Create_OnBehalf;
        public void ViewLoans(Options<Book> item)
        {
            Locator.Loans.Book = item.Value;
            Locator.Main.GoTo(Locator.Loans);
        }

        Author author;
        public Author Author { get => author; set { Set(ref author, value); OnNavigating(); RaisePropertyChanged(nameof(AuthorFilterText)); RaisePropertyChanged(nameof(IsAuthorFiltered)); ClearAuthorCommand.RaiseCanExecuteChanged(); } }
        public string AuthorFilterText => Author != null ? Author.ToString() : "Yazar Filtresi";
        public bool IsAuthorFiltered => Author != null;

        RelayCommand selectAuthorCommand;
        public RelayCommand SelectAuthorCommand { get => selectAuthorCommand; set => Set(ref selectAuthorCommand, value); }

        public bool CanSelectAuthor() => Locator.Main.Scopes.Author_Read;
        public async Task SelectAuthorAsync()
        {
            Locator.Authors.CanSelect = true;
            Locator.Main.GoTo(Locator.Authors);
            Author = (await Locator.Authors.SelectItemDialogAsync())?.Value;
            Locator.Authors.CanSelect = false;
            Locator.Main.GoBack();
        }

        RelayCommand clearAuthorCommand;
        public RelayCommand ClearAuthorCommand { get => clearAuthorCommand; set => Set(ref clearAuthorCommand, value); }

        public bool CanClearAuthor() => IsAuthorFiltered;
        public void ClearAuthor() => Author = null;

        protected override IEnumerable<Book> ProvideItems() => Managers.Repositories.Books.Query().As(q => IsAuthorFiltered ? q.Where(x => x.Author.Id == Author.Id) : q).Where(x => x.Library.Id == Locator.Main.Library.Id).OrderBy(x => x.Name);
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
            if (Locator.Main.Library != null && Locator.Main.Scopes != null)
            {
                CanCreate = Locator.Main.Scopes.Book_All;
                FetchItems();
            }
        }
    }
}
