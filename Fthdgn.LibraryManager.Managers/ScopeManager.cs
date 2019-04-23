using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fthdgn.LibraryManager.Context;

namespace Fthdgn.LibraryManager.Managers
{
    public class ScopeManager : LibraryManagerManager
    {
        public const string Author_Read = "author:read";
        public const string Author_All = "author:all";

        public const string Book_Read = "book:read";
        public const string Book_All = "book:all";

        public const string Customer_Read = "customer:read";
        public const string Customer_All = "customer:all";

        public const string Loan_Read = "loan:read";
        public const string Loan_Create = "loan:create";
        public const string Loan_Create_OnBehalf = "loan:create:onbehalf";
        public const string Loan_All = "loan:all";

        public const string User_Read = "user:read";
        public const string User_All = "user:all";

        public const string Role_Read = "role:read";
        public const string Role_All = "role:all";
        
        public ScopeManager(LibraryManagerManagers managers) : base(managers) { }

        public IEnumerable<string> Read()
        {
            yield return Author_Read;
            yield return Book_Read;
            yield return Loan_Read;
            yield return User_Read;
            yield return Role_Read;
        }

        public IEnumerable<string> All()
        {
            yield return Author_All;
            yield return Book_All;
            yield return Customer_All;
            yield return Loan_All;
            yield return User_All;
            yield return Role_All;
        }

        public IEnumerable<string> Customer()
        {
            yield return Author_Read;
            yield return Book_Read;
            yield return Loan_Create;
        }

        public IEnumerable<string> Editor()
        {
            yield return Author_All;
            yield return Book_All;
            yield return Loan_Read;
            yield return User_Read;
            yield return Role_Read;
        }

        public IEnumerable<string> Loaner()
        {
            yield return Author_Read;
            yield return Book_Read;
            yield return Loan_Create_OnBehalf;
            yield return User_Read;
            yield return Role_Read;
        }

        public IEnumerable<string> Admin()
        {
            yield return Author_All;
            yield return Book_All;
            yield return Customer_All;
            yield return Loan_All;
            yield return User_All;
            yield return Role_All;
        }
    }
}
