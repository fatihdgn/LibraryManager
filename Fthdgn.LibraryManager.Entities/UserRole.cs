using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Entities
{
    public class UserRole : Entity
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
        public virtual Library Library { get; set; }
    }
}
