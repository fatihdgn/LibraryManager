using Fthdgn.LibraryManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Managers
{
    public class LoanManager : LibraryManagerManager
    {
        public LoanManager(LibraryManagerManagers managers) : base(managers)
        {
        }

        public IEnumerable<Loan> ByUser(User user)
        {
            if (Repositories.UserRoles.Get(user).Any(x => x.Library == null))
                foreach (var loan in Repositories.Loans.Get())
                    yield return loan;
            else
                foreach (var library in Repositories.Libraries.Filter(Repositories.UserRoles.Get(user).Select(x => x.Library)))
                    foreach (var loan in Repositories.Loans.Query().Where(x => x.Library.Id == library.Id))
                        yield return loan;
        }
    }
}
