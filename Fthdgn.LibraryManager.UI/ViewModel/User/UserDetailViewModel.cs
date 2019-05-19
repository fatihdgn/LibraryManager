using AutoMapper;
using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Managers;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class UserDetailViewModel : ItemDetailViewModel<UserViewModel>
    {
        public LibraryManagerManagers Managers { get; set; }
        public UserDetailViewModel(ViewModelLocator locator, LibraryManagerManagers managers) : base(locator)
        {
            Name = nameof(UserDetailViewModel);
            DisplayName = "Kullanıcı Detayı";
            Managers = managers;
            SelectLibraryCommand = new RelayCommand(async () => await SelectLibraryAsync(), CanSelectLibrary, true);
            SelectRoleCommand = new RelayCommand(async () => await SelectRoleAsync(), CanSelectRole, true);
        }

        RelayCommand selectLibraryCommand;
        public RelayCommand SelectLibraryCommand { get => selectLibraryCommand; set => Set(ref selectLibraryCommand, value); }

        public bool CanSelectLibrary() => Locator.Main.Scopes.Library_Read;
        public async Task SelectLibraryAsync()
        {
            var mode = Mode;
            Mode = DetailMode.Select;
            Locator.Libraries.CanSelect = true;
            Locator.Main.GoTo(Locator.Libraries);
            var library = await Locator.Libraries.SelectItemDialogAsync();
            Locator.Libraries.CanSelect = false;
            Mode = mode;
            Locator.Main.GoBack();

            if (library != null)
                Item.Value.Library = library.Value;
        }

        RelayCommand selectRoleCommand;
        public RelayCommand SelectRoleCommand { get => selectRoleCommand; set => Set(ref selectRoleCommand, value); }

        public bool CanSelectRole() => Locator.Main.Scopes.Role_Read;
        public async Task SelectRoleAsync()
        {
            var mode = Mode;
            Mode = DetailMode.Select;
            Locator.Roles.CanSelect = true;
            Locator.Main.GoTo(Locator.Roles);
            var role = await Locator.Roles.SelectItemDialogAsync();
            Locator.Roles.CanSelect = false;
            Mode = mode;
            Locator.Main.GoBack();

            if (role != null)
                Item.Value.Role = role.Value;
        }
    }
}
