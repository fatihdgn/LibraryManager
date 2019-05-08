using Fthdgn.LibraryManager.Context;
using Fthdgn.LibraryManager.Entities;
using System.Linq;

namespace Fthdgn.LibraryManager.Repositories
{
    public class LibraryManagerRepositories
    {
        public LibraryManagerDB Context { get; set; }
        public LibraryManagerRepositories(LibraryManagerDB context)
        {
            Context = context;
            Users = new UserRepository(Context);
            Libraries = new LibraryManagerRepository<Library>(Context);
            Books = new LibraryManagerRepository<Book>(Context);
            Authors = new LibraryManagerRepository<Author>(Context);
            Roles = new RoleRepository(Context);
            Loans = new LibraryManagerRepository<Loan>(Context);
            Media = new LibraryManagerRepository<Media>(Context);
            UserRoles = new UserRoleRepository(Context);
        }

        public UserRepository Users { get; set; }
        public LibraryManagerRepository<Library> Libraries { get; set; }
        public LibraryManagerRepository<Book> Books { get; set; }
        public LibraryManagerRepository<Author> Authors { get; set; }
        public RoleRepository Roles { get; set; }
        public LibraryManagerRepository<Loan> Loans { get; set; }
        public LibraryManagerRepository<Media> Media { get; set; }
        
        public UserRoleRepository UserRoles { get; set; }

        public void Save() => Context?.SaveChanges();
    }
}