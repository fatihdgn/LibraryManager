using Fthdgn.LibraryManager.UI.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public abstract class ItemsViewModel<T> : ViewModel
        where T : class
    {
        public ItemsViewModel(ViewModelLocator locator) : base(locator)
        {
            Items.CollectionChanged += (_, __) => RaisePropertyChanged(nameof(FilteredItems));
            SelectItemCommand = new RelayCommand<T>(OnSelectItem, CanSelectItem);
        }

        private bool canSearch;
        public bool CanSearch
        {
            get => canSearch;
            set { Set(ref canSearch, value); }
        }

        private bool autoSelect = true;
        public bool AutoSelect
        {
            get => autoSelect;
            set { Set(ref autoSelect, value); }
        }

        private string search;
        public string Search
        {
            get => search;
            set { Set(ref search, value); RaisePropertyChanged(nameof(FilteredItems)); }
        }

        public ObservableCollection<Options<T>> Items { get; set; } = new ObservableCollection<Options<T>>();
        protected virtual bool FilterItem(string search, T item) => true;
        public IEnumerable<Options<T>> FilteredItems => string.IsNullOrEmpty(Search) ? Items : Items.Where(x => FilterItem(Search, x?.Value));

        protected abstract IEnumerable<T> ProvideItems();
        protected virtual Options<T> ProvideOptions(T item) => new Options<T>(item, isSelectable: true); 
        protected virtual void FetchItems()
        {
            Items.Clear();
            foreach (var item in ProvideItems())
                Items.Add(ProvideOptions(item));

            if (AutoSelect && Items.Count == 1)
                OnSelectItem(Items.First().Value);
        }


        protected virtual bool CanSelectItem(T item) => true;
        public RelayCommand<T> SelectItemCommand { get; set; }
        protected abstract void OnSelectItem(T item);
    }
}
