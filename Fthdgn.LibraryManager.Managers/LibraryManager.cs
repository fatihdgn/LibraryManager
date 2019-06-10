using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Extensions;
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

        public IEnumerable<Library> ByUser(User user) => Repositories.UserRoles.Get(user).Any(x => x.Library == null) ? Repositories.Libraries.Get() : Repositories.Libraries.Filter(Repositories.UserRoles.Get(user).Select(x => x.Library)).ToList();

        public const string MainLibraryName = "Ana Kütüphane";
        public Library Main() => Repositories.Libraries.Query().FirstOrDefault(x => x.Name == MainLibraryName) ?? Repositories.Libraries.Add(new Library
        {
            Name = MainLibraryName
        }).With(x => Repositories.Save());
    }
}
