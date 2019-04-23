using Fthdgn.LibraryManager.Context;
using Fthdgn.LibraryManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Repositories
{
    public class LibraryManagerRepository<TEntity> : Repository<LibraryManagerDB, TEntity>
        where TEntity : Entity
    {
        public LibraryManagerRepository(LibraryManagerDB context) : base(context) { }
    }
}
