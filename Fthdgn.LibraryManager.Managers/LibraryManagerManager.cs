using Fthdgn.LibraryManager.Context;
using Fthdgn.LibraryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Managers
{
    public class LibraryManagerManager
    {
        public LibraryManagerDB Context { get; set; }
        public LibraryManagerRepositories Repositories { get; set; }
        public LibraryManagerManagers Managers { get; set; }
        public LibraryManagerManager(LibraryManagerManagers managers)
        {
            Managers = managers;
            Repositories = Managers?.Repositories;
            Context = Managers?.Context;
        }
    }
}
