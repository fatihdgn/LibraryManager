using Fthdgn.LibraryManager.Managers;
using Fthdgn.LibraryManager.Repositories;
using Fthdgn.LibraryManager.UI.Pages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class LoginViewModel : ViewModel
    {
        public LibraryManagerManagers Managers { get; set; }
        public LoginViewModel(ViewModelLocator locator, LibraryManagerManagers managers) : base(locator)
        {
            Name = nameof(LoginViewModel);
            DisplayName = "Kullanıcı Girişi";
            LoginCommand = new RelayCommand(Login, CanLogin);
            Managers = managers;
        }

        private string username;
        public string Username
        {
            get => username;
            set { Set(ref username, value); LoginCommand.RaiseCanExecuteChanged(); }
        }

        private string password;
        public string Password
        {
            get => password;
            set { Set(ref password, value); LoginCommand.RaiseCanExecuteChanged(); }
        }

        public bool CanLogin() => !(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password));
        public RelayCommand LoginCommand { get; set; }
        public void Login()
        {
            if(Managers.Users.Verify(Username, Password))
            {
                Locator.Main.User = Managers.Repositories.Users.GetByUsernameOrMailAddress(Username);
                Locator.Main.GoTo<UserLibraries>(removeHistory: true);
            }
        }
    }
}
