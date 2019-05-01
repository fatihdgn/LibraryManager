using Fthdgn.LibraryManager.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public class ItemDetailViewModel<T> : ViewModel
    {
        public ItemDetailViewModel(ViewModelLocator locator) : base(locator)
        {

        }

        DetailMode mode = DetailMode.View;
        public DetailMode Mode { get => mode; set => Set(ref mode, value); } 

        Options<T> item;
        public Options<T> Item { get => item; set => Set(ref item, value); }

        protected TaskCompletionSource<Options<T>> viewItemDialogCompletionSource;
        public Task<Options<T>> ViewItemAsync(Options<T> item)
        {
            Item = item;
            if (viewItemDialogCompletionSource == null)
                viewItemDialogCompletionSource = new TaskCompletionSource<Options<T>>();
            return viewItemDialogCompletionSource.Task;
        }

        public override void OnNavigatingAway()
        {
            viewItemDialogCompletionSource?.SetResult(Item);
            viewItemDialogCompletionSource = null;
            //editItemDialogCompletionSource?.SetResult(null);
            //deleteItemDialogCompletionSource?.SetResult(null);
            //selectItemDialogCompletionSource?.SetResult(null);
        }

    }

    public enum DetailMode
    {
        View,
        Create,
        Edit,
        Delete
    }
}
