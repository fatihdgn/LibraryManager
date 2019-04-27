using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.UI.Extensions;
using Fthdgn.LibraryManager.UI.Models;
using Fthdgn.LibraryManager.UI.Pages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using MahApps.Metro.Controls.Dialogs;

namespace Fthdgn.LibraryManager.UI.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModel
    {
        User user;
        public User User { get => user; set { Set(ref user, value, broadcast: true); RaisePropertyChanged(nameof(CanShowUserInfo)); } }
        public bool CanShowUserInfo => User != null;

        Library library;
        public Library Library { get => library; set { Set(ref library, value, broadcast: true); RaisePropertyChanged(nameof(CanShowLibraryInfo)); } }
        public bool CanShowLibraryInfo => Library != null;

        Scopes scopes;
        public Scopes Scopes
        {
            get => scopes;
            set => Set(ref scopes, value, broadcast: true);
        }


        public IDialogCoordinator Dialog { get; set; }
        public MainViewModel(ViewModelLocator locator) : base(locator)
        {
            Dialog = DialogCoordinator.Instance;

            SimpleIoc.Default.Reregister<Login>();
            SimpleIoc.Default.Reregister<UserLibraries>();
            SimpleIoc.Default.Reregister<Home>();

            GoBackCommand = new RelayCommand(GoBack, CanGoBack);
            GoToCommand = new RelayCommand<string>(GoTo);
            GoTo<Login>();
        }

        Page currentPage;
        public Page CurrentPage { get => currentPage; set => Set(ref currentPage, value); }

        Stack<Page> pageStack = new Stack<Page>();
        public RelayCommand<string> GoToCommand { get; set; }

        public void GoTo<TPage>(bool removeHistory = false) where TPage : Page
        {
            if (CurrentPage != null) pageStack.Push(CurrentPage);
            if (removeHistory) pageStack.Clear();
            CurrentPage = SimpleIoc.Default.GetInstance<TPage>();
            GoBackCommand.RaiseCanExecuteChanged();
        }
        public void GoTo(string page)
        {
            switch (page)
            {
                case nameof(Login): GoTo<Login>(); break;
                case nameof(UserLibraries): GoTo<UserLibraries>(); break;
                case nameof(Home): GoTo<Home>(); break;
                default:
                    break;
            }
        }
        public RelayCommand GoBackCommand { get; protected set; }
        bool CanGoBack() => pageStack.Any();
        void GoBack()
        {
            CurrentPage = pageStack.Pop();
            GoBackCommand.RaiseCanExecuteChanged();
        }
    }
}