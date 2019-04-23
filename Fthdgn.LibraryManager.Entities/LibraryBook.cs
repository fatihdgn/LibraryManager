using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Entities
{
    public class LibraryBook : Entity
    {
        public Library Library { get; set; }
        public Book Book { get; set; }
        public List<Loan> Loans { get; set; } = new List<Loan>();
    }
}
