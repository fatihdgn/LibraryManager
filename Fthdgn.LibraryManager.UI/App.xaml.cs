using CommonServiceLocator;
using Fthdgn.LibraryManager.Context;
using Fthdgn.LibraryManager.Managers;
using Fthdgn.LibraryManager.Repositories;
using Fthdgn.LibraryManager.UI.Caching;
using Fthdgn.LibraryManager.UI.Mapping;
using Fthdgn.LibraryManager.UI.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Fthdgn.LibraryManager.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            LibraryManagerMapperConfiguration.Initialize();
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register(() => new LibraryManagerDB());
            SimpleIoc.Default.Register<LibraryManagerRepositories>();
            SimpleIoc.Default.Register<LibraryManagerManagers>();
            SimpleIoc.Default.GetInstance<LibraryManagerManagers>().Provide();
            SimpleIoc.Default.Register(() => new CredentialCacheFile("cache.dat"));
            SimpleIoc.Default.Register<ViewModelLocator>(true);
            base.OnStartup(e);
        }
    }
}
