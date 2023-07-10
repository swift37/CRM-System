using FluentValidation;
using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Librarian.Models;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Librarian.ViewModels
{
    public class PrivateSecurityChangeViewModel : ViewModel
    {
        private readonly IValidator<RegisterRequest> _registerValidator;
        private readonly IRepository<Employee> _employeesRepository;

        #region Login
        private string? _Login;

        /// <summary>
        /// Login
        /// </summary>
        public string? Login
        {
            get => _Login;
            set
            {
                if (Set(ref _Login, value))
                {
                    RegisterValidate();
                    OnPropertyChanged(nameof(RegisterExeptions));
                }
            }
        }
        #endregion

        #region Password
        private string? _Password;

        /// <summary>
        /// Password
        /// </summary>
        public string? Password
        {
            get => _Password;
            set
            {
                if (Set(ref _Password, value))
                {
                    RegisterValidate();
                    OnPropertyChanged(nameof(RegisterExeptions));
                }
            }
        }
        #endregion

        #region RegisterExeptions
        private ObservableCollection<string>? _RegisterExeptions = new();

        /// <summary>
        /// Register exeptions
        /// </summary>
        public ObservableCollection<string>? RegisterExeptions { get => _RegisterExeptions; set => Set(ref _RegisterExeptions, value); }
        #endregion

        #region IsCorrectRegisterData
        public bool _IsCorrectRegisterData;

        /// <summary>
        /// Is correct register data?
        /// </summary>
        public bool IsCorrectRegisterData { get => _IsCorrectRegisterData; set => Set(ref _IsCorrectRegisterData, value); }
        #endregion

        public PrivateSecurityChangeViewModel(IValidator<RegisterRequest> registerValidator, IRepository<Employee> employeesRepository) 
        {
            if (App.IsDesignMode)
               InitProps("login", "password");

            _registerValidator = registerValidator;
            _employeesRepository = employeesRepository;
        }

        public void InitProps(string? login, string? password)
        {
            Login = login;
            Password = password;
        }

        public void RegisterValidate()
        {
            if (_employeesRepository.Entities is null)
                throw new ArgumentNullException(nameof(_employeesRepository.Entities));

            IsCorrectRegisterData = false;
            var validation = _registerValidator
                    .Validate(new RegisterRequest { Login = Login, Password = Password });

            RegisterExeptions?.ClearAdd(validation.Errors.Select(e => e.ErrorMessage));

            if (!validation.IsValid) return;

            if (_employeesRepository.Entities.Select(e => e.Login).Contains(Login))
            {
                RegisterExeptions?.Clear();
                RegisterExeptions?.Add("The login you entered is already taken.");
                return;
            }

            IsCorrectRegisterData = true;
        }
    }
}
