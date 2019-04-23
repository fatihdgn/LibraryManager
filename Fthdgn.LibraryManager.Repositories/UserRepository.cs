using Fthdgn.LibraryManager.Context;
using Fthdgn.LibraryManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Repositories
{
    public class UserRepository : LibraryManagerRepository<User>
    {
        public UserRepository(LibraryManagerDB context) : base(context)
        {
        }

        public User GetByUsernameOrMailAddress(string usernameOrMailAddress) => Query().FirstOrDefault(x => x.Username == usernameOrMailAddress || x.MailAddress == usernameOrMailAddress);
    }
}
