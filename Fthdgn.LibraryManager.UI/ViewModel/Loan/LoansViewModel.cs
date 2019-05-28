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
    public class LoansViewModel : DetailedItemsViewModel<Loan, LoanViewModel, LoanDetailViewModel>
    {
        public LoansViewModel(ViewModelLocator locator, LoanDetailViewModel detailViewModel, LibraryManagerManagers managers) : base(locator, detailViewModel, managers)
        {
            Name = nameof(LoansViewModel);
            DisplayName = "Ödünç Listesi";
            CanCreate = false;
            CanSearch = true;
            CanSelect = false;
            AutoSelect = false;

            Managers = managers;
            SelectUserCommand = new RelayCommand(async () => await SelectUserAsync(), CanSelectUser, true);
            ClearUserCommand = new RelayCommand(ClearUser, CanClearUser);
            SelectBookCommand = new RelayCommand(async () => await SelectBookAsync(), CanSelectBook, true);
            ClearBookCommand = new RelayCommand(ClearBook, CanClearBook);
        }

        protected override IEnumerable<Loan> ProvideItems() => 
            Managers.Loans.ByUser(Locator.Main.User)
                .As(x => User != null ? x.Where(q => q.User.Id == User.Id) : x)
                .As(x => Book != null ? x.Where(q => q.Book.Id == Book.Id) : x)
                .OrderByDescending(x => x.ReturnsAt);

        User user;
        public User User { get => user; set { Set(ref user, value); OnNavigating(); RaisePropertyChanged(nameof(UserFilterText)); RaisePropertyChanged(nameof(IsUserFiltered)); ClearUserCommand.RaiseCanExecuteChanged(); } }
        public string UserFilterText => User != null ? User.ToString() : "Kullanıcı Filtresi";
        public bool IsUserFiltered => User != null;

        RelayCommand selectUserCommand;
        public RelayCommand SelectUserCommand { get => selectUserCommand; set => Set(ref selectUserCommand, value); }
        
        public bool CanSelectUser() => Locator.Main.Scopes.User_Read;
        public async Task SelectUserAsync()
        {
            Locator.Users.CanSelect = true;
            Locator.Main.GoTo(Locator.Users);
            User = (await Locator.Users.SelectItemDialogAsync())?.Value;
            Locator.Users.CanSelect = false;
            Locator.Main.GoBack();
        }

        RelayCommand clearUserCommand;
        public RelayCommand ClearUserCommand { get => clearUserCommand; set => Set(ref clearUserCommand, value); }

        public bool CanClearUser() => IsUserFiltered;
        public void ClearUser() => User = null;

        Book book;
        public Book Book { get => book; set { Set(ref book, value); OnNavigating(); RaisePropertyChanged(nameof(BookFilterText)); RaisePropertyChanged(nameof(IsBookFiltered)); ClearBookCommand.RaiseCanExecuteChanged(); } }
        public string BookFilterText => IsBookFiltered ? Book.ToString() : "Kitap Filtresi";
        public bool IsBookFiltered => Book != null;

        RelayCommand selectBookCommand;
        public RelayCommand SelectBookCommand { get => selectBookCommand; set => Set(ref selectBookCommand, value); }

        public bool CanSelectBook() => Locator.Main.Scopes.Book_Read;
        public async Task SelectBookAsync()
        {
            Locator.Books.CanSelect = true;
            Locator.Main.GoTo(Locator.Books);
            Book = (await Locator.Books.SelectItemDialogAsync())?.Value;
            Locator.Books.CanSelect = false;
            Locator.Main.GoBack();
        }

        RelayCommand clearBookCommand;
        public RelayCommand ClearBookCommand { get => clearBookCommand; set => Set(ref clearBookCommand, value); }

        public bool CanClearBook() => IsBookFiltered;
        public void ClearBook() => Book = null;

        protected override Options<Loan> ProvideOptions(Loan item) => Locator.Main.Scopes.As(s => new Options<Loan>(item, true, item.ReturnedAt == null, false, CanSelect));
        protected override bool FilterItem(string search, Loan item) => search.ToLowerInvariant().As(sl =>
            item.Book.Name.ToLowerInvariant().Contains(sl) ||
            item.Book.LCC.ToLowerInvariant().Contains(sl) ||
            item.Book.LCCN.ToLowerInvariant().Contains(sl) ||
            item.User.Name.ToLowerInvariant().Contains(sl) ||
            item.User.Surname.ToLowerInvariant().Contains(sl) ||
            item.Library.Name.ToLowerInvariant().Contains(sl)
        );

        protected override async Task OnItemEditAsync(Options<Loan> item)
        {
            var vm = Locator.LoanReturnDetail;
            vm.Mode = DetailMode.Delete;
            Locator.Main.GoTo(vm);
            var result = await vm.DeleteItemAsync(item.MapTo<Loan, LoanViewModel>());
            if (result?.IsChanged ?? false)
            {
                var loan = Managers.Repositories.Loans.Get(result.Value.Id);
                loan.ReturnedAt = DateTimeOffset.Now;
                Managers.Repositories.Loans.Update(loan);
                Managers.Save();
            }
            Locator.Main.GoBack();
            vm.Mode = DetailMode.View;
        }

        public override void OnNavigating()
        {
            if (Locator.Main.Library != null && Locator.Main.User != null && Locator.Main.Scopes != null)
            {
                FetchItems();
            }
        }
    }
}
