using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Librarian.Services.Interfaces;
using Librarian.Services;
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
using Microsoft.EntityFrameworkCore;

namespace Librarian.ViewModels
{
    public class SuppliesViewModel : ViewModel
    {
        private readonly IRepository<Supply> _suppliesRepository;
        private readonly IRepository<SupplyDetails> _suppliesDetailsRepository;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Supplier> _suppliersRepository;
        private readonly IUserDialogService _dialogService;

        private CollectionViewSource _suppliesViewSource;
        private CollectionViewSource _suppliersViewSource;

        #region Properties

        #region CurrentEmployee
        private Employee? _CurrentEmployee;

        /// <summary>
        /// Current Employee
        /// </summary>
        public Employee? CurrentEmployee { get => _CurrentEmployee; set => Set(ref _CurrentEmployee, value); }
        #endregion

        #region IsEditingAccessible
        /// <summary>
        /// Is editing accessible?
        /// </summary>
        public bool IsEditingAccessible => CurrentEmployee?.PermissionLevel > 2;
        #endregion

        #region IsAddingSuppliersAccessible
        /// <summary>
        /// Is adding entities accessible?
        /// </summary>
        public bool IsAddingSuppliersAccessible => CurrentEmployee?.PermissionLevel > 2;
        #endregion

        #region IsAddingSuppliesAccessible
        /// <summary>
        /// Is adding entities accessible?
        /// </summary>
        public bool IsAddingSuppliesAccessible => CurrentEmployee?.PermissionLevel > 1;
        #endregion

        #region IsSuppliersTabAccessible
        /// <summary>
        /// Is suppliers tab accessible?
        /// </summary>
        public bool IsSuppliersTabAccessible => CurrentEmployee?.PermissionLevel > 1;
        #endregion


        #region SuppliesView
        /// <summary>
        /// Supplies collection view.
        /// </summary>
        public ICollectionView SuppliesView => _suppliesViewSource.View;
        #endregion

        #region SuppliesFilter
        private string? _SuppliesFilter;

        /// <summary>
        /// Supplies filter.
        /// </summary>
        public string? SuppliesFilter
        {
            get => _SuppliesFilter;
            set
            {
                if (Set(ref _SuppliesFilter, value))
                    _suppliesViewSource.View.Refresh();
            }
        }
        #endregion

        #region Supplies
        private ObservableCollection<Supply>? _Supplies;

        /// <summary>
        /// Supplies collection.
        /// </summary>
        public ObservableCollection<Supply>? Supplies
        {
            get => _Supplies;
            set
            {
                if (Set(ref _Supplies, value))
                    _suppliesViewSource.Source = value;
                OnPropertyChanged(nameof(SuppliesView));
            }
        }
        #endregion

        #region SelectedSupply
        private Supply? _SelectedSupply;

        /// <summary>
        /// Selected supply.
        /// </summary>
        public Supply? SelectedSupply { get => _SelectedSupply; set => Set(ref _SelectedSupply, value); }
        #endregion


        #region SuppliersView
        /// <summary>
        /// Suppliers collection view.
        /// </summary>
        public ICollectionView SuppliersView => _suppliersViewSource.View;
        #endregion

        #region SuppliersFilter
        private string? _SuppliersFilter;

        /// <summary>
        /// Suppliers filter.
        /// </summary>
        public string? SuppliersFilter
        {
            get => _SuppliersFilter;
            set
            {
                if (Set(ref _SuppliersFilter, value))
                    _suppliersViewSource.View.Refresh();
            }
        }
        #endregion

        #region Suppliers
        private ObservableCollection<Supplier>? _Suppliers;

        /// <summary>
        /// Suppliers collection.
        /// </summary>
        public ObservableCollection<Supplier>? Suppliers
        {
            get => _Suppliers;
            set
            {
                if (Set(ref _Suppliers, value))
                    _suppliersViewSource.Source = value;
                OnPropertyChanged(nameof(SuppliersView));
            }
        }
        #endregion

        #region SelectedSupplier
        private Supplier? _SelectedSupplier;

        /// <summary>
        /// Selected supplier.
        /// </summary>
        public Supplier? SelectedSupplier { get => _SelectedSupplier; set => Set(ref _SelectedSupplier, value); }
        #endregion

        #endregion

        #region LoadDataCommand
        private ICommand? _LoadDataCommand;

        /// <summary>
        /// Load data command
        /// </summary>
        public ICommand? LoadDataCommand => _LoadDataCommand ??= new LambdaCommandAsync(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);

        private bool CanLoadDataCommandExecute() => true;

        private async Task OnLoadDataCommandExecuted()
        {
            if (_suppliesRepository.Entities is null || _suppliersRepository.Entities is null || _productsRepository.Entities is null)
                return;

            Supplies = (await _suppliesRepository.Entities.Where(o => o.IsActual).ToArrayAsync()).ToObservableCollection();

            Suppliers = (await _suppliersRepository.Entities.Where(o => o.IsActual).ToArrayAsync()).ToObservableCollection();

            _ = await _productsRepository.Entities.ToArrayAsync();
        }
        #endregion

