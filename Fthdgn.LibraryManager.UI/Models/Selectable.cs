using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.Models
{
    public class Selectable<T> : ViewModelBase
    {
        public Selectable() { }
        public Selectable(T value, bool isSelected = false)
        {
            Value = value;
            IsSelected = isSelected;
        }
        T value;
        public T Value { get => value; set => Set(ref this.value, value); }

        bool isSelected;
        public bool IsSelected { get => isSelected; set => Set(ref isSelected, value); }
    }
}
