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

        public void ResetUser()
        {
            user = null;
            RaisePropertyChanged(nameof(User));
            RaisePropertyChanged(nameof(CanShowUserInfo));
        }

        Library library;
        public Library Library { get => library; set { Set(ref library, value, broadcast: true); RaisePropertyChanged(nameof(CanShowLibraryInfo)); } }
        public bool CanShowLibraryInfo => Library != null;

        public string Title => $"{DisplayName} | {(CurrentPage?.DataContext as ViewModel)?.DisplayName}";

        Scopes scopes;
        public Scopes Scopes
        {
            get => scopes;
            set => Set(ref scopes, value, broadcast: true);
        }


        public IDialogCoordinator Dialog { get; set; }
        public MainViewModel(ViewModelLocator locator) : base(locator)
        {
            Name = nameof(MainViewModel);
            DisplayName = "Kütüphane Yönetimi";
            PropertyChanged += (_, e) => { if (e.PropertyName == nameof(DisplayName)) RaisePropertyChanged(nameof(Title)); };
            Dialog = DialogCoordinator.Instance;

            SimpleIoc.Default.Reregister<Login>();
            SimpleIoc.Default.Reregister<UserLibraries>();
            SimpleIoc.Default.Reregister<Home>();

            SimpleIoc.Default.Reregister<Books>();
            SimpleIoc.Default.Reregister<BookDetail>();

            SimpleIoc.Default.Reregister<Authors>();
            SimpleIoc.Default.Reregister<AuthorDetail>();

            SimpleIoc.Default.Reregister<Libraries>();
            SimpleIoc.Default.Reregister<LibraryDetail>();

            SimpleIoc.Default.Reregister<Users>();
            SimpleIoc.Default.Reregister<UserDetail>();

            GoBackCommand = new RelayCommand(GoBack, CanGoBack);
            GoToCommand = new RelayCommand<string>(GoTo);
            GoTo<Login>();
        }

        Page currentPage;
        public Page CurrentPage { get => currentPage; set { Set(ref currentPage, value); RaisePropertyChanged(nameof(Title)); } }

        Stack<Page> pageStack = new Stack<Page>();
        public RelayCommand<string> GoToCommand { get; set; }

        public void GoTo<TPage>(bool removeHistory = false) where TPage : Page
        {
            if (CurrentPage != null) pageStack.Push(CurrentPage);
            if (removeHistory) pageStack.Clear();
            var pPre = CurrentPage;
            var vmPre = pPre?.DataContext as ViewModel;
            var pCurrent = SimpleIoc.Default.GetInstance<TPage>();
            var vmCurrent = pCurrent?.DataContext as ViewModel;
            vmCurrent?.OnNavigating();
            vmPre?.OnNavigatingAway(vmCurrent?.Name);
            CurrentPage = pCurrent;
            vmCurrent?.OnNavigated();
            vmPre?.OnNavigatedAway(vmCurrent?.Name);
            GoBackCommand.RaiseCanExecuteChanged();
        }
        public void GoTo(string page)
        {
            switch (page)
            {
                case nameof(Login): GoTo<Login>(); break;
                case nameof(UserLibraries): GoTo<UserLibraries>(); break;
                case nameof(Home): GoTo<Home>(); break;

                case nameof(Books): GoTo<Books>(); break;
                case nameof(BookDetail): GoTo<BookDetail>(); break;

                case nameof(Authors): GoTo<Authors>(); break;
                case nameof(AuthorDetail): GoTo<AuthorDetail>(); break;

                case nameof(Libraries): GoTo<Libraries>(); break;
                case nameof(LibraryDetail): GoTo<LibraryDetail>(); break;

                case nameof(Users): GoTo<Users>(); break;
                case nameof(UserDetail): GoTo<UserDetail>(); break;
                default:
                    break;
            }
        }

        public void GoTo(ViewModel viewModel) => GoTo(viewModel?.Name);
        public RelayCommand GoBackCommand { get; protected set; }
        bool CanGoBack() => pageStack.Any();
        public void GoBack()
        {
            var pPre = CurrentPage;
            var vmPre = pPre?.DataContext as ViewModel;
            var pCurrent = pageStack.Pop();
            var vmCurrent = pCurrent?.DataContext as ViewModel;
            vmCurrent?.OnNavigating();
            vmPre?.OnNavigatingAway(vmCurrent?.Name);
            CurrentPage = pCurrent;
            vmCurrent?.OnNavigated();
            vmPre?.OnNavigatedAway(vmCurrent?.Name);
            GoBackCommand.RaiseCanExecuteChanged();
        }
    }
}