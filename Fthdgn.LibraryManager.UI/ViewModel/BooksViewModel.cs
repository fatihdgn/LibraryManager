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
    public class BooksViewModel : ItemsViewModel<Book>
    {
        public LibraryManagerManagers Managers { get; set; }

        public BooksViewModel(ViewModelLocator locator, LibraryManagerManagers managers) : base(locator)
        {
            Name = "Kitaplar";
            CanSearch = true;
            CanSelect = false;
            AutoSelect = false;

            Managers = managers;
            Messenger.Default.Register<PropertyChangedMessage<Library>>(this, pcm => OnNavigating());
        }

        protected override IEnumerable<Book> ProvideItems() => Managers.Repositories.Books.Query().Where(x => x.Library.Id == Locator.Main.Library.Id);
        protected override Options<Book> ProvideOptions(Book item) => Locator.Main.Scopes.As(s => new Options<Book>(item, s.Book_Read, s.Book_All, s.Book_All, CanSelect));
        protected override bool FilterItem(string search, Book item) => item.Name.ToLowerInvariant().Contains(search.ToLowerInvariant());

        protected override void OnItemView(Options<Book> item)
        {
            base.OnItemView(item);

        }

        public override void OnNavigating()
        {
            if (Locator.Main.Library != null)
            {
                CanCreate = Locator.Main.Scopes.Book_All;
                FetchItems();
            }
        }
    }
}
