using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Entities
{
    public class Loan : Entity
    {
        public virtual User User { get; set; }
        public virtual LibraryBook Book { get; set; }
        public virtual DateTimeOffset? LoanedAt { get; set; }
        public virtual DateTimeOffset? ReturnsAt { get; set; }
        public virtual DateTimeOffset? ReturnedAt { get; set; }
    }
}
