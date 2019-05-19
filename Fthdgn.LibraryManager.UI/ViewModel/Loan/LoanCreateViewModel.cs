using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Managers;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class LoanCreateViewModel : ItemDetailViewModel<LoanViewModel>
    {
        public LibraryManagerManagers Managers { get; set; }
        public LoanCreateViewModel(ViewModelLocator locator, LibraryManagerManagers managers) : base(locator)
        {
            Name = nameof(LoanCreateViewModel);
            DisplayName = "Kitap Ödünç Al";
            Managers = managers;
            SelectBookCommand = new RelayCommand(async () => await SelectBookAsync(), CanSelectBook, true);
        }

        RelayCommand selectBookCommand;
        public RelayCommand SelectBookCommand { get => selectBookCommand; set => Set(ref selectBookCommand, value); }

        public bool CanSelectBook() => Locator.Main.Scopes.Book_Read;
        public async Task SelectBookAsync()
        {
            Mode = DetailMode.Select;
            Locator.Books.CanSelect = true;
            Locator.Main.GoTo(Locator.Books);
            var book = await Locator.Books.SelectItemDialogAsync();
            Locator.Books.CanSelect = false;
            Locator.Main.GoBack();
            Mode = DetailMode.Create;

            if (book != null)
                Item.Value.Book = book.Value;
        }

        protected async override Task YesAsync()
        {
            await base.YesAsync();
            if (Decision == Decision.Yes)
            {
                Managers.Repositories.Loans.Add(new Loan
                {
                    Library = Locator.Main.Library,
                    User = Locator.Main.User,
                    Book = Item.Value.Book,
                    LoanedAt = DateTimeOffset.Now,
                    ReturnsAt = Item.Value.ReturnsAt
                });
                Managers.Save();
            }
        }

        public override void OnNavigating()
        {
            if (Mode != DetailMode.Select)
                Item = new Models.Options<LoanViewModel>(new LoanViewModel());
        }
    }
}
