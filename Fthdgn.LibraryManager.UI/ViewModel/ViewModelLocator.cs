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
using Fthdgn.LibraryManager.UI.Caching;
using Fthdgn.LibraryManager.UI.Extensions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;

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
            Register<MainViewModel>();
            Register<LoginViewModel>();
            Register<UserLibrariesViewModel>();
            Register<HomeViewModel>();
            Register<BooksViewModel>();
            Register<BookDetailViewModel>();

            Register<AuthorsViewModel>();
            Register<AuthorDetailViewModel>();

            Register<LibrariesViewModel>();
            Register<LibraryDetailViewModel>();

            Register<UsersViewModel>();
            Register<UserDetailViewModel>();

            Register<LoanCreateViewModel>();
            Register<LoanOnBehalfViewModel>();

            Register<LoanReturnViewModel>();
            Register<LoanReturnDetailViewModel>();

            Register<LoanReturnOnBehalfViewModel>();
        }

        void Register<T>(Func<T> factory = null) where T : class
        {
            if (factory != null) SimpleIoc.Default.Register<T>(factory);
            else SimpleIoc.Default.Register<T>();
        }
        T Get<T>() => ServiceLocator.Current.GetInstance<T>();

        public MainViewModel Main => Get<MainViewModel>();
        public LoginViewModel Login => Get<LoginViewModel>();
        public UserLibrariesViewModel UserLibraries => Get<UserLibrariesViewModel>();
        public HomeViewModel Home => Get<HomeViewModel>();

        public BooksViewModel Books => Get<BooksViewModel>();
        public BookDetailViewModel BookDetail => Get<BookDetailViewModel>();

        public AuthorsViewModel Authors => Get<AuthorsViewModel>();
        public AuthorDetailViewModel AuthorDetail => Get<AuthorDetailViewModel>();

        public LibrariesViewModel Libraries => Get<LibrariesViewModel>();
        public LibraryDetailViewModel LibraryDetail => Get<LibraryDetailViewModel>();

        public UsersViewModel Users => Get<UsersViewModel>();
        public UserDetailViewModel UserDetail => Get<UserDetailViewModel>();

        public LoanCreateViewModel LoanCreate => Get<LoanCreateViewModel>();
        public LoanOnBehalfViewModel LoanOnBehalf => Get<LoanOnBehalfViewModel>();

        public LoanReturnViewModel LoanReturn => Get<LoanReturnViewModel>();
        public LoanReturnDetailViewModel LoanReturnDetail => Get<LoanReturnDetailViewModel>();

        public LoanReturnOnBehalfViewModel LoanReturnOnBehalf => Get<LoanReturnOnBehalfViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}