using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Librarian.Services.Interfaces;
using Librarian.Services;
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
    public class CustomersViewModel : ViewModel
    {
        private readonly IRepository<Customer> _customersRepository;
        private readonly IUserDialogService _dialogService;
        private CollectionViewSource _customersViewSource;

        #region Properties

        #region CustomersView
        public ICollectionView CustomersView => _customersViewSource.View;
        #endregion

        #region CustomersFilter
        private string? _CustomersFilter;

        /// <summary>
        /// Customers filter
        /// </summary>
        public string? CustomersFilter
        {
            get => _CustomersFilter;
            set
            {
                if (Set(ref _CustomersFilter, value))
                    _customersViewSource.View.Refresh();
            }
        }
        #endregion

        #region Customers
        private ObservableCollection<Customer>? _Customers;

        /// <summary>
        /// Customers collection
        /// </summary>
        public ObservableCollection<Customer>? Customers
        {
            get => _Customers;
            set
            {
                if (Set(ref _Customers, value))
                    _customersViewSource.Source = value;
                OnPropertyChanged(nameof(CustomersView));
            }
        }
        #endregion

        #region SelectedCustomer
        private Customer? _SelectedCustomer;

        /// <summary>
        /// Selected Customer
        /// </summary>
        public Customer? SelectedCustomer { get => _SelectedCustomer; set => Set(ref _SelectedCustomer, value); }
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
            if (_customersRepository.Entities is null) return;

            Customers = (await _customersRepository.Entities.ToArrayAsync()).ToObservableCollection();
        }
        #endregion

        #region AddCustomerCommand
        private ICommand? _AddCustomerCommand;

        /// <summary>
        /// Add customer command
        /// </summary>
        public ICommand? AddCustomerCommand => _AddCustomerCommand ??= new LambdaCommand(OnAddCustomerCommandExecuted, CanAddCustomerCommandExecute);

        private bool CanAddCustomerCommandExecute() => true;

        private void OnAddCustomerCommandExecuted()
        {
            var customer = new Customer();

            if (!_dialogService.EditCustomer(customer)) return;

            _customersRepository.Add(customer);
            Customers?.Add(customer);

            SelectedCustomer = customer;
        }
        #endregion

        #region EditCustomerCommand
        private ICommand? _EditCustomerCommand;

        /// <summary>
        /// Edit customer command 
        /// </summary>
        public ICommand? EditCustomerCommand => _EditCustomerCommand ??= new LambdaCommand<Customer>(OnEditBuyerCommandExecuted, CanEditBuyerCommandnExecute);

        private bool CanEditBuyerCommandnExecute(Customer? customer) => customer != null || SelectedCustomer != null;

        private void OnEditBuyerCommandExecuted(Customer? customer)
        {
            var editableCustomer = customer ?? SelectedCustomer;
            if (editableCustomer is null) return;

            if (!_dialogService.EditCustomer(editableCustomer))
                return;

            _customersRepository.Update(editableCustomer);
            _customersViewSource.View.Refresh();
        }
        #endregion

        #region ShowCustomerFullInfoCommand
        private ICommand? _ShowCustomerFullInfoCommand;

        /// <summary>
        /// Show customer full info command 
        /// </summary>
        public ICommand? ShowCustomerFullInfoCommand => _ShowCustomerFullInfoCommand ??= new LambdaCommand<Customer>(OnShowCustomerFullInfoCommandExecuted, CanShowCustomerFullInfoCommandnExecute);

        private bool CanShowCustomerFullInfoCommandnExecute(Customer? customer) => customer != null || SelectedCustomer != null;

        private void OnShowCustomerFullInfoCommandExecuted(Customer? customer)
        {
            var currentCustomer = customer ?? SelectedCustomer;
            if (currentCustomer is null) return;

            _dialogService.ShowFullCustomerInfo(currentCustomer);
        }
        #endregion

        #region RemoveCustomerCommand
        private ICommand? _RemoveCustomerCommand;

        /// <summary>
        /// Remove customer command
        /// </summary>
        public ICommand? RemoveCustomerCommand => _RemoveCustomerCommand ??= new LambdaCommand<Customer>(OnRemoveCustomerCommandExecuted, CanRemoveCustomerCommandExecute);

        private bool CanRemoveCustomerCommandExecute(Customer? customer) => customer != null || SelectedCustomer != null;

        private void OnRemoveCustomerCommandExecuted(Customer? customer)
        {
            var removableCustomer = customer ?? SelectedCustomer;
            if (removableCustomer is null) return;

            //todo: Переделать диалог с подтверждением удаления
            if (!_dialogService.Confirmation(
                $"Do you confirm the permanent deletion of the customer \"{removableCustomer.Name}\"?", 
                "Customer deleting")) return;

            if (_customersRepository.Entities != null && _customersRepository.Entities.Any(c => c == customer || c == SelectedCustomer))
                _customersRepository.Remove(removableCustomer.Id);


            Customers?.Remove(removableCustomer);
            if (ReferenceEquals(SelectedCustomer, removableCustomer))
                SelectedCustomer = null;
        }
        #endregion

        #endregion

        public CustomersViewModel() : this(new DebugCustomersRepository(), new UserDialogService())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnLoadDataCommandExecuted();
        }

        public CustomersViewModel(IRepository<Customer> customerRepository, IUserDialogService dialogService)
        {
            _customersRepository = customerRepository;
            _dialogService = dialogService;

            _customersViewSource = new CollectionViewSource();

            _customersViewSource.Filter += OnBuyersFilter;
        }

        private void OnBuyersFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Customer customer) || string.IsNullOrWhiteSpace(CustomersFilter)) return;

            if ((!customer.Name?.Contains(CustomersFilter) ?? true) 
                && (!customer.ContactNumber?.Contains(CustomersFilter) ?? true) 
                && (!customer.ContactMail?.Contains(CustomersFilter) ?? true))
                e.Accepted = false;
        }
    }
}
