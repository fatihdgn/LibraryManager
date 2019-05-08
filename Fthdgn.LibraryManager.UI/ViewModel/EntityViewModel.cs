using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class EntityViewModel : ViewModelBase
    {
        string id;
        public string Id { get => id; set => Set(ref id, value); }

        DateTimeOffset createdAt;
        public DateTimeOffset CreatedAt { get => createdAt; set => Set(ref createdAt, value); }

        DateTimeOffset? changedAt;
        public DateTimeOffset? ChangedAt { get => changedAt; set => Set(ref changedAt, value); }

        bool isEnabled = true;
        public bool IsEnabled { get => isEnabled; set => Set(ref isEnabled, value); }
    }
}
