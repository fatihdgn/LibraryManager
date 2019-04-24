﻿using Fthdgn.LibraryManager.UI.Models;
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

            if (AutoSelect && Items.Count(x => x.IsSelectable) == 1)
                OnItemSelect(Items.FirstOrDefault(x => x.IsSelectable));
        }
        
        protected virtual bool IsItemViewable(Options<T> item) => item?.IsViewable ?? false;
        public RelayCommand<Options<T>> ViewItemCommand { get; set; }
        protected virtual void OnItemView(Options<T> item)
        {
            if (item?.IsViewable ?? false)
                throw new NotImplementedException();
        }

        protected virtual bool IsItemEditable(Options<T> item) => item?.IsEditable ?? false;
        public RelayCommand<Options<T>> EditItemCommand { get; set; }
        protected virtual void OnItemEdit(Options<T> item)
        {
            if (item?.IsEditable ?? false)
                throw new NotImplementedException();
        }

        protected virtual bool IsItemDeletable(Options<T> item) => item?.IsDeletable ?? false;
        public RelayCommand<Options<T>> DeleteItemCommand { get; set; }
        protected virtual void OnItemDelete(Options<T> item)
        {
            if (item?.IsDeletable ?? false)
                throw new NotImplementedException();
        }

        protected virtual bool IsItemSelectable(Options<T> item) => item?.IsSelectable ?? false;
        public RelayCommand<Options<T>> SelectItemCommand { get; set; }
        protected virtual void OnItemSelect(Options<T> item)
        {
            if (item?.IsSelectable?? false)
                throw new NotImplementedException();
        }
    }
}
