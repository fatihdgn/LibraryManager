using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Entities
{
    public class Library : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string MailAddress { get; set; }
        public virtual Media Image { get; set; }
        public virtual List<Media> Media { get; set; }
        public virtual List<Book> Books { get; set; }
        public virtual List<Author> Authors { get; set; }
        public virtual List<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual List<Loan> Loans { get; set; } = new List<Loan>();

        public override string ToString() => Name;
    }
}
