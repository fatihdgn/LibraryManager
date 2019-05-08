using Fthdgn.LibraryManager.UI.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class ItemDetailViewModel<T> : ViewModel
        where T : class, new()
    {
        public ItemDetailViewModel(ViewModelLocator locator) : base(locator)
        {
            YesCommand = new RelayCommand(async () => await YesAsync(), IsYesAvailable, true);
            NoCommand = new RelayCommand(async () => await NoAsync(), IsNoAvailable, true);
        }

        DetailMode mode = DetailMode.View;
        public DetailMode Mode { get => mode; set => Set(ref mode, value); }

        Options<T> item;
        public Options<T> Item
        {
            get => item;
            set => Set(ref item, value);
        }
        
        Decision decision;
        public Decision Decision { get => decision; set => Set(ref decision, value); }

        protected TaskCompletionSource<Options<T>> viewItemDialogCompletionSource;
        public Task<Options<T>> ViewItemAsync(Options<T> item)
        {
            Mode = DetailMode.View;
            Item = item;
            if (viewItemDialogCompletionSource == null)
                viewItemDialogCompletionSource = new TaskCompletionSource<Options<T>>();
            return viewItemDialogCompletionSource.Task;
        }

        protected TaskCompletionSource<Options<T>> createItemDialogCompletionSource;
        public Task<Options<T>> CreateItemAsync()
        {
            Mode = DetailMode.Create;
            Item = new Options<T>(new T(), isViewable: true, isEditable: true, isDeletable: true);
            if (createItemDialogCompletionSource == null)
                createItemDialogCompletionSource = new TaskCompletionSource<Options<T>>();
            return createItemDialogCompletionSource.Task;
        }

        protected TaskCompletionSource<Options<T>> editItemDialogCompletionSource;
        public Task<Options<T>> EditItemAsync(Options<T> item)
        {
            Mode = DetailMode.Edit;
            Item = item;
            if (editItemDialogCompletionSource == null)
                editItemDialogCompletionSource = new TaskCompletionSource<Options<T>>();
            return editItemDialogCompletionSource.Task;
        }

        protected TaskCompletionSource<Options<T>> deleteItemDialogCompletionSource;
        public Task<Options<T>> DeleteItemAsync(Options<T> item)
        {
            Mode = DetailMode.Delete;
            Item = item;
            if (deleteItemDialogCompletionSource == null)
                deleteItemDialogCompletionSource = new TaskCompletionSource<Options<T>>();
            return deleteItemDialogCompletionSource.Task;
        }
        protected virtual bool IsYesAvailable() => true;
        public RelayCommand YesCommand { get; set; }
        protected virtual async Task YesAsync()
        {
            await Task.Yield();
            Decision = Decision.Yes;
            Locator.Main.GoBack();
        }

        protected virtual bool IsNoAvailable() => true;
        public RelayCommand NoCommand { get; set; }
        protected virtual async Task NoAsync()
        {
            await Task.Yield();
            Decision = Decision.No;
            Locator.Main.GoBack();
        }
        public override void OnNavigating()
        {
            base.OnNavigating();
            Decision = Decision.None;
        }

        public override void OnNavigatingAway()
        {
            Item.IsChanged = Decision == Decision.Yes;
            switch (Mode)
            {
                case DetailMode.View:
                    viewItemDialogCompletionSource?.SetResult(Item);
                    break;
                case DetailMode.Create:
                    createItemDialogCompletionSource?.SetResult(Item);
                    break;
                case DetailMode.Edit:
                    editItemDialogCompletionSource?.SetResult(Item);
                    break;
                case DetailMode.Delete:
                    deleteItemDialogCompletionSource?.SetResult(Item);
                    break;
                default:
                    break;
            }
            viewItemDialogCompletionSource = null;
            createItemDialogCompletionSource = null;
            editItemDialogCompletionSource = null;
            deleteItemDialogCompletionSource = null;
        }


    }

    public enum DetailMode
    {
        View,
        Create,
        Edit,
        Delete
    }

    public enum Decision
    {
        None,
        Yes,
        No
    }
}
