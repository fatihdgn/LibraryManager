using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Entities
{
    public class Media : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Uri { get; set; }
        public virtual byte[] Content { get; set; }
        public virtual Library Library { get; set; }
    }
}
