using Fthdgn.LibraryManager.Context;
using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Repositories
{
    public class UserRoleRepository : LibraryManagerRepository<UserRole>
    {
        public UserRoleRepository(LibraryManagerDB context) : base(context)
        {
        }

        public IEnumerable<UserRole> Get(User user = null, Role role = null, Library library = null) => 
            Query()
                .As(q => user != null ? q.Where(x => x.User.Id == user.Id) : q)
                .As(q => role != null ? q.Where(x => x.Role.Id == role.Id) : q)
                .As(q => library != null ? q.Where(x => x.Library.Id == library.Id) : q);
    }
}
