using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.UI.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class HomeViewModel : ViewModel
    {
        public HomeViewModel(ViewModelLocator locator) : base(locator)
        {
            Name = "Ana Sayfa";
            Messenger.Default.Register<PropertyChangedMessage<User>>(this, pcm => RaisePropertyChanged(nameof(User)));
            Messenger.Default.Register<PropertyChangedMessage<Library>>(this, pcm => RaisePropertyChanged(nameof(Library)));
            Messenger.Default.Register<PropertyChangedMessage<Scopes>>(this, pcm => RaisePropertyChanged(nameof(Scopes)));
            GoToCommand = new RelayCommand<string>(GoTo, CanGoTo);
        }

        public User User => Locator.Main.User;
        public Library Library => Locator.Main.Library;
        public Scopes Scopes => Locator.Main.Scopes;

        public bool CanGoTo(string page) => true;
        public RelayCommand<string> GoToCommand { get; protected set; }
        public void GoTo(string page) => Locator.Main.GoTo(page);
    }
}
