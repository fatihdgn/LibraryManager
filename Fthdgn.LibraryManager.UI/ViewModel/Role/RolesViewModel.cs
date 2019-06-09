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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class RolesViewModel : DetailedItemsViewModel<Role, RoleViewModel, RoleDetailViewModel>
    {
        public RolesViewModel(ViewModelLocator locator, RoleDetailViewModel detailViewModel, LibraryManagerManagers managers) : base(locator, detailViewModel, managers)
        {
            Name = nameof(RolesViewModel);
            DisplayName = "Roller";
            CreateText = "Yeni Rol";
            CanSearch = true;
            CanSelect = false;
            AutoSelect = false;

            Managers = managers;
            Messenger.Default.Register<PropertyChangedMessage<Library>>(this, pcm => OnNavigating());
            ViewUsersCommand = new RelayCommand<Options<Role>>(ViewLoans, CanViewLoans);
        }

        RelayCommand<Options<Role>> viewUsersCommand;
        public RelayCommand<Options<Role>> ViewUsersCommand { get => viewUsersCommand; set => Set(ref viewUsersCommand, value); }

        public bool CanViewLoans(Options<Role> item) => !CanSelect && Locator.Main.Scopes.User_Read;
        public void ViewLoans(Options<Role> item)
        {
            Locator.Users.Role = item.Value;
            Locator.Main.GoTo(Locator.Users);
        }

        protected override IEnumerable<Role> ProvideItems() => Managers.Repositories.Roles.Query().OrderBy(x => x.Name);
        protected override Options<Role> ProvideOptions(Role item) => Locator.Main.Scopes.As(s => new Options<Role>(item, s.Role_Read, s.Role_All, s.Role_All, CanSelect));
        protected override bool FilterItem(string search, Role item) => item.Name.ToLowerInvariant().Contains(search.ToLowerInvariant());

        protected override RoleViewModel Map(Role item, RoleViewModel model = null)
        {
            var map = base.Map(item, model);
            map.Scopes = new ObservableCollection<Selectable<string>>(Managers.Scopes.Read().Concat(Managers.Scopes.All()).Concat(Managers.Scopes.Loan()).Distinct().OrderBy(x => x).Select(x => new Selectable<string>(x, item.Scopes.Value.Contains(x))));
            return map;
        }
        protected override Role Map(RoleViewModel item, Role model = null)
        {
            var map = base.Map(item, model);
            map.Scopes = new StringListJsonEntity(item.Scopes.Where(x => x.IsSelected).Select(x => x.Value).ToList());

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
                CanCreate = Locator.Main.Scopes.Library_All;
                FetchItems();
            }
        }
    }
}
