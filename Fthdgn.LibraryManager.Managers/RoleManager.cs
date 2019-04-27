using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fthdgn.LibraryManager.Context;
using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Extensions;
using Fthdgn.LibraryManager.Managers;
using Fthdgn.LibraryManager.Repositories;

namespace Fthdgn.LibraryManager.Managers
{
    public class RoleManager : LibraryManagerManager
    {
        public RoleManager(LibraryManagerManagers managers) : base(managers) { }

        public Role Provide(string name, IEnumerable<string> scopes) => Repositories.Roles.GetByName(name) ?? Context.Roles.Add(new Role { Name = name, Scopes = scopes.ToList() }).With(x => Context.SaveChanges());

        public Role Customer() => Provide(nameof(Customer), Managers.Scopes.Customer());
        public Role Editor() => Provide(nameof(Editor), Managers.Scopes.Editor());
        public Role Loaner() => Provide(nameof(Loaner), Managers.Scopes.Loaner());
        public Role Admin() => Provide(nameof(Admin), Managers.Scopes.Admin());

        public IEnumerable<Role> All()
        {
            yield return Customer();
            yield return Editor();
            yield return Loaner();
            yield return Admin();
        }

        public IEnumerable<Role> Get(User user, Library library = null) => Repositories.UserRoles.Query().Where(x => x.User.Id == user.Id).As(q => library == null ? q.Where(x => x.Library.Id == null) : q.Where(x => x.Library.Id == library.Id)).Select(x => x.Role);
    }
}
