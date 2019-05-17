using Fthdgn.LibraryManager.Entities;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class LibraryViewModel : EntityViewModel
    {
        string name;
        public string Name { get => name; set => Set(ref name, value); }

        string address;
        public string Address { get => address; set => Set(ref address, value); }
        
        string phoneNumber;
        public string PhoneNumber { get => phoneNumber; set => Set(ref phoneNumber, value); }
        
        string mailAddress;
        public string MailAddress { get => mailAddress; set => Set(ref mailAddress, value); }
    }
}
