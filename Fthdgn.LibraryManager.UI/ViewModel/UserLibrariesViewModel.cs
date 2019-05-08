using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Extensions;
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
    public class UserLibrariesViewModel : ItemsViewModel<Library>
    {
        public LibraryManagerManagers Managers { get; set; }

        public UserLibrariesViewModel(ViewModelLocator locator, LibraryManagerManagers managers) : base(locator)
        {
            Name = nameof(UserLibrariesViewModel);
            DisplayName = "Kütüphaneler";
            CreateText = "Yeni Kütüphane";
            CanSearch = true;
            CanSelect = true;
            AutoSelect = true;

            Managers = managers;
            Messenger.Default.Register<PropertyChangedMessage<User>>(this, pcm => OnNavigating());
        }

        protected override IEnumerable<Library> ProvideItems() => Managers.Libraries.ByUser(Locator.Main.User);
        protected override Options<Library> ProvideOptions(Library item) => Scopes.From(Managers.Scopes.Get(Locator.Main.User, item)).As(s => new Options<Library>(item, s.Library_Read, s.Library_All, s.Library_All, true));
        protected override bool FilterItem(string search, Library item) => item.Name.ToLowerInvariant().Contains(search.ToLowerInvariant());
        protected override async Task OnItemSelectAsync(Options<Library> item)
        {
            await base.OnItemSelectAsync(item);
            Locator.Main.Library = item?.Value;
            Locator.Main.Scopes = Scopes.From(Managers.Scopes.Get(Locator.Main.User, Locator.Main.Library));
            Locator.Main.GoTo<Home>(Items.Count == 1);
        }

        public override void OnNavigating()
        {
            Locator.Main.Library = null;
            if (Locator.Main.User != null)
            {
                FetchItems();
                CanCreate = Scopes.From(Managers.Scopes.Get(Locator.Main.User)).Library_All;
            }
        }
    }
}
