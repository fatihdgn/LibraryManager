using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Extensions;
using Fthdgn.LibraryManager.Managers;
using Fthdgn.LibraryManager.UI.Extensions;
using Fthdgn.LibraryManager.UI.Models;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{

    public class LoanReturnOnBehalfViewModel : DetailedItemsViewModel<Loan, LoanViewModel, LoanReturnDetailViewModel>
    {
        public LoanReturnOnBehalfViewModel(ViewModelLocator locator, LoanReturnDetailViewModel detailViewModel, LibraryManagerManagers managers) : base(locator, detailViewModel, managers)
        {
            Name = nameof(LoanReturnOnBehalfViewModel);
            DisplayName = "Kitap İade Al";
            CreateText = "";
            CanCreate = false;
            CanSearch = true;
            CanSelect = true;
            AutoSelect = false;

            Managers = managers;
        }

        protected override IEnumerable<Loan> ProvideItems() => Managers.Repositories.Loans.Query().Where(x => x.Library.Id == Locator.Main.Library.Id && x.ReturnedAt == null).OrderBy(x => x.LoanedAt);
        protected override Options<Loan> ProvideOptions(Loan item) => Locator.Main.Scopes.As(s => new Options<Loan>(item, s.Loan_Read, s.Loan_All, s.Loan_All, CanSelect));
        protected override bool FilterItem(string search, Loan item) => item.Book.Name.ToLowerInvariant().Contains(search.ToLowerInvariant()) || item.User.Name.ToLowerInvariant().Contains(search.ToLowerInvariant()) || item.User.Surname.ToLowerInvariant().Contains(search.ToLowerInvariant());

        protected override async Task OnItemSelectAsync(Options<Loan> item)
        {
            DetailViewModel.Mode = DetailMode.Delete;
            Locator.Main.GoTo(DetailViewModel);
            var result = await DetailViewModel.DeleteItemAsync(item.MapTo<Loan, LoanViewModel>());
            if (result?.IsChanged ?? false)
            {
                var loan = Managers.Repositories.Loans.Get(result.Value.Id);
                loan.ReturnedAt = DateTimeOffset.Now;
                Managers.Repositories.Loans.Update(loan);
                Managers.Save();
            }
            Locator.Main.GoBack();
            DetailViewModel.Mode = DetailMode.View;
        }

        public override void OnNavigating()
        {
            if (Locator.Main.Library != null)
            {
                FetchItems();
            }
        }
    }
}
