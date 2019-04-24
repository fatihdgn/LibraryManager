using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Managers;
using Fthdgn.LibraryManager.UI.Models;
using Fthdgn.LibraryManager.UI.Pages;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class LibrariesViewModel : ItemsViewModel<Library>
    {
        public LibraryManagerManagers Managers { get; set; }

        public LibrariesViewModel(ViewModelLocator locator, LibraryManagerManagers managers) : base(locator)
        {
            CanSearch = true;
            AutoSelect = true;

            Managers = managers;
            Messenger.Default.Register<PropertyChangedMessage<User>>(this, pcm => FetchItems());
            if (Locator.Main.User != null) FetchItems();
        }

        protected override IEnumerable<Library> ProvideItems() => Managers.Libraries.ByUser(Locator.Main.User);
        protected override bool FilterItem(string search, Library item) => item.Name.ToLowerInvariant().Contains(search.ToLowerInvariant());
        protected override void OnItemSelect(Options<Library> item)
        {
            Locator.Main.Library = item?.Value;
            Locator.Main.GoTo<Home>(Items.Count == 1);
        }
    }

    //public class LibrariesViewModel : ViewModel
    //{
    //    public LibraryManagerManagers Managers { get; set; }
    //    public LibrariesViewModel(ViewModelLocator locator, LibraryManagerManagers managers) : base(locator)
    //    {
    //        Managers = managers;
    //        Libraries.CollectionChanged += (_, __) => RaisePropertyChanged(nameof(FilteredLibraries));
    //        Messenger.Default.Register<PropertyChangedMessage<User>>(this, pcm => FetchLibraries());
    //        SelectLibraryCommand = new RelayCommand<Library>(SelectLibrary, CanSelectLibrary);
    //        if (Locator.Main.User != null) FetchLibraries();
    //    }

    //    private string search;
    //    public string Search
    //    {
    //        get => search;
    //        set { Set(ref search, value); RaisePropertyChanged(nameof(FilteredLibraries)); }
    //    }
        
    //    public ObservableCollection<Library> Libraries { get; set; } = new ObservableCollection<Library>();
    //    public IEnumerable<Library> FilteredLibraries => string.IsNullOrEmpty(Search) ? Libraries : Libraries.Where(x => x.Name.ToLowerInvariant().Contains(Search.ToLowerInvariant()));

    //    void FetchLibraries()
    //    {
    //        Libraries.Clear();
    //        foreach (var library in Managers.Libraries.ByUser(Locator.Main.User))
    //            Libraries.Add(library);

    //        if (Libraries.Count == 1)
    //            SelectLibrary(Libraries.First());
    //    }

    //    bool CanSelectLibrary(Library library) => true;
    //    public RelayCommand<Library> SelectLibraryCommand { get; set; }
    //    void SelectLibrary(Library library)
    //    {
    //        Locator.Main.Library = library;
    //        Locator.Main.GoTo<Home>(Libraries.Count == 1);
    //    }
    //}
}
