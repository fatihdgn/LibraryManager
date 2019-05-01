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
            ViewItemCommand = new RelayCommand<Options<T>>(OnItemView, IsItemViewable);
            EditItemCommand = new RelayCommand<Options<T>>(OnItemEdit, IsItemEditable);
            DeleteItemCommand = new RelayCommand<Options<T>>(OnItemDelete, IsItemDeletable);
            SelectItemCommand = new RelayCommand<Options<T>>(OnItemSelect, IsItemSelectable);
        }
        
        private bool autoSelect = true;
        public bool AutoSelect
        {
            get => autoSelect;
            set => Set(ref autoSelect, value);
        }

        private bool canSearch;
        public bool CanSearch
        {
            get => canSearch;
            set => Set(ref canSearch, value);
        }

        private bool canCreate;
        public bool CanCreate
        {
            get => canCreate;
            set => Set(ref canCreate, value);
        }

        private bool canSelect;
        public bool CanSelect
        {
            get => canSelect;
            set => Set(ref canSelect, value);
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
        protected virtual Options<T> ProvideOptions(T item) => new Options<T>(item, isSelectable: CanSelect);
        protected virtual void FetchItems()
        {
            Items.Clear();
            foreach (var item in ProvideItems())
                Items.Add(ProvideOptions(item));

            if (AutoSelect && Items.Count(x => x.IsSelectable) == 1)
                OnItemSelect(Items.FirstOrDefault(x => x.IsSelectable));
        }

        protected TaskCompletionSource<bool> createItemDialogCompletionSource;
        protected virtual bool IsItemCreatable() => CanCreate;
        public RelayCommand CreateItemCommand { get; set; }
        protected virtual void OnItemCreate()
        {
            if (CanCreate)
            {
                createItemDialogCompletionSource?.SetResult(true);
                createItemDialogCompletionSource = null;
            }
        }
        public Task<bool> CreateItemDialogAsync()
        {
            if (createItemDialogCompletionSource == null)
                createItemDialogCompletionSource = new TaskCompletionSource<bool>();
            return createItemDialogCompletionSource.Task;
        }

        protected TaskCompletionSource<Options<T>> viewItemDialogCompletionSource;
        protected virtual bool IsItemViewable(Options<T> item) => item?.IsViewable ?? false;
        public RelayCommand<Options<T>> ViewItemCommand { get; set; }
        protected virtual void OnItemView(Options<T> item)
        {
            if (IsItemViewable(item))
            {
                viewItemDialogCompletionSource?.SetResult(item);
                viewItemDialogCompletionSource = null;
            }
        }
        public Task<Options<T>> ViewItemDialogAsync()
        {
            if (viewItemDialogCompletionSource == null)
                viewItemDialogCompletionSource = new TaskCompletionSource<Options<T>>();
            return viewItemDialogCompletionSource.Task;
        }

        protected TaskCompletionSource<Options<T>> editItemDialogCompletionSource;
        protected virtual bool IsItemEditable(Options<T> item) => item?.IsEditable ?? false;
        public RelayCommand<Options<T>> EditItemCommand { get; set; }
        protected virtual void OnItemEdit(Options<T> item)
        {
            if (item?.IsEditable ?? false)
            {
                editItemDialogCompletionSource?.SetResult(item);
                editItemDialogCompletionSource = null;
            }
        }
        public Task<Options<T>> EditItemDialogAsync()
        {
            if (editItemDialogCompletionSource == null)
                editItemDialogCompletionSource = new TaskCompletionSource<Options<T>>();
            return editItemDialogCompletionSource.Task;
        }

        protected TaskCompletionSource<Options<T>> deleteItemDialogCompletionSource;
        protected virtual bool IsItemDeletable(Options<T> item) => item?.IsDeletable ?? false;
        public RelayCommand<Options<T>> DeleteItemCommand { get; set; }
        protected virtual void OnItemDelete(Options<T> item)
        {
            if (item?.IsDeletable ?? false)
            {
                deleteItemDialogCompletionSource?.SetResult(item);
                deleteItemDialogCompletionSource = null;
            }
        }
        public Task<Options<T>> DeleteItemDialogAsync()
        {
            if (deleteItemDialogCompletionSource == null)
                deleteItemDialogCompletionSource = new TaskCompletionSource<Options<T>>();
            return deleteItemDialogCompletionSource.Task;
        }

        protected TaskCompletionSource<Options<T>> selectItemDialogCompletionSource;
        protected virtual bool IsItemSelectable(Options<T> item) => item?.IsSelectable ?? false;
        public RelayCommand<Options<T>> SelectItemCommand { get; set; }
        protected virtual void OnItemSelect(Options<T> item)
        {
            if (item?.IsSelectable ?? false)
            {
                selectItemDialogCompletionSource?.SetResult(item);
                selectItemDialogCompletionSource = null;
            }
        }
        public Task<Options<T>> SelectItemDialogAsync()
        {
            if (selectItemDialogCompletionSource == null)
                selectItemDialogCompletionSource = new TaskCompletionSource<Options<T>>();
            return selectItemDialogCompletionSource.Task;
        }

        public override void OnNavigatingAway()
        {
            viewItemDialogCompletionSource?.SetResult(null);
            viewItemDialogCompletionSource = null;
            editItemDialogCompletionSource?.SetResult(null);
            editItemDialogCompletionSource = null;
            deleteItemDialogCompletionSource?.SetResult(null);
            deleteItemDialogCompletionSource = null;
            selectItemDialogCompletionSource?.SetResult(null);
            selectItemDialogCompletionSource = null;
        }
    }
}
