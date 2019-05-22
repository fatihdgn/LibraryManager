using Fthdgn.LibraryManager.Entities;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class BookViewModel : EntityViewModel
    {
        string name;
        public string Name { get => name; set => Set(ref name, value); }

        string description;
        public string Description { get => description; set => Set(ref description, value); }


        Author author;
        public Author Author { get => author; set => Set(ref author, value); }

        string publishedAt;
        public string PublishedAt { get => publishedAt; set => Set(ref publishedAt, value); }
        
        int? pages;
        public int? Pages { get => pages; set => Set(ref pages, value); }
        
        string lcc;
        public string LCC { get => lcc; set => Set(ref lcc, value); }
        
        string lccn;
        public string LCCN { get => lccn; set => Set(ref lccn, value); }
    }
}
