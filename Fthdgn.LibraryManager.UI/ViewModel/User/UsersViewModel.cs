using AutoMapper;
using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Extensions;
using Fthdgn.LibraryManager.Managers;
using Fthdgn.LibraryManager.Repositories;
using Fthdgn.LibraryManager.UI.Extensions;
using Fthdgn.LibraryManager.UI.Models;
using Fthdgn.LibraryManager.UI.Pages;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class UsersViewModel : DetailedItemsViewModel<User, UserViewModel, UserDetailViewModel>
    {
        public UsersViewModel(ViewModelLocator locator, UserDetailViewModel detailViewModel, LibraryManagerManagers managers) : base(locator, detailViewModel, managers)
        {
            Name = nameof(UsersViewModel);
            DisplayName = "Kullanıcılar";
            CreateText = "Yeni Kullanıcı";
            CanSearch = true;
            CanSelect = false;
            AutoSelect = false;

            Managers = managers;
            Messenger.Default.Register<PropertyChangedMessage<Library>>(this, pcm => OnNavigating());
            ViewLoansCommand = new RelayCommand<Options<User>>(ViewLoans, CanViewLoans);
            
        }

        protected override IEnumerable<User> ProvideItems() => Managers.Users.ByUser(Locator.Main.User).OrderBy(x => x.Name);
        protected override Options<User> ProvideOptions(User item) => Locator.Main.Scopes.As(s => new Options<User>(item, s.User_Read, s.User_All, s.User_All, CanSelect));
        protected override bool FilterItem(string search, User item) => item.Name.ToLowerInvariant().Contains(search.ToLowerInvariant()) || item.Surname.ToLowerInvariant().Contains(search.ToLowerInvariant()) || item.MailAddress.ToLowerInvariant().Contains(search.ToLowerInvariant());

        RelayCommand<Options<User>> viewLoansCommand;
        public RelayCommand<Options<User>> ViewLoansCommand { get => viewLoansCommand; set => Set(ref viewLoansCommand, value); }

        public bool CanViewLoans(Options<User> item) => !CanSelect && Locator.Main.Scopes.Loan_Create_OnBehalf;
        public void ViewLoans(Options<User> item)
        {
            Locator.Loans.User = item.Value;
            Locator.Main.GoTo(Locator.Loans);
        }

        protected override UserViewModel Map(User item, UserViewModel model = null)
        {
            var map = base.Map(item, model);

            var userRole = Managers.Repositories.UserRoles.Query().FirstOrDefault(x => x.User.Id == map.Id) ?? Managers.Repositories.UserRoles.Add(new UserRole { User = item });
            map.Library = userRole.Library;
            map.Role = userRole.Role;
            return map;
        }

        protected override User Map(UserViewModel item, User model = null)
        {
            var map = base.Map(item, model);
            if (item.Password != null)
            {
                map.PasswordHash = item.Password;
                Managers.Users.ResolvePassword(map);
            }
            var userRole = Managers.Repositories.UserRoles.Query().FirstOrDefault(x => x.User.Id == map.Id) ?? Managers.Repositories.UserRoles.Add(new UserRole { User = map });
            userRole.Library = item.Library;
            userRole.Role = item.Role ?? Managers.Roles.Customer();

            //if (map.Library == null) map.Library = Locator.Main.Library;
            //if (item.Author?.Id != null)
            //    map.Author = Managers.Repositories.Authors.Get(item.Author.Id);
            //else
            //    map.Author = null;
            return map;
        }

        public override void OnNavigating()
        {
            if (Locator.Main.Library != null && Locator.Main.Scopes != null)
            {
                CanCreate = Locator.Main.Scopes.User_All;
                FetchItems();
            }
        }
    }
}
