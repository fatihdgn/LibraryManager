using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Managers
{
    public class UserRoleManager : LibraryManagerManager
    {
        public UserRoleManager(LibraryManagerManagers managers) : base(managers)
        {
        }
        
        public UserRole Provide(User user, Role role, Library library = null) => Repositories.UserRoles.Get(user, role, library).FirstOrDefault() ?? Context.UserRoles.Add(new UserRole { User = user, Role = role, Library = library }).With(x => Context.SaveChanges());

        public UserRole Admin() => Provide(Managers.Users.Admin(), Managers.Roles.Admin());
    }
}
