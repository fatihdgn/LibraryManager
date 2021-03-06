﻿using Fthdgn.LibraryManager.Entities;
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
    public class UserLibrariesViewModel : DetailedItemsViewModel<Library, LibraryViewModel, LibraryDetailViewModel>
    {
        public UserLibrariesViewModel(ViewModelLocator locator, LibraryDetailViewModel detailViewModel, LibraryManagerManagers managers) : base(locator, detailViewModel, managers)
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
            AutoSelect = false;
            Locator.Main.Library = item?.Value;
            Locator.Main.Scopes = Scopes.From(Managers.Scopes.Get(Locator.Main.User, Locator.Main.Library));
            Locator.Main.GoTo<Home>();

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

        public override void OnNavigatingAway(string to = null, bool isBack = false)
        {
            base.OnNavigatingAway(to, isBack);
            if (to == nameof(Login))
                Locator.Main.User = null;
        }
    }
}
