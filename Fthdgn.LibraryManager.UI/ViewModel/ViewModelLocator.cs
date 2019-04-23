/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Fthdgn.LibraryManager.UI"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using Fthdgn.LibraryManager.Context;
using Fthdgn.LibraryManager.UI.Extensions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            Reregister<ViewModelLocator>();
            Register<MainViewModel>();
            Register<LoginViewModel>();
            Register<LibrariesViewModel>();
            Register<HomeViewModel>();
        }

        void Reregister<T>() where T : class => SimpleIoc.Default.Reregister<T>();
        void Register<T>() where T : class => SimpleIoc.Default.Register<T>();
        T Get<T>() => ServiceLocator.Current.GetInstance<T>();

        public MainViewModel Main => Get<MainViewModel>();
        public LoginViewModel Login => Get<LoginViewModel>();
        public LibrariesViewModel Libraries => Get<LibrariesViewModel>();
        public HomeViewModel Home => Get<HomeViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}