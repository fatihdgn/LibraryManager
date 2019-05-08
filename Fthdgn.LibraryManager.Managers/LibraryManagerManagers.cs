using Fthdgn.LibraryManager.Context;
using Fthdgn.LibraryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Managers
{
    public class LibraryManagerManagers
    {
        public LibraryManagerDB Context { get; set; }
        public LibraryManagerRepositories Repositories { get; set; }

        public LibraryManagerManagers(LibraryManagerRepositories repositories)
        {
            Repositories = repositories;
            Context = Repositories?.Context;
            Roles = new RoleManager(this);
            Scopes = new ScopeManager(this);
            Users = new UserManager(this);
            UserRoles = new UserRoleManager(this);
            Libraries = new LibraryManager(this);
        }

        public RoleManager Roles { get; set; }
        public ScopeManager Scopes { get; set; }
        public UserManager Users { get; set; }
        public UserRoleManager UserRoles { get; set; }
        public LibraryManager Libraries { get; set; }

        public void Save() => Context?.SaveChanges();

        public void Provide()
        {
            Roles.All().ToList();
            Users.Admin();
            UserRoles.Admin();
        }
    }
}
