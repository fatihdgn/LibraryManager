using AutoMapper;
using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Managers;
using Fthdgn.LibraryManager.Repositories;
using Fthdgn.LibraryManager.UI.Extensions;
using Fthdgn.LibraryManager.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    public abstract class DetailedItemsViewModel<T, TItemViewModel, TItemDetailViewModel> : ItemsViewModel<T>
        where T : Entity, new()
        where TItemViewModel : EntityViewModel, new()
        where TItemDetailViewModel : ItemDetailViewModel<TItemViewModel>
    {

        LibraryManagerManagers managers;
        public LibraryManagerManagers Managers { get => managers; set => Set(ref managers, value); }

        LibraryManagerRepository<T> repository;
        public LibraryManagerRepository<T> Repository { get => repository; set => Set(ref repository, value); }

        TItemDetailViewModel detailViewModel;
        public TItemDetailViewModel DetailViewModel
        {
            get => detailViewModel;
            set => Set(ref detailViewModel, value);
        }

        public DetailedItemsViewModel(ViewModelLocator locator, TItemDetailViewModel detailViewModel, LibraryManagerManagers managers) : base(locator)
        {
            DetailViewModel = detailViewModel;
            Managers = managers;
            Repository = Managers.Repositories.Repository<T>();
        }

        protected virtual Options<TItemViewModel> Map(Options<T> options) => options?.MapTo<T, TItemViewModel>();
        protected virtual Options<T> Map(Options<TItemViewModel> options) => options?.MapTo<TItemViewModel, T>();
        protected virtual TItemViewModel Map(T item, TItemViewModel model = null) => model == null ? Mapper.Map<T, TItemViewModel>(item) : Mapper.Map(item, model);
        protected virtual T Map(TItemViewModel item, T model = null) => model == null ? Mapper.Map<TItemViewModel, T>(item) : Mapper.Map(item, model);


        protected override async Task OnItemViewAsync(Options<T> item)
        {
            await base.OnItemViewAsync(item);
            Locator.Main.GoTo(DetailViewModel);
            var result = await DetailViewModel.ViewItemAsync(Map(item));
        }

        protected override async Task OnItemCreateAsync()
        {
            await base.OnItemCreateAsync();
            Locator.Main.GoTo(DetailViewModel);
            var result = await DetailViewModel.CreateItemAsync();
            if (result?.IsChanged ?? false)
            {
                Repository.Add(Map(result.Value, new T()));
                Repository.Save();
                FetchItems();
            }
        }

        protected override async Task OnItemEditAsync(Options<T> item)
        {
            await base.OnItemEditAsync(item);
            Locator.Main.GoTo(DetailViewModel);
            var result = await DetailViewModel.EditItemAsync(Map(item));
            if (result?.IsChanged ?? false)
            {
                Repository.Update(Map(result.Value, item.Value));
                Repository.Save();
                FetchItems();
            }
        }

        protected override async Task OnItemDeleteAsync(Options<T> item)
        {
            await base.OnItemDeleteAsync(item);
            Locator.Main.GoTo(DetailViewModel);
            var result = await DetailViewModel.DeleteItemAsync(Map(item));
            if (result?.IsChanged ?? false)
            {
                Repository.Remove(item.Value);
                Repository.Save();
                FetchItems();
            }
        }
    }
}
