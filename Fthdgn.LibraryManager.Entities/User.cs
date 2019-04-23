using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Entities
{
    public class User : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Username { get; set; }
        public virtual string MailAddress { get; set; }
        public virtual string Address { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual Media Image { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
