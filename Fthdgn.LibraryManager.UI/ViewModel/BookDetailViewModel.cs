using Fthdgn.LibraryManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class BookDetailViewModel : ItemDetailViewModel<BookViewModel>
    {
        public BookDetailViewModel(ViewModelLocator locator) : base(locator)
        {
            Name = nameof(BookDetailViewModel);
            DisplayName = "Kitap Detayı";
        }
    }
}
