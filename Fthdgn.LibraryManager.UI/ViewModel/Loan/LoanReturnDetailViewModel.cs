using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class LoanReturnDetailViewModel : ItemDetailViewModel<LoanViewModel>
    {
        public LoanReturnDetailViewModel(ViewModelLocator locator) : base(locator)
        {
            Name = nameof(LoanReturnDetailViewModel);
            DisplayName = "İade Detayı";
        }
    }
}
