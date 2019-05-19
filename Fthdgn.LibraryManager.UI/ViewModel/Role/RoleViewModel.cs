using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.UI.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class RoleViewModel : EntityViewModel
    {
        string name;
        public string Name { get => name; set => Set(ref name, value); }


        ObservableCollection<Selectable<string>> scopes;
        public ObservableCollection<Selectable<string>> Scopes { get => scopes; set => Set(ref scopes, value); }
    }
}