        #region AddSupplyCommand
        private ICommand? _AddSupplyCommand;

        /// <summary>
        /// Add supply command
        /// </summary>
        public ICommand? AddSupplyCommand => _AddSupplyCommand
            ??= new LambdaCommand(OnAddSupplyCommandExecuted, CanAddSupplyCommandExecute);

        private bool CanAddSupplyCommandExecute() => true;

        private void OnAddSupplyCommandExecuted()
        {
            var supply = new Supply();
            supply.SupplyDate = DateTime.Now;
            supply.SupplyDetails = new HashSet<SupplyDetails>();

            if (!_dialogService.EditSupply(supply)) return;

            _suppliesRepository.Add(supply);

            if (supply.SupplyDetails != null)
            {
                _suppliesDetailsRepository.AutoSaveChanges = false;

                foreach (var item in supply.SupplyDetails)
                    _suppliesDetailsRepository.Add(item);

                _suppliesDetailsRepository.SaveChanges();
                _suppliesDetailsRepository.AutoSaveChanges = true;
            }

            Supplies?.Add(supply);

            SelectedSupply = supply;
        }
        #endregion

        #region EditSupplyCommand
        private ICommand? _EditSupplyCommand;

        /// <summary>
        /// Edit supply command 
        /// </summary>
        public ICommand? EditSupplyCommand => _EditSupplyCommand ??= new LambdaCommand<Supply>(OnEditSupplyCommandExecuted, CanEditSupplyCommandnExecute);

        private bool CanEditSupplyCommandnExecute(Supply? supply) => supply != null || SelectedSupply != null;

        private void OnEditSupplyCommandExecuted(Supply? supply)
        {
            var editableSupply = supply ?? SelectedSupply;
            if (editableSupply is null) return;

            var unchangedSupplyDetails = editableSupply.SupplyDetails;

            if (!_dialogService.EditSupply(editableSupply)) return;

            _suppliesRepository.Update(editableSupply);

            _suppliesViewSource.View.Refresh();
        }
        #endregion

        #region ShowSupplyDetailsCommand
        private ICommand? _ShowSupplyDetailsCommand;

        /// <summary>
        /// Show supply details command 
        /// </summary>
        public ICommand? ShowSupplyDetailsCommand => _ShowSupplyDetailsCommand ??= new LambdaCommand<Supply>(OnShowSupplyDetailsCommandExecuted, CanShowSupplyDetailsCommandnExecute);

        private bool CanShowSupplyDetailsCommandnExecute(Supply? supply) => supply != null || SelectedSupply != null;

        private void OnShowSupplyDetailsCommandExecuted(Supply? supply)
        {
            var currentSupply = supply ?? SelectedSupply;
            if (currentSupply is null) return;

            _dialogService.ShowFullSupplyInfo(currentSupply);
        }
        #endregion

        #region ArchiveSupplyCommand
        private ICommand? _ArchiveSupplyCommand;

        /// <summary>
        /// Archive selected supply command 
        /// </summary>
        public ICommand? ArchiveSupplyCommand => _ArchiveSupplyCommand
            ??= new LambdaCommand<Supply>(OnArchiveSupplyCommandExecuted, CanArchiveSupplyCommandnExecute);

        private bool CanArchiveSupplyCommandnExecute(Supply? supply) => supply != null || SelectedSupply != null;

        private void OnArchiveSupplyCommandExecuted(Supply? supply)
        {
            var archivableSupply = supply ?? SelectedSupply;
            if (archivableSupply is null) return;

            if (!_dialogService.Confirmation(
                $"Are you sure you want to archive the supply for {archivableSupply.SupplyDate} ?",
                "Supply archiving")) return;

            if (_suppliesRepository.Entities != null && _suppliesRepository.Entities.Any(o => o == archivableSupply))
                _suppliesRepository.Archive(archivableSupply);

            Supplies?.Remove(archivableSupply);

            if (ReferenceEquals(SelectedSupply, archivableSupply))
                SelectedSupply = null;
        }
        #endregion


        #region AddSupplierCommand
        private ICommand? _AddSupplierCommand;

        /// <summary>
        /// Add supplier command
        /// </summary>
        public ICommand? AddSupplierCommand => _AddSupplierCommand
            ??= new LambdaCommand(OnAddSupplierCommandExecuted, CanAddSupplierCommandExecute);

        private bool CanAddSupplierCommandExecute() => true;

        private void OnAddSupplierCommandExecuted()
        {
            var supplier = new Supplier();

            if (!_dialogService.EditSupplier(supplier)) return;

            _suppliersRepository.Add(supplier);

            Suppliers?.Add(supplier);

            SelectedSupplier = supplier;
        }
        #endregion

        #region EditSupplierCommand
        private ICommand? _EditSupplierCommand;

