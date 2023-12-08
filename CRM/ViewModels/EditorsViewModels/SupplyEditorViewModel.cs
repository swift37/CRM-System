using CRM.DAL.Entities;
using CRM.Infrastructure.DebugServices;
using CRM.Interfaces;
using CRM.Services.Interfaces;
using CRM.Services;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace CRM.ViewModels
{
    public class SupplyEditorViewModel : ViewModel
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Supplier> _suppliersRepository;
        private readonly IUserDialogService _dialogService;

        private CollectionViewSource _suppliersViewSource;

        #region Properties

        #region CurrentSupply
        private Supply? _CurrentSupply;

        /// <summary>
        /// Current Supply
        /// </summary>
        public Supply? CurrentSupply { get => _CurrentSupply; set => Set(ref _CurrentSupply, value); }
        #endregion

        #region SelectedSupplyDetails
        private SupplyDetails? _SelectedSupplyDetails;

        /// <summary>
        /// Selected supply details
        /// </summary>
        public SupplyDetails? SelectedSupplyDetails { get => _SelectedSupplyDetails; set => Set(ref _SelectedSupplyDetails, value); }
        #endregion

        #region SupplyId
        /// <summary>
        /// Supply id
        /// </summary>
        public int SupplyId { get; set; }
        #endregion

        #region SupplyDate
        private DateTime _SupplyDate;

        /// <summary>
        /// Supply date
        /// </summary>
        public DateTime SupplyDate { get => _SupplyDate; set => Set(ref _SupplyDate, value); }
        #endregion

        #region SupplyCost
        private decimal _SupplyCost;

        /// <summary>
        /// Supply cost
        /// </summary>
        public decimal SupplyCost { get => _SupplyCost; set => Set(ref _SupplyCost, value); }
        #endregion

        #region SupplyProductsQuantity
        private int _SupplyProductsQuantity;

        /// <summary>
        /// Supply products quantity
        /// </summary>
        public int SupplyProductsQuantity { get => _SupplyProductsQuantity; set => Set(ref _SupplyProductsQuantity, value); }
        #endregion

        #region SupplySupplier
        private Supplier? _SupplySupplier;

        /// <summary>
        /// Supplier
        /// </summary>
        public Supplier? SupplySupplier { get => _SupplySupplier; set => Set(ref _SupplySupplier, value); }
        #endregion

        #region SupplyDetails
        private ObservableCollection<SupplyDetails>? _SupplyDetails;

        /// <summary>
        /// Supply details collection 
        /// </summary>
        public ObservableCollection<SupplyDetails>? SupplyDetails { get => _SupplyDetails; set => Set(ref _SupplyDetails, value); }
        #endregion


        #region SuppliersView
        /// <summary>
        /// Suppliers collection view.
        /// </summary>
        public ICollectionView SuppliersView => _suppliersViewSource.View;
        #endregion 

        #region Suppliers
        private IEnumerable<Supplier>? _Suppliers;

        /// <summary>
        /// Suppliers collection
        /// </summary>
        public IEnumerable<Supplier>? Suppliers
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

        #region SuppliersFilter
        private string? _SuppliersFilter;

        /// <summary>
        /// Suppliers filter
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

        #endregion

        #region LoadRepositoriesCommand
        private ICommand? _LoadRepositoriesCommand;

        /// <summary>
        /// Load repositories command 
        /// </summary>
        public ICommand? LoadRepositoriesCommand => _LoadRepositoriesCommand ??= new LambdaCommandAsync(OnLoadRepositoriesCommandExecuted, CanLoadRepositoriesCommandExecute);

        private bool CanLoadRepositoriesCommandExecute() => true;

        private async Task OnLoadRepositoriesCommandExecuted()
        {
            if (_suppliersRepository.Entities is null)
                throw new ArgumentNullException("Suppliers list is empty or failed to load", nameof(_suppliersRepository.Entities));
            if (_productsRepository.Entities is null)
                throw new ArgumentNullException("Products list is empty or failed to load", nameof(_productsRepository.Entities));

            Suppliers = await _suppliersRepository.Entities.ToArrayAsync();
            _ = await _productsRepository.Entities.ToArrayAsync();
        }
        #endregion

        #region AddSupplyDetailsCommand
        private ICommand? _AddSupplyDetailsCommand;

        /// <summary>
        /// Add supply details command
        /// </summary>
        public ICommand? AddSupplyDetailsCommand => _AddSupplyDetailsCommand
            ??= new LambdaCommand(OnAddSupplyDetailsCommandExecuted, CanAddSupplyDetailsCommandExecute);

        private bool CanAddSupplyDetailsCommandExecute() => true;

        private void OnAddSupplyDetailsCommandExecuted()
        {
            var supplyDetails = new SupplyDetails();
            supplyDetails.Supply = CurrentSupply;

            if (!_dialogService.EditSupplyDetails(supplyDetails)) return;

            SupplyDetails?.Add(supplyDetails);

            SupplyCost += supplyDetails.Quantity * supplyDetails.UnitPrice;
            SupplyProductsQuantity += supplyDetails.Quantity;
        }
        #endregion

        #region RemoveSupplyDetailsCommand
        private ICommand? _RemoveSupplyDetailsCommand;

        /// <summary>
        /// Remove supply details command
        /// </summary>
        public ICommand? RemoveSupplyDetailsCommand => _RemoveSupplyDetailsCommand
            ??= new LambdaCommand<SupplyDetails>(OnRemoveSupplyDetailsCommandExecuted, CanRemoveSupplyDetailsCommandExecute);

        private bool CanRemoveSupplyDetailsCommandExecute(SupplyDetails? supplyDetails) => true;

        private void OnRemoveSupplyDetailsCommandExecuted(SupplyDetails? supplyDetails)
        {
            var removableSupplyDetails = supplyDetails ?? SelectedSupplyDetails;
            if (removableSupplyDetails is null) return;

            if (SupplyDetails != null && SupplyDetails.Any(d => d == removableSupplyDetails))
                SupplyDetails.Remove(removableSupplyDetails);

            SupplyCost -= removableSupplyDetails.Quantity * removableSupplyDetails.UnitPrice;
            SupplyProductsQuantity -= removableSupplyDetails.Quantity;

            if (ReferenceEquals(SelectedSupplyDetails, removableSupplyDetails))
                SelectedSupplyDetails = null;
        }
        #endregion

        public SupplyEditorViewModel() : this(
            new DebugProductsRepository(),
            new DebugSuppliersRepository(),
            new UserDialogService())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            InitProps(new Supply { Id = 1, SupplyDate = DateTime.Now });
        }

        public SupplyEditorViewModel(
            IRepository<Product> products,
            IRepository<Supplier> suppliers,
            IUserDialogService userDialogService)
        {
            _productsRepository = products;
            _suppliersRepository = suppliers;
            _dialogService = userDialogService;

            _suppliersViewSource = new CollectionViewSource();

            _suppliersViewSource.Filter += OnSuppliersFilter;
        }

        public void InitProps(Supply supply)
        {
            CurrentSupply = supply;
            SupplyId = supply.Id;
            SupplyDate = supply.SupplyDate;
            SupplyCost = supply.SupplyCost;
            SupplyProductsQuantity = supply.ProductsQuantity;
            SupplySupplier = supply.Supplier;
            SupplyDetails = supply.SupplyDetails?.ToObservableCollection();
        }

        private void OnSuppliersFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Supplier supplier) || string.IsNullOrWhiteSpace(SuppliersFilter)) return;

            if ((!supplier.Name?.Contains(SuppliersFilter) ?? true) && 
                (!supplier.ContactNumber?.Contains(SuppliersFilter) ?? true))
                e.Accepted = false;
        }
    }
}
