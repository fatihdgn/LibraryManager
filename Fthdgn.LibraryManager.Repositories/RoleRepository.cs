using Fthdgn.LibraryManager.Context;
using Fthdgn.LibraryManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Repositories
{
    public class RoleRepository : LibraryManagerRepository<Role>
    {
        public RoleRepository(LibraryManagerDB context) : base(context)
        {
        }

        public Role GetByName(string name) => Query().FirstOrDefault(x => x.Name == name);
    }
}