        /// <summary>
        /// Edit supplier command 
        /// </summary>
        public ICommand? EditSupplierCommand => _EditSupplierCommand ??= new LambdaCommand<Supplier>(OnEditSupplierCommandExecuted, CanEditSupplierCommandnExecute);

        private bool CanEditSupplierCommandnExecute(Supplier? supplier) => supplier != null || SelectedSupplier != null;

        private void OnEditSupplierCommandExecuted(Supplier? supplier)
        {
            var editableSupplier = supplier ?? SelectedSupplier;
            if (editableSupplier is null) return;

            if (!_dialogService.EditSupplier(editableSupplier)) return;

            _suppliersRepository.Update(editableSupplier);

            _suppliersViewSource.View.Refresh();
        }
        #endregion

        #region ShowSupplierFullInfoCommand
        private ICommand? _ShowSupplierFullInfoCommand;

        /// <summary>
        /// Show supplier full info command 
        /// </summary>
        public ICommand? ShowSupplierFullInfoCommand => _ShowSupplierFullInfoCommand ??= new LambdaCommand<Supplier>(OnShowSupplierFullInfoCommandExecuted, CanShowSupplierFullInfoCommandnExecute);

        private bool CanShowSupplierFullInfoCommandnExecute(Supplier? supplier) => supplier != null || SelectedSupplier != null;

        private void OnShowSupplierFullInfoCommandExecuted(Supplier? supplier)
        {
            var currentSupplier = supplier ?? SelectedSupplier;
            if (currentSupplier is null) return;

            _dialogService.ShowFullSupplierInfo(currentSupplier);
        }
        #endregion

        #region ArchiveSupplierCommand
        private ICommand? _ArchiveSupplierCommand;

        /// <summary>
        /// Archive selected supplier command 
        /// </summary>
        public ICommand? ArchiveSupplierCommand => _ArchiveSupplierCommand
            ??= new LambdaCommand<Supplier>(OnArchiveSupplierCommandExecuted, CanArchiveSupplierCommandnExecute);

        private bool CanArchiveSupplierCommandnExecute(Supplier? supplier) => supplier != null || SelectedSupplier != null;

        private void OnArchiveSupplierCommandExecuted(Supplier? supplier)
        {
            var archivableSupplier = supplier ?? SelectedSupplier;
            if (archivableSupplier is null) return;

            if (!_dialogService.Confirmation(
                $"Do you confirm the permanent deletion of the supplier \"{archivableSupplier.Name}\"?",
                "Supplier deleting")) return;

            if (_suppliersRepository.Entities != null && _suppliersRepository.Entities.Any(s => s == archivableSupplier))
                _suppliersRepository.Archive(archivableSupplier);

            Suppliers?.Remove(archivableSupplier);

            if (ReferenceEquals(SelectedSupplier, archivableSupplier))
                SelectedSupplier = null;
        }
        #endregion

        public SuppliesViewModel() : this(
            new DebugSuppliesRepository(),
            new DebugSuppliesDetailsRepository(),
            new DebugProductsRepository(),
            new DebugSuppliersRepository(),
            new UserDialogService())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnLoadDataCommandExecuted();
        }

        public SuppliesViewModel(
            IRepository<Supply> suppliesRepository,
            IRepository<SupplyDetails> suppliesDetailsRepository,
            IRepository<Product> products,
            IRepository<Supplier> suppliers,
            IUserDialogService dialogService)
        {
            _suppliesRepository = suppliesRepository;
            _suppliesDetailsRepository = suppliesDetailsRepository;
            _productsRepository = products;
            _suppliersRepository = suppliers;
            _dialogService = dialogService;

            _suppliesViewSource = new CollectionViewSource();
            _suppliersViewSource = new CollectionViewSource();

            _suppliesViewSource.Filter += OnSuppliesFilter;
            _suppliersViewSource.Filter += OnSuppliersFilter;
        }

        private void OnSuppliesFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Supply supply) || string.IsNullOrWhiteSpace(SuppliesFilter)) return;

            var supplyDate = supply.SupplyDate.ToString();

            if ((!supplyDate.Contains(SuppliesFilter)) &&
                (!supply.SupplyDetails?.Any(d => d.Product?.Name?.Contains(SuppliesFilter) ?? false) ?? true) &&
                !supply.SupplyCost.ToString().Contains(SuppliesFilter) &&
                (!supply.Supplier?.ContactNumber?.Contains(SuppliesFilter) ?? true))
                e.Accepted = false;
        }

        private void OnSuppliersFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Supplier supplier) || string.IsNullOrWhiteSpace(SuppliersFilter)) return;

            if ((!supplier.Name?.Contains(SuppliersFilter) ?? true) &&
                (!supplier.ContactTitle?.Contains(SuppliersFilter) ?? true) &&
                (!supplier.ContactNumber?.Contains(SuppliersFilter) ?? true))
                e.Accepted = false;
        }
    }
}
