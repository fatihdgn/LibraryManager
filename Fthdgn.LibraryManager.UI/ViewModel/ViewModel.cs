using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class ViewModel : ViewModelBase
    {
        string name;
        public string Name { get => name; set => Set(ref name, value?.Replace(nameof(ViewModel), string.Empty)); }

        string displayName;
        public string DisplayName { get => displayName; set => Set(ref displayName, value); }

        public ViewModelLocator Locator { get; set; }
        public ViewModel(ViewModelLocator locator)
        {
            Locator = locator;
            LaunchCommand = new RelayCommand<string>(Launch);
        }

        public RelayCommand<string> LaunchCommand { get; protected set; }
        void Launch(string item) => Process.Start(item);

        public virtual void OnNavigating() { }
        public virtual void OnNavigated() { }

        public virtual void OnNavigatingAway(string to = null) { }
        public virtual void OnNavigatedAway(string to = null) { }
    }
}
