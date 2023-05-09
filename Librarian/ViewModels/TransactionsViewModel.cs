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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class TransactionsViewModel : ViewModel
    {
        private readonly IRepository<Transaction> _transactionsRepository;
        private readonly IUserDialogService _dialogService;

        private CollectionViewSource _transactionsViewSource;

        #region Properties

        #region TransactionsView
        /// <summary>
        /// Transactions collection view.
        /// </summary>
        public ICollectionView TransactionsView => _transactionsViewSource.View;
        #endregion

        #region TransactionsFilter
        private string? _TransactionsFilter;

        /// <summary>
        /// Transactions filter.
        /// </summary>
        public string? TransactionsFilter
        {
            get => _TransactionsFilter;
            set
            {
                if (Set(ref _TransactionsFilter, value))
                    _transactionsViewSource.View.Refresh();
            }
        }
        #endregion

        #region Transactions
        private ObservableCollection<Transaction>? _Transactions;

        /// <summary>
        /// Transactions collection.
        /// </summary>
        public ObservableCollection<Transaction>? Transactions
        {
            get => _Transactions;
            set
            {
                if (Set(ref _Transactions, value))
                    _transactionsViewSource.Source = value;
                OnPropertyChanged(nameof(TransactionsView));
            }
        }
        #endregion

        #region SelectedTransaction
        private Transaction? _SelectedTransaction;

        /// <summary>
        /// Selected transaction.
        /// </summary>
        public Transaction? SelectedTransaction { get => _SelectedTransaction; set => Set(ref _SelectedTransaction, value); }
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
            if (_transactionsRepository.Entities is null) return;

            Transactions = (await _transactionsRepository.Entities.ToArrayAsync()).ToObservableCollection();
        }
        #endregion

        public TransactionsViewModel() : this(new DebugTransactionsRepository(), new UserDialogService())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnLoadDataCommandExecuted();
        }

        public TransactionsViewModel(IRepository<Transaction> transactionRepository, IUserDialogService dialogService)
        {
            _transactionsRepository = transactionRepository;
            _dialogService = dialogService;

            _transactionsViewSource = new CollectionViewSource();

            _transactionsViewSource.Filter += OnTransactionsFilter;
        }

        private void OnTransactionsFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Transaction transaction) || string.IsNullOrWhiteSpace(TransactionsFilter)) return;

            var transactionDate = transaction.TransactionDate.ToString();

            if ((transactionDate is null || !transactionDate.Contains(TransactionsFilter)) &&
                (transaction.Book is null || transaction.Book.Name is null || !transaction.Book.Name.Contains(TransactionsFilter)) &&
                !transaction.Amount.ToString().Contains(TransactionsFilter) &&
                (transaction.Buyer is null || transaction.Buyer.ContactNumber is null || !transaction.Buyer.ContactNumber.Contains(TransactionsFilter)) &&
                (transaction.Seller is null || transaction.Seller.Name is null || transaction.Seller.Surname is null || 
                (!transaction.Seller.Name.Contains(TransactionsFilter) && !transaction.Seller.Surname.Contains(TransactionsFilter))))
                    e.Accepted = false;
        }
    }
}
