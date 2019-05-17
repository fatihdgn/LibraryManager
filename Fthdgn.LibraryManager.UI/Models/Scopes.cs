using Fthdgn.LibraryManager.Extensions;
using Fthdgn.LibraryManager.Managers;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.Models
{
    public class Scopes
    {
        public bool Any_All => Author_All || Book_All || Loan_All || User_All || Library_All;

        public bool Author_Read { get; set; }
        public bool Author_All { get; set; }

        public bool Book_Read { get; set; }
        public bool Book_All { get; set; }

        public bool Loan_Read { get; set; }
        public bool Loan_Create { get; set; }
        public bool Loan_Create_OnBehalf { get; set; }
        public bool Loan_All { get; set; }

        public bool User_Read { get; set; }
        public bool User_All { get; set; }

        public bool Role_Read { get; set; }
        public bool Role_All { get; set; }

        public bool Library_Read { get; set; }
        public bool Library_All { get; set; }

        public void Populate(IEnumerable<string> scopes)
        {
            Author_All = scopes.Contains(ScopeManager.Author_All);
            Author_Read = Author_All || scopes.Contains(ScopeManager.Author_Read);

            Book_All = scopes.Contains(ScopeManager.Book_All);
            Book_Read = Book_All || scopes.Contains(ScopeManager.Book_Read);

            Loan_All = scopes.Contains(ScopeManager.Loan_All);
            Loan_Read = Loan_All || scopes.Contains(ScopeManager.Loan_Read);
            Loan_Create = Loan_All || scopes.Contains(ScopeManager.Loan_Create);
            Loan_Create_OnBehalf = Loan_All || scopes.Contains(ScopeManager.Loan_Create_OnBehalf);

            User_All = scopes.Contains(ScopeManager.User_All);
            User_Read = User_All || scopes.Contains(ScopeManager.User_Read);

            Role_All = scopes.Contains(ScopeManager.Role_All);
            Role_Read = Role_All || scopes.Contains(ScopeManager.Role_Read);

            Library_All = scopes.Contains(ScopeManager.Library_All);
            Library_Read = Library_All || scopes.Contains(ScopeManager.Library_Read);
        }

        public static Scopes From(IEnumerable<string> scopes) => new Scopes().With(x => x.Populate(scopes));
    }
}
