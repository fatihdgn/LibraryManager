using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class LibraryDetailViewModel : ViewModel
    {
        public DetailMode Mode { get; set; } = DetailMode.View;
        public LibraryDetailViewModel(ViewModelLocator locator) : base(locator)
        {

        }

    }

    public enum DetailMode
    {
        View,
        Create,
        Edit,
        Delete
    }
}
