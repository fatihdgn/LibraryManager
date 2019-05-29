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
    public class LibrariesViewModel : DetailedItemsViewModel<Library, LibraryViewModel, LibraryDetailViewModel>
    {
        public LibrariesViewModel(ViewModelLocator locator, LibraryDetailViewModel detailViewModel, LibraryManagerManagers managers) : base(locator, detailViewModel, managers)
        {
            Name = nameof(LibrariesViewModel);
            DisplayName = "Kütüphaneler";
            CreateText = "Yeni Kütüphane";
            CanSearch = true;
            CanSelect = false;
            AutoSelect = false;

            Managers = managers;
            Messenger.Default.Register<PropertyChangedMessage<Library>>(this, pcm => OnNavigating());
            ViewUsersCommand = new RelayCommand<Options<Library>>(ViewLoans, CanViewLoans);
        }

        RelayCommand<Options<Library>> viewUsersCommand;
        public RelayCommand<Options<Library>> ViewUsersCommand { get => viewUsersCommand; set => Set(ref viewUsersCommand, value); }

        public bool CanViewLoans(Options<Library> item) => !CanSelect && Locator.Main.Scopes.User_Read;
        public void ViewLoans(Options<Library> item)
        {
            Locator.Users.Library = item.Value;
            Locator.Main.GoTo(Locator.Users);
        }
        protected override IEnumerable<Library> ProvideItems() => Managers.Libraries.ByUser(Locator.Main.User).OrderBy(x => x.Name);
        protected override Options<Library> ProvideOptions(Library item) => Locator.Main.Scopes.As(s => new Options<Library>(item, s.Library_Read, s.Library_All, s.Library_All, CanSelect));
        protected override bool FilterItem(string search, Library item) => item.Name.ToLowerInvariant().Contains(search.ToLowerInvariant());

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
