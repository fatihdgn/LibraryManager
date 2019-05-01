using Fthdgn.LibraryManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class BookDetailViewModel : ItemDetailViewModel<Book>
    {
        public BookDetailViewModel(ViewModelLocator locator) : base(locator)
        {
        }
    }
}
