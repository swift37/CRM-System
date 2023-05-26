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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class SellersViewModel : ViewModel
    {
        private readonly IRepository<Seller> _sellersRepository;
        private readonly IUserDialogService _dialogService;
        private CollectionViewSource _sellersViewSource;

        #region Properties

        #region SellersView
        public ICollectionView SellersView => _sellersViewSource.View;
        #endregion

        #region Sellers
        private ObservableCollection<Seller>? _Sellers;

        /// <summary>
        /// Sellers collection
        /// </summary>
        public ObservableCollection<Seller>? Sellers
        {
            get => _Sellers;
            set
            {
                if (Set(ref _Sellers, value))
                    _sellersViewSource.Source = value;
                OnPropertyChanged(nameof(SellersView));
            }
        }
        #endregion

        #region SellersFilter
        private string? _SellersFilter;

        /// <summary>
        /// Filter _sellersRepository by Name, Surname...
        /// </summary>
        public string? SellersFilter
        {
            get => _SellersFilter;
            set
            {
                if (Set(ref _SellersFilter, value))
                    _sellersViewSource.View.Refresh();
            }

        }
        #endregion

        #region SelectedSeller
        private Seller? _SelectedSeller;

        /// <summary>
        /// Selected seller
        /// </summary>
        public Seller? SelectedSeller { get => _SelectedSeller; set => Set(ref _SelectedSeller, value); }
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
            if (_sellersRepository.Entities is null) return;

            Sellers = (await _sellersRepository.Entities.ToArrayAsync()).ToObservableCollection();
        }
        #endregion

        #region AddSellerCommand
        private ICommand? _AddSellerCommand;

        /// <summary>
        /// Add seller command
        /// </summary>
        public ICommand? AddSellerCommand => _AddSellerCommand ??= new LambdaCommand(OnAddSellerCommandExecuted, CanAddSellerCommandExecute);

        private bool CanAddSellerCommandExecute() => true;

        private void OnAddSellerCommandExecuted()
        {
            var seller = new Seller();

            if (!_dialogService.EditSeller(seller)) return;

            _sellersRepository.Add(seller);
            Sellers?.Add(seller);

            SelectedSeller = seller;
        }
        #endregion

        #region RemoveSellerCommand
        private ICommand? _RemoveSellerCommand;

        /// <summary>
        /// Remove seller command
        /// </summary>
        public ICommand? RemoveSellerCommand => _RemoveSellerCommand
            ??= new LambdaCommand<Seller>(OnRemoveSellerCommandExecuted, CanRemoveSellerCommandExecute);

        private bool CanRemoveSellerCommandExecute(Seller? seller) => seller != null || SelectedSeller != null;

        private void OnRemoveSellerCommandExecuted(Seller? seller)
        {
            var removableSeller = seller ?? SelectedSeller;
            if (removableSeller is null) return;

            //todo: Переделать диалог с подтверждением удаления
            if (!_dialogService.Confirmation(
                $"Do you confirm the permanent deletion of the seller \"{removableSeller.Name}\"?",
                "Seller deleting")) return;

            if (_sellersRepository.Entities != null
                && _sellersRepository.Entities.Any(c => c == seller || c == SelectedSeller))
                _sellersRepository.Remove(removableSeller.Id);


            Sellers?.Remove(removableSeller);
            if (ReferenceEquals(SelectedSeller, removableSeller))
                SelectedSeller = null;
        }
        #endregion

        #endregion

        public SellersViewModel() : this(new DebugSellersRepository(), new UserDialogService())
        {
            if(!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnLoadDataCommandExecuted();
        }

        public SellersViewModel(IRepository<Seller> sellersRepository, IUserDialogService dialogService)
        {
            _sellersRepository = sellersRepository;
            _dialogService = dialogService;

            _sellersViewSource = new CollectionViewSource
            {
                SortDescriptions =
                {
                    new SortDescription(nameof(Seller.Name), ListSortDirection.Ascending)
                }
            };

            _sellersViewSource.Filter += OnSellersFilter;
        }

        private void OnSellersFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Seller seller) || string.IsNullOrWhiteSpace(SellersFilter)) return;

            if ((seller.Name is null || !seller.Name.Contains(SellersFilter)) && (seller.Surname is null || !seller.Surname.Contains(SellersFilter)))
                e.Accepted = false;
        }
    }
}
