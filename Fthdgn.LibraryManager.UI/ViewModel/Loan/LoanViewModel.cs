using Fthdgn.LibraryManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class LoanViewModel : EntityViewModel
    {
        User user;
        public User User { get => user; set => Set(ref user, value); }
        
        Book book;
        public Book Book { get => book; set => Set(ref book, value); }
        
        DateTimeOffset? returnsAt;
        public DateTimeOffset? ReturnsAt { get => returnsAt; set => Set(ref returnsAt, value); }
        
        DateTimeOffset? returnedAt;
        public DateTimeOffset? ReturnedAt { get => returnedAt; set => Set(ref returnedAt, value); }
    }
}
