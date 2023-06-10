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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class BuyersViewModel : ViewModel
    {
        private readonly IRepository<Buyer> _buyersRepository;
        private readonly IUserDialogService _dialogService;
        private CollectionViewSource _buyersViewSource;

        #region Properties

        #region BuyersView
        public ICollectionView BuyersView => _buyersViewSource.View;
        #endregion

        #region BuyersCount
        private int _BuyersCount;

        /// <summary>
        /// Buyers count
        /// </summary>
        public int BuyersCount { get => _BuyersCount; set => Set(ref _BuyersCount, value); }
        #endregion

        #region BuyersFilter
        private string? _BuyersFilter;

        /// <summary>
        /// Buyers filter
        /// </summary>
        public string? BuyersFilter
        {
            get => _BuyersFilter;
            set
            {
                if (Set(ref _BuyersFilter, value))
                    _buyersViewSource.View.Refresh();
            }
        }
        #endregion

        #region Buyers
        private ObservableCollection<Buyer>? _Buyers;

        /// <summary>
        /// Buyers collection
        /// </summary>
        public ObservableCollection<Buyer>? Buyers
        {
            get => _Buyers;
            set
            {
                if (Set(ref _Buyers, value))
                    _buyersViewSource.Source = value;
                OnPropertyChanged(nameof(BuyersView));
            }
        }
        #endregion

        #region SelectedBuyer
        private Buyer? _SelectedBuyer;

        /// <summary>
        /// Selected buyer
        /// </summary>
        public Buyer? SelectedBuyer { get => _SelectedBuyer; set => Set(ref _SelectedBuyer, value); }
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
            if (_buyersRepository.Entities is null) return;

            Buyers = (await _buyersRepository.Entities.ToArrayAsync()).ToObservableCollection();

            BuyersCount = await _buyersRepository.Entities.CountAsync();
        }
        #endregion

        #region AddBuyerCommand
        private ICommand? _AddBuyerCommand;

        /// <summary>
        /// Add buyer command
        /// </summary>
        public ICommand? AddBuyerCommand => _AddBuyerCommand ??= new LambdaCommand(OnAddBuyerCommandExecuted, CanAddBuyerCommandExecute);

        private bool CanAddBuyerCommandExecute() => true;

        private void OnAddBuyerCommandExecuted()
        {
            var buyer = new Buyer();

            if (!_dialogService.EditBuyer(buyer)) return;

            _buyersRepository.Add(buyer);
            Buyers?.Add(buyer);

            SelectedBuyer = buyer;
        }
        #endregion

        #region RemoveBuyerCommand
        private ICommand? _RemoveBuyerCommand;

        /// <summary>
        /// RemoveBuyer command
        /// </summary>
        public ICommand? RemoveBuyerCommand => _RemoveBuyerCommand ??= new LambdaCommand<Buyer>(OnRemoveBuyerCommandExecuted, CanRemoveBuyerCommandExecute);

        private bool CanRemoveBuyerCommandExecute(Buyer? buyer) => buyer != null || SelectedBuyer != null;

        private void OnRemoveBuyerCommandExecuted(Buyer? buyer)
        {
            var removableBuyer = buyer ?? SelectedBuyer;
            if (removableBuyer is null) return;

            //todo: Переделать диалог с подтверждением удаления
            if (!_dialogService.Confirmation($"Do you confirm the permanent deletion of the client \"{removableBuyer.Name}\"?", "Client deleting")) return;

            if (_buyersRepository.Entities != null && _buyersRepository.Entities.Any(b => b == buyer || b == SelectedBuyer))
                _buyersRepository.Remove(removableBuyer.Id);


            Buyers?.Remove(removableBuyer);
            if (ReferenceEquals(SelectedBuyer, removableBuyer))
                SelectedBuyer = null;
        }
        #endregion

        #endregion

        public BuyersViewModel() : this(new DebugBuyersRepository(), new UserDialogService())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnLoadDataCommandExecuted();
        }

        public BuyersViewModel(IRepository<Buyer> buyersRepository, IUserDialogService dialogService)
        {
            _buyersRepository = buyersRepository;
            _dialogService = dialogService;

            _buyersViewSource = new CollectionViewSource
            {
                //SortDescriptions =
                //{
                //    new SortDescription(nameof(Buyer.Name), ListSortDirection.Ascending)
                //}
            };

            _buyersViewSource.Filter += OnBuyersFilter;
        }

        private void OnBuyersFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Buyer buyer) || string.IsNullOrWhiteSpace(BuyersFilter)) return;

            if ((!buyer.Name?.Contains(BuyersFilter) ?? false) 
                && (!buyer.ContactNumber?.Contains(BuyersFilter) ?? false) 
                && (!buyer.ContactMail?.Contains(BuyersFilter) ?? false))
                e.Accepted = false;
        }
    }
}
