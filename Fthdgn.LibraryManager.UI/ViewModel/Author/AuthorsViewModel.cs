using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Extensions;
using Fthdgn.LibraryManager.Managers;
using Fthdgn.LibraryManager.UI.Models;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{

    public class AuthorsViewModel : DetailedItemsViewModel<Author, AuthorViewModel, AuthorDetailViewModel>
    {
        public AuthorsViewModel(ViewModelLocator locator, AuthorDetailViewModel detailViewModel, LibraryManagerManagers managers) : base(locator, detailViewModel, managers)
        {
            Name = nameof(AuthorsViewModel);
            DisplayName = "Yazarlar";
            CreateText = "Yeni Yazar";
            CanSearch = true;
            CanSelect = false;
            AutoSelect = false;

            Managers = managers;
            Messenger.Default.Register<PropertyChangedMessage<Library>>(this, pcm => OnNavigating());
        }

        protected override IEnumerable<Author> ProvideItems() => Managers.Repositories.Authors.Query().Where(x => x.Library.Id == Locator.Main.Library.Id).OrderBy(x => x.Name);
        protected override Options<Author> ProvideOptions(Author item) => Locator.Main.Scopes.As(s => new Options<Author>(item, s.Author_Read, s.Author_All, s.Author_All, CanSelect));
        protected override bool FilterItem(string search, Author item) => item.Name.ToLowerInvariant().Contains(search.ToLowerInvariant()) || item.Surname.ToLowerInvariant().Contains(search.ToLowerInvariant());

        protected override Author Map(AuthorViewModel item, Author model = null)
        {
            var map = base.Map(item, model);
            if (map.Library == null) map.Library = Locator.Main.Library;
            return map;
        }

        public override void OnNavigating()
        {
            if (Locator.Main.Library != null && Locator.Main.Scopes != null)
            {
                CanCreate = Locator.Main.Scopes.Author_All;
                FetchItems();
            }
        }
    }
}
