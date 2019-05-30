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
        public Options(T value, bool isViewable = false, bool isEditable = false, bool isDeletable = false, bool isSelectable = false, object tag = null)
        {
            Value = value;
            IsViewable = isViewable;
            IsEditable = isEditable;
            IsDeletable = isDeletable;
            IsSelectable = isSelectable;
            Tag = tag;
            ToggleOptionsCommand = new RelayCommand(ToggleOptions);
        }

        bool isChanged = false;
        public bool IsChanged
        {
            get => isChanged;
            set => Set(ref isChanged, value);
        }

        private T value;
        public T Value
        {
            get => value;
            set
            {
                if (Set(ref this.value, value) && this.value is ViewModelBase)
                    (this.value as ViewModelBase).PropertyChanged += (_, __) => IsChanged = true;
            }
        }


        object tag;
        public object Tag { get => tag; set { Set(ref tag, value); RaisePropertyChanged(nameof(HasTag)); } }

        public bool HasTag => Tag != null;

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
