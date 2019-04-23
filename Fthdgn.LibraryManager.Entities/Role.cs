using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Entities
{
    public class Role : Entity
    {
        public virtual string Name { get; set; }
        public virtual StringListJsonEntity Scopes { get; set; } = new StringListJsonEntity();
        public virtual List<UserRole> Users { get; set; } = new List<UserRole>();
    }
}
