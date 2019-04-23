using Fthdgn.LibraryManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Managers
{
    public class LibraryManager : LibraryManagerManager
    {
        public LibraryManager(LibraryManagerManagers managers) : base(managers)
        {
        }

        public IEnumerable<Library> ByUser(User user) => Repositories.UserRoles.Get(user).Any(x => x.Library == null) ? Repositories.Libraries.Get() : Repositories.UserRoles.Get(user).Select(x => x.Library).ToList();
    }
}
