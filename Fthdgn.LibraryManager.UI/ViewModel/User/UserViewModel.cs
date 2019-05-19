using Fthdgn.LibraryManager.Entities;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class UserViewModel : EntityViewModel
    {
        string name;
        public string Name { get => name; set => Set(ref name, value); }

        string surname;
        public string Surname { get => surname; set => Set(ref surname, value); }

        string username;
        public string Username { get => username; set => Set(ref username, value); }

        string mailAddress;
        public string MailAddress { get => mailAddress; set => Set(ref mailAddress, value); }
        
        string address;
        public string Address { get => address; set => Set(ref address, value); }

        string phoneNumber;
        public string PhoneNumber { get => phoneNumber; set => Set(ref phoneNumber, value); }

        string password;
        public string Password { get => password; set => Set(ref password, value); }
        
        Library library;
        public Library Library { get => library; set => Set(ref library, value); }
        
        Role role;
        public Role Role { get => role; set => Set(ref role, value); }
    }
}
