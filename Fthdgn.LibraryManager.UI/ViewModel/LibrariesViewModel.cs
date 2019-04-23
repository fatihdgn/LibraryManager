using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Managers;
using Fthdgn.LibraryManager.UI.Pages;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class LibrariesViewModel : ViewModel
    {
        public LibraryManagerManagers Managers { get; set; }
        public LibrariesViewModel(ViewModelLocator locator, LibraryManagerManagers managers) : base(locator)
        {
            Managers = managers;
            Locator.Main.PropertyChanged += (_, e) => { if (e.PropertyName == nameof(User)) RaisePropertyChanged(nameof(User)); FetchLibraries(); };
            SelectLibraryCommand = new RelayCommand<Library>(SelectLibrary, CanSelectLibrary);
        }

        public User User => Locator.Main.User;
        public ObservableCollection<Library> Libraries { get; set; } = new ObservableCollection<Library>();
        public string DisplayName => User?.Name ?? User?.Username ?? string.Empty;

        void FetchLibraries()
        {
            Libraries.Clear();
            foreach (var library in Managers.Libraries.ByUser(User))
                Libraries.Add(library);

            if (Libraries.Count == 1)
                SelectLibrary(Libraries.First());
        }

        bool CanSelectLibrary(Library library) => true;
        public RelayCommand<Library> SelectLibraryCommand { get; set; }
        void SelectLibrary(Library library)
        {
            Locator.Main.Library = library;
            Locator.Main.GoTo<Home>(Libraries.Count == 1);
        }
    }
}
