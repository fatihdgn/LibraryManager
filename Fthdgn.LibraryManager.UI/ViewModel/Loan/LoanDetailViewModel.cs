using AutoMapper;
using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Managers;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class LoanDetailViewModel : ItemDetailViewModel<LoanViewModel>
    {
        public LibraryManagerManagers Managers { get; set; }
        public LoanDetailViewModel(ViewModelLocator locator, LibraryManagerManagers managers) : base(locator)
        {
            Name = nameof(LoanDetailViewModel);
            DisplayName = "Ödünç Detayı";
            Managers = managers;
        }
    }
}
