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
    public class RoleDetailViewModel : ItemDetailViewModel<RoleViewModel>
    {
        public LibraryManagerManagers Managers { get; set; }
        public RoleDetailViewModel(ViewModelLocator locator, LibraryManagerManagers managers) : base(locator)
        {
            Name = nameof(RoleDetailViewModel);
            DisplayName = "Rol Detayı";
            Managers = managers;
            //SelectAuthorCommand = new RelayCommand(async () => await SelectAuthorAsync(), CanSelectAuthor, true);

        }


        //RelayCommand selectAuthorCommand;
        //public RelayCommand SelectAuthorCommand { get => selectAuthorCommand; set => Set(ref selectAuthorCommand, value); }

        //public bool CanSelectAuthor() => Locator.Main.Scopes.Author_Read;
        //public async Task SelectAuthorAsync()
        //{
        //    var mode = Mode;
        //    Mode = DetailMode.Select;
        //    Locator.Authors.CanSelect = true;
        //    Locator.Main.GoTo(Locator.Authors);
        //    var author = await Locator.Authors.SelectItemDialogAsync();
        //    Locator.Authors.CanSelect = false;
        //    Mode = mode;
        //    Locator.Main.GoBack();

        //    if (author != null)
        //        Item.Value.Author = author.Value;
        //}

        //public override void OnNavigating()
        //{
        //    base.OnNavigating();
        //    if (Mode == DetailMode.Create || Mode == DetailMode.Edit)
        //        Authors = new ObservableCollection<Author>(Managers.Repositories.Authors.Query().ToList());
        //}

        //ObservableCollection<Author> authors;
        //public ObservableCollection<Author> Authors { get => authors; set => Set(ref authors, value); }
    }
}
