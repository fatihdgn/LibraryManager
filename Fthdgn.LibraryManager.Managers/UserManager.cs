using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Extensions;
using Fthdgn.LibraryManager.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Managers
{
    public class UserManager : LibraryManagerManager
    {
        public UserManager(LibraryManagerManagers managers) : base(managers)
        {
        }
        
        public User Provide(string username, Func<string, User> provider) => Repositories.Users.GetByUsernameOrMailAddress(username) ?? Context.Users.Add(provider?.Invoke(username)?.With(ResolvePassword)).With(x => Context.SaveChanges());

        void ResolvePassword(User user)
        {
            if (user != null && !string.IsNullOrEmpty(user.PasswordHash) && !SecurePasswordHasher.Default.IsHashSupported(user.PasswordHash))
                user.PasswordHash = SecurePasswordHasher.Default.Hash(user.PasswordHash);
        }

        public bool Verify(string usernameOrMailAddress, string password) => SecurePasswordHasher.Default.Verify(password, Repositories.Users.GetByUsernameOrMailAddress(usernameOrMailAddress)?.PasswordHash);

        public User Admin() => Provide(nameof(Admin).ToLower(), username => new User {
            Username = username, 
            PasswordHash = username
        });
    }
}
