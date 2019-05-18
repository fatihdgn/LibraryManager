using Fthdgn.LibraryManager.Managers;
using Fthdgn.LibraryManager.Repositories;
using Fthdgn.LibraryManager.UI.Caching;
using Fthdgn.LibraryManager.UI.Models;
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
        public CredentialCacheFile Cache { get; set; }
        public LibraryManagerManagers Managers { get; set; }
        public LoginViewModel(ViewModelLocator locator, LibraryManagerManagers managers, CredentialCacheFile cache) : base(locator)
        {
            Name = nameof(LoginViewModel);
            DisplayName = "Kullanıcı Girişi";
            LoginCommand = new RelayCommand(Login, CanLogin);
            Managers = managers;
            Cache = cache;
            RetrieveFromCache();
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

        private bool rememberMe;
        public bool RememberMe
        {
            get => rememberMe;
            set { Set(ref rememberMe, value); LoginCommand.RaiseCanExecuteChanged(); }
        }

        void RetrieveFromCache()
        {
            Cache.WithState(s =>
            {
                if (s != null)
                {
                    Username = s.Username;
                    Password = s.Password;
                    RememberMe = true;
                }
            });
        }

        public bool CanLogin() => !(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password));
        public RelayCommand LoginCommand { get; set; }
        public void Login()
        {
            if (Managers.Users.Verify(Username, Password))
            {
                Locator.Main.User = Managers.Repositories.Users.GetByUsernameOrMailAddress(Username);
                Cache.State = RememberMe ? new Credential(this.Username, this.Password) : null;
                Locator.Main.GoTo<UserLibraries>();
            }
        }
    }
}
