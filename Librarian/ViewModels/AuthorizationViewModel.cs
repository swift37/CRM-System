using Librarian.DAL.Entities;
using Librarian.Models;
using Librarian.Services;
using Librarian.Services.Interfaces;
using Librarian.ViewModels.Base;
using Swftx.Wpf.Commands;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class AuthorizationViewModel : DialogViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserDialogService _dialogService;

        #region Properties

        #region Password
        private string? _Password;

        /// <summary>
        /// User password
        /// </summary>
        public string? Password { get => _Password; set => Set(ref _Password, value); }
        #endregion

        #region Login
        private string? _Login;

        /// <summary>
        /// User password
        /// </summary>
        public string? Login { get => _Login; set => Set(ref _Login, value); }
        #endregion

        #region AuthExeptions
        private ObservableCollection<string>? _AuthExeptions = new ObservableCollection<string>();

        /// <summary>
        /// Authorization Exeptions
        /// </summary>
        public ObservableCollection<string>? AuthExeptions { get => _AuthExeptions; set => Set(ref _AuthExeptions, value); }
        #endregion

        #endregion

        #region SignInCommand
        private ICommand? _SignInCommand;

        /// <summary>
        /// Sign In command
        /// </summary>
        public ICommand? SignInCommand => _SignInCommand ??= new LambdaCommandAsync(OnSignInCommandExecuted, CanSignInCommandnExecute);

        private bool CanSignInCommandnExecute() => 
            !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);

        private async Task OnSignInCommandExecuted()
        {
            var loginRequest = new LoginRequest { Login = Login, Password = Password };

            //var registerRequest = new RegisterRequest { Login = Login, Password = Password };
            //var employee = await _authorizationService.RegisterAsync(registerRequest);

            Employee? employee;

            try
            {
                employee = await _authorizationService.LoginAsync(loginRequest);
            }
            catch (Exception e)
            {
                AuthExeptions?.Clear();
                AuthExeptions?.Add(e.Message);
                return;
            }

            _dialogService.OpenMainWindow(employee);
            OnDialogComplete(EventArgs.Empty);
        }
        #endregion

        public AuthorizationViewModel() : this(
            new AuthorizationService(), 
            new UserDialogService())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }


        public AuthorizationViewModel(
            IAuthorizationService authorizationService, 
            IUserDialogService dialogService)
        {
            _authorizationService = authorizationService;
            _dialogService = dialogService;
        }
    }
}
