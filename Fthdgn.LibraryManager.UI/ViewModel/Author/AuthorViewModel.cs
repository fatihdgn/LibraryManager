using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class AuthorViewModel : EntityViewModel
    {

        string name;
        public string Name { get => name; set => Set(ref name, value); }

        string surname;
        public string Surname { get => surname; set => Set(ref surname, value); }
    }
}
