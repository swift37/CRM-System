using FluentValidation;
using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Librarian.Models;
using Librarian.Services;
using Librarian.Services.Interfaces;
using Librarian.Services.Validators;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class EmployeeEditorViewModel : ViewModel
    {
        private readonly IRepository<WorkingRate> _workingRatesRepository;
        private readonly IRepository<Employee> _employeesRepository;
        private readonly IUserDialogService _dialogService;
        private readonly IPasswordHashingService _hashingService;
        private readonly IValidator<RegisterRequest> _registerValidator;
        private CollectionViewSource _workingRatesViewSource;

        #region Properties

        #region WorkingRatesView
        /// <summary>
        /// WorkingRates collection view.
        /// </summary>
        public ICollectionView WorkingRatesView => _workingRatesViewSource.View;
        #endregion

        #region WorkingRatesFilter
        private string? _WorkingRatesFilter;

        /// <summary>
        /// Filter working rates
        /// </summary>
        public string? WorkingRatesFilter
        {
            get => _WorkingRatesFilter;
            set
            {
                if (Set(ref _WorkingRatesFilter, value))
                    _workingRatesViewSource.View.Refresh();
            }
        }
        #endregion

        #region WorkingRates
        private ObservableCollection<WorkingRate>? _WorkingRates;

        /// <summary>
        /// WorkingRates collection
        /// </summary>
        public ObservableCollection<WorkingRate>? WorkingRates
        {
            get => _WorkingRates;
            set
            {
                if (Set(ref _WorkingRates, value))
                    _workingRatesViewSource.Source = value;
                OnPropertyChanged(nameof(WorkingRatesView));
            }
        }
        #endregion

        #region EmployeeId
        /// <summary>
        /// Employee id
        /// </summary>
        public int EmployeeId { get; set; }
        #endregion

        #region EmployeeName
        private string? _EmployeeName;

        /// <summary>
        /// Employee name
        /// </summary>
        public string? EmployeeName { get => _EmployeeName; set => Set(ref _EmployeeName, value); }
        #endregion

        #region EmployeeSurname
        private string? _EmployeeSurname;

        /// <summary>
        /// Employee surname
        /// </summary>
        public string? EmployeeSurname { get => _EmployeeSurname; set => Set(ref _EmployeeSurname, value); }
        #endregion

        #region EmployeeDateOfBirth
        private DateTime _EmployeeDateOfBirth;

        /// <summary>
        /// Employee date of birth
        /// </summary>
        public DateTime EmployeeDateOfBirth { get => _EmployeeDateOfBirth; set => Set(ref _EmployeeDateOfBirth, value); }
        #endregion

        #region EmployeeHireDate
        private DateTime _EmployeeHireDate;

        /// <summary>
        /// Employee hire date
        /// </summary>
        public DateTime EmployeeHireDate { get => _EmployeeHireDate; set => Set(ref _EmployeeHireDate, value); }
        #endregion

        #region EmployeeExtension
        private DateTime? _EmployeeExtension;

        /// <summary>
        /// Employee hire date
        /// </summary>
        public DateTime? EmployeeExtension { get => _EmployeeExtension; set => Set(ref _EmployeeExtension, value); }
        #endregion

        #region EmployeeTitle
        private string? _EmployeeTitle;

        /// <summary>
        /// Employee title
        /// </summary>
        public string? EmployeeTitle { get => _EmployeeTitle; set => Set(ref _EmployeeTitle, value); }
        #endregion

        #region EmployeeContactNumber
        private string? _EmployeeContactNumber;

        /// <summary>
        /// Employee contact number
        /// </summary>
        public string? EmployeeContactNumber { get => _EmployeeContactNumber; set => Set(ref _EmployeeContactNumber, value); }
        #endregion

        #region EmployeeMail
        private string? _EmployeeMail;

        /// <summary>
        /// Employee mail
        /// </summary>
        public string? EmployeeMail { get => _EmployeeMail; set => Set(ref _EmployeeMail, value); }
        #endregion

        #region EmployeeIdentityDocumentNumber
        private string? _EmployeeIdentityDocumentNumber;

        /// <summary>
        /// Employee identity document number
        /// </summary>
        public string? EmployeeIdentityDocumentNumber { get => _EmployeeIdentityDocumentNumber; set => Set(ref _EmployeeIdentityDocumentNumber, value); }
        #endregion

        #region EmployeeWorkingRate
        private WorkingRate? _EmployeeWorkingRate;

        /// <summary>
        /// Employee working rate
        /// </summary>
        public WorkingRate? EmployeeWorkingRate { get => _EmployeeWorkingRate; set => Set(ref _EmployeeWorkingRate, value); }
        #endregion

        #region EmployeeAddress
        private string? _EmployeeAddress;

        /// <summary>
        /// Employee address
        /// </summary>
        public string? EmployeeAddress { get => _EmployeeAddress; set => Set(ref _EmployeeAddress, value); }
        #endregion

        #region EmployeeLogin
        private string? _EmployeeLogin;

        /// <summary>
        /// Employee login
        /// </summary>
        public string? EmployeeLogin 
        {
            get => _EmployeeLogin;
            set
            {
                if (Set(ref _EmployeeLogin, value))
                {
                    RegisterValidate();
                    OnPropertyChanged(nameof(RegisterExeptions));
                }
            }
        }
        #endregion

        #region EmployeePassword
        private string? _EmployeePassword;

        /// <summary>
        /// Employee password
        /// </summary>
        public string? EmployeePassword 
        { 
            get => _EmployeePassword;
            set 
            {
                if (Set(ref _EmployeePassword, value))
                {
                    RegisterValidate();
                    OnPropertyChanged(nameof(RegisterExeptions));
                }  
            }
        }
        #endregion

        #region EmployeePermissionLevel
        private int _EmployeePermissionLevel;

        /// <summary>
        /// Employee permission level
        /// </summary>
        public int EmployeePermissionLevel { get => _EmployeePermissionLevel; set => Set(ref _EmployeePermissionLevel, value); }
        #endregion


        #region RegisterExeptions
        private ObservableCollection<string>? _RegisterExeptions = new();

        /// <summary>
        /// Register exeptions
        /// </summary>
        public ObservableCollection<string>? RegisterExeptions { get => _RegisterExeptions; set => Set(ref _RegisterExeptions, value); }
        #endregion

        #region IsNewEmployee
        /// <summary>
        /// Is new employee?
        /// </summary>
        public bool IsNewEmployee { get; set; } = true;
        #endregion

        #region IsCorrectRegisterData
        public bool _IsCorrectRegisterData;

        /// <summary>
        /// Is correct register data?
        /// </summary>
        public bool IsCorrectRegisterData 
        {
            get 
            {
                if (!IsNewEmployee) return true;
                return _IsCorrectRegisterData;
            }
            set => Set(ref _IsCorrectRegisterData, value);
        }
        #endregion

        #endregion

        #region ChangePasswordCommand
        private ICommand? _ChangePasswordCommand;

        /// <summary>
        /// Change password command
        /// </summary>
        public ICommand? ChangePasswordCommand => _ChangePasswordCommand ??= new LambdaCommand(OnChangePasswordCommandExecuted, CanChangePasswordCommandExecute);

        private bool CanChangePasswordCommandExecute() => true;

        private void OnChangePasswordCommandExecuted()
        {
            if (!_dialogService.ChangePassword(out string? newLogin, out string? newPassword)) return;

            EmployeePassword = _hashingService.Hash(newPassword);
            EmployeeLogin = newLogin;
        }
        #endregion

        public EmployeeEditorViewModel() : this(
            new DebugWorkingRatesRepository(),
            new DebugEmployeesRepository(),
            new UserDialogService(),
            new PasswordHashingService(),
            new RegisterRequestValidator())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            InitProps(new Employee { Id = 1, Name = "John", Surname = "Winston" });
        }

        public EmployeeEditorViewModel(
            IRepository<WorkingRate> workingRatesRepository, 
            IRepository<Employee> employeesRepository, 
            IUserDialogService dialogService,
            IPasswordHashingService hashingService,
            IValidator<RegisterRequest> registerValidator)
        {
            _workingRatesRepository = workingRatesRepository;
            _employeesRepository = employeesRepository;
            _dialogService = dialogService;
            _hashingService = hashingService;
            _registerValidator = registerValidator;

            _workingRatesViewSource = new CollectionViewSource();

            _workingRatesViewSource.Filter += OnWorkingRatesFilter;
        }

        public void InitProps(Employee employee)
        {
            EmployeeId = employee.Id;
            EmployeeName = employee.Name;
            EmployeeSurname = employee.Surname;
            EmployeeDateOfBirth = employee.DateOfBirth;
            EmployeeHireDate = employee.HireDate;
            EmployeeExtension = employee.Extension;
            EmployeeTitle = employee.Title;
            EmployeeContactNumber = employee.ContactNumber;
            EmployeeMail = employee.ContactMail;
            EmployeeIdentityDocumentNumber = employee.IdentityDocumentNumber;
            EmployeeWorkingRate = employee.WorkingRate;
            EmployeeAddress = employee.Address;
            EmployeeLogin = employee.Login;
            EmployeePassword = employee.Password;
            EmployeePermissionLevel = employee.PermissionLevel;
            IsNewEmployee = false;
        }

        public void RegisterValidate()
        {
            if (_employeesRepository.Entities is null)
                throw new ArgumentNullException(nameof(_employeesRepository.Entities));

            IsCorrectRegisterData = false;
            var validation = _registerValidator
                    .Validate(new RegisterRequest { Login = EmployeeLogin, Password = EmployeePassword });

            RegisterExeptions?.ClearAdd(validation.Errors.Select(e => e.ErrorMessage));

            if (!validation.IsValid) return;

            if (_employeesRepository.Entities.Select(e => e.Login).Contains(EmployeeLogin))
            {
                RegisterExeptions?.Clear();
                RegisterExeptions?.Add("The login you entered is already taken.");
                return;
            }

            IsCorrectRegisterData = true;
        }

        private void OnWorkingRatesFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is WorkingRate workingRate) || string.IsNullOrWhiteSpace(WorkingRatesFilter)) return;

            if (workingRate.Name is null || !workingRate.Name.Contains(WorkingRatesFilter))
                e.Accepted = false;
        }
    }
}
