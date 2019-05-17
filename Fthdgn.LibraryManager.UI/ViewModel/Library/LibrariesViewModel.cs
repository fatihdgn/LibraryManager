using AutoMapper;
using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Extensions;
using Fthdgn.LibraryManager.Managers;
using Fthdgn.LibraryManager.Repositories;
using Fthdgn.LibraryManager.UI.Extensions;
using Fthdgn.LibraryManager.UI.Models;
using Fthdgn.LibraryManager.UI.Pages;
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
        }

        protected override IEnumerable<Library> ProvideItems() => Managers.Repositories.Libraries.Query().OrderBy(x => x.Name);
        protected override Options<Library> ProvideOptions(Library item) => Locator.Main.Scopes.As(s => new Options<Library>(item, s.Library_Read, s.Library_All, s.Library_All, CanSelect));
        protected override bool FilterItem(string search, Library item) => item.Name.ToLowerInvariant().Contains(search.ToLowerInvariant());

        protected override Library Map(LibraryViewModel item, Library model = null)
        {
            var map = base.Map(item, model);
            //if (map.Library == null) map.Library = Locator.Main.Library;
            //if (item.Author?.Id != null)
            //    map.Author = Managers.Repositories.Authors.Get(item.Author.Id);
            //else
            //    map.Author = null;
            return map;
        }

        public override void OnNavigating()
        {
            if (Locator.Main.Library != null)
            {
                CanCreate = Locator.Main.Scopes.Library_All;
                FetchItems();
            }
        }
    }
}
