using CommonServiceLocator;
using Fthdgn.LibraryManager.Context;
using Fthdgn.LibraryManager.Managers;
using Fthdgn.LibraryManager.Repositories;
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
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register(() => new LibraryManagerDB());
            SimpleIoc.Default.Register<LibraryManagerRepositories>();
            SimpleIoc.Default.Register<LibraryManagerManagers>();
            SimpleIoc.Default.GetInstance<LibraryManagerManagers>().Provide();
            base.OnStartup(e);
        }
    }
}
