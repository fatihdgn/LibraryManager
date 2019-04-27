using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.Models
{
    public class Options<T> : ObservableObject
    {
        public Options(T value, bool isViewable = false, bool isEditable = false, bool isDeletable = false, bool isSelectable = false)
        {
            Value = value;
            IsViewable = isViewable;
            IsEditable = isEditable;
            IsDeletable = isDeletable;
            IsSelectable = isSelectable;
            ToggleOptionsCommand = new RelayCommand(ToggleOptions);
        }
        private T value;
        public T Value
        {
            get => value;
            set => Set(ref this.value, value);
        }

        private bool isViewable;
        public bool IsViewable
        {
            get => isViewable;
            set { if (Set(ref isViewable, value)) RaisePropertyChanged(nameof(IsOptionsAvailable)); }
        }

        private bool isEditable;
        public bool IsEditable
        {
            get => isEditable;
            set { if (Set(ref isEditable, value)) RaisePropertyChanged(nameof(IsOptionsAvailable)); }
        }

        private bool isDeletable;
        public bool IsDeletable
        {
            get => isDeletable;
            set { if (Set(ref isDeletable, value)) RaisePropertyChanged(nameof(IsOptionsAvailable)); }
        }

        private bool isSelectable;
        public bool IsSelectable
        {
            get => isSelectable;
            set => Set(ref isSelectable, value);
        }

        public bool IsOptionsAvailable => IsViewable || IsEditable || IsDeletable;

        private bool isOptionsVisible;
        public bool IsOptionsVisible
        {
            get => isOptionsVisible;
            set { Set(ref isOptionsVisible, value); RaisePropertyChanged(nameof(IsOptionsHidden)); }
        }
        public bool IsOptionsHidden => !IsOptionsVisible;

        public RelayCommand ToggleOptionsCommand { get; set; }
        public void ToggleOptions() => IsOptionsVisible = !IsOptionsVisible;
    }
}
