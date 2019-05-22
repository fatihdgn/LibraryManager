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

        public IEnumerable<User> ByUser(User user)
        {
            if (Repositories.UserRoles.Get(user).Any(x => x.Library == null))
                foreach (var u in Repositories.Users.Get())
                    yield return u;
            else
                foreach (var library in Repositories.Libraries.Filter(Repositories.UserRoles.Get(user).Select(x => x.Library)))
                    foreach (var u in Repositories.Users.Filter(Repositories.UserRoles.Get(null, null, library).Select(x => x.User)))
                        yield return u;
        }

        public User Provide(string username, Func<string, User> provider) => Repositories.Users.GetByUsernameOrMailAddress(username) ?? Context.Users.Add(provider?.Invoke(username)?.With(ResolvePassword)).With(x => Context.SaveChanges());

        public void ResolvePassword(User user)
        {
            if (user != null && !string.IsNullOrEmpty(user.PasswordHash) && !SecurePasswordHasher.Default.IsHashSupported(user.PasswordHash))
                user.PasswordHash = SecurePasswordHasher.Default.Hash(user.PasswordHash);
        }

        public bool Verify(string usernameOrMailAddress, string password) => SecurePasswordHasher.Default.Verify(password, Repositories.Users.GetByUsernameOrMailAddress(usernameOrMailAddress)?.PasswordHash);

        public User Admin() => Provide(nameof(Admin).ToLower(), username => new User
        {
            Username = username,
            Name = nameof(Admin),
            PasswordHash = username
        });
    }
}
