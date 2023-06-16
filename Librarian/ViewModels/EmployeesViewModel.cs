using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Librarian.Services;
using Librarian.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class EmployeesViewModel : ViewModel
    {
        private readonly IRepository<Employee> _employeesRepository;
        private readonly IRepository<WorkingRate> _workingRatesRepository;
        private readonly IUserDialogService _dialogService;
        private CollectionViewSource _employeesViewSource;

        #region Properties

        #region EmployeesView
        public ICollectionView EmployeesView => _employeesViewSource.View;
        #endregion

        #region EmployeesCount
        private int _EmployeesCount;

        /// <summary>
        /// Employees count
        /// </summary>
        public int EmployeesCount { get => _EmployeesCount; set => Set(ref _EmployeesCount, value); }
        #endregion

        #region Employees
        private ObservableCollection<Employee>? _Employees;

        /// <summary>
        /// Employees collection
        /// </summary>
        public ObservableCollection<Employee>? Employees
        {
            get => _Employees;
            set
            {
                if (Set(ref _Employees, value))
                    _employeesViewSource.Source = value;
                OnPropertyChanged(nameof(EmployeesView));
            }
        }
        #endregion

        #region EmployeesFilter
        private string? _EmployeesFilter;

        /// <summary>
        /// Employees filter 
        /// </summary>
        public string? EmployeesFilter
        {
            get => _EmployeesFilter;
            set
            {
                if (Set(ref _EmployeesFilter, value))
                    _employeesViewSource.View.Refresh();
            }

        }
        #endregion

        #region SelectedEmployee
        private Employee? _SelectedEmployee;

        /// <summary>
        /// Selected employee
        /// </summary>
        public Employee? SelectedEmployee { get => _SelectedEmployee; set => Set(ref _SelectedEmployee, value); }
        #endregion

        #endregion

        #region Commands

        #region LoadDataCommand
        private ICommand? _LoadDataCommand;

        /// <summary>
        /// Load data command
        /// </summary>
        public ICommand? LoadDataCommand => _LoadDataCommand ??= new LambdaCommandAsync(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);

        private bool CanLoadDataCommandExecute() => true;

        private async Task OnLoadDataCommandExecuted()
        {
            if (_employeesRepository.Entities is null) return;

            Employees = (await _employeesRepository.Entities.ToArrayAsync()).ToObservableCollection();

            EmployeesCount = await _employeesRepository.Entities.CountAsync();
        }
        #endregion

        #region AddEmployeeCommand
        private ICommand? _AddEmployeeCommand;

        /// <summary>
        /// Add employee command
        /// </summary>
        public ICommand? AddEmployeeCommand => _AddEmployeeCommand ??= new LambdaCommand(OnAddEmployeeCommandExecuted, CanAddEmployeeCommandExecute);

        private bool CanAddEmployeeCommandExecute() => true;

        private void OnAddEmployeeCommandExecuted()
        {
            var employee = new Employee();

            if (!_dialogService.EditEmployee(employee, _workingRatesRepository)) return;

            _employeesRepository.Add(employee);
            Employees?.Add(employee);

            SelectedEmployee = employee;
        }
        #endregion

        #region EditEmployeeCommand
        private ICommand? _EditEmployeeCommand;

        /// <summary>
        /// Edit employee command 
        /// </summary>
        public ICommand? EditEmployeeCommand => _EditEmployeeCommand ??= new LambdaCommand<Employee>(OnEditEmployeeCommandExecuted, CanEditEmployeeCommandnExecute);

        private bool CanEditEmployeeCommandnExecute(Employee? employee) => employee != null || SelectedEmployee != null;

        private void OnEditEmployeeCommandExecuted(Employee? employee)
        {
            var editableEmployee = employee ?? SelectedEmployee;
            if (editableEmployee is null) return;

            if (!_dialogService.EditEmployee(editableEmployee, _workingRatesRepository))
                return;

            _employeesRepository.Update(editableEmployee);
            _employeesViewSource.View.Refresh();
        }
        #endregion

        #region RemoveEmployeeCommand
        private ICommand? _RemoveEmployeeCommand;

        /// <summary>
        /// Remove employee command
        /// </summary>
        public ICommand? RemoveEmployeeCommand => _RemoveEmployeeCommand
            ??= new LambdaCommand<Employee>(OnRemoveEmployeeCommandExecuted, CanRemoveEmployeeCommandExecute);

        private bool CanRemoveEmployeeCommandExecute(Employee? employee) => employee != null || SelectedEmployee != null;

        private void OnRemoveEmployeeCommandExecuted(Employee? employee)
        {
            var removableEmployee = employee ?? SelectedEmployee;
            if (removableEmployee is null) return;

            //todo: Переделать диалог с подтверждением удаления
            if (!_dialogService.Confirmation(
                $"Do you confirm the permanent deletion of the employee \"{removableEmployee.Name}\"?",
                "Employee deleting")) return;

            if (_employeesRepository.Entities != null
                && _employeesRepository.Entities.Any(e => e == employee || e == SelectedEmployee))
                _employeesRepository.Remove(removableEmployee.Id);


            Employees?.Remove(removableEmployee);
            if (ReferenceEquals(SelectedEmployee, removableEmployee))
                SelectedEmployee = null;
        }
        #endregion

        #endregion

        public EmployeesViewModel() : this(
            new DebugEmployeesRepository(), 
            new DebugWorkingRatesRepository(),
            new UserDialogService())
        {
            if(!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnLoadDataCommandExecuted();
        }

        public EmployeesViewModel(
            IRepository<Employee> sellersRepository,
            IRepository<WorkingRate> workingRatesRepository,
            IUserDialogService dialogService)
        {
            _employeesRepository = sellersRepository;
            _workingRatesRepository = workingRatesRepository;
            _dialogService = dialogService;

            _employeesViewSource = new CollectionViewSource();

            _employeesViewSource.Filter += OnEmployeesFilter;
        }

        private void OnEmployeesFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Employee employee) || string.IsNullOrWhiteSpace(EmployeesFilter)) return;

            if ((!employee.Name?.Contains(EmployeesFilter) ?? true) && 
                (!employee.Surname?.Contains(EmployeesFilter) ?? true) &&
                !employee.DateOfBirth.ToString().Contains(EmployeesFilter) &&
                !employee.HireDate.ToString().Contains(EmployeesFilter) && 
                (!employee.IdentityDocumentNumber?.Contains(EmployeesFilter) ?? true) && 
                (!employee.ContactNumber?.Contains(EmployeesFilter) ?? true))
                e.Accepted = false;
        }
    }
}
