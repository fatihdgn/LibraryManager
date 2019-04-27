using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Entities
{
    public class Book : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTimeOffset? PublishedAt { get; set; }
        public virtual Author Author { get; set; }
        public virtual int? Pages { get; set; }
        public virtual Media Image { get; set; }
        public virtual string LCC { get; set; }
        public virtual string LCCN { get; set; }
        public virtual Library Library { get; set; }
        public virtual List<Loan> Loans { get; set; }
    }
}
