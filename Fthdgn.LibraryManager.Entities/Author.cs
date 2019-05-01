using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Entities
{
    public class Author : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
        public virtual Library Library { get; set; }
    }
}
