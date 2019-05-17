using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class AuthorDetailViewModel : ItemDetailViewModel<AuthorViewModel>
    {
        public AuthorDetailViewModel(ViewModelLocator locator) : base(locator)
        {
            Name = nameof(AuthorDetailViewModel);
            DisplayName = "Yazar Detayı";
        }
    }
}
