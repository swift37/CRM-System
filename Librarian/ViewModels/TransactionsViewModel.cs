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
        private readonly IRepository<Book> _booksRepository;
        private readonly IRepository<Seller> _sellersRepository;
        private readonly IRepository<Buyer> _buyersRepository;
        private readonly IUserDialogService _dialogService;

        private CollectionViewSource _transactionsViewSource;

        #region Properties

        #region TransactionsView
        /// <summary>
        /// Transactions collection view.
        /// </summary>
        public ICollectionView TransactionsView => _transactionsViewSource.View;
        #endregion

        #region TransactionsCount
        private int _TransactionsCount;

        /// <summary>
        /// Transactions count
        /// </summary>
        public int TransactionsCount { get => _TransactionsCount; set => Set(ref _TransactionsCount, value); }
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

            TransactionsCount = await _transactionsRepository.Entities.CountAsync();
        }
        #endregion

        #region AddTransactionCommand
        private ICommand? _AddTransactionCommand;

        /// <summary>
        /// Add transaction command
        /// </summary>
        public ICommand? AddTransactionCommand => _AddTransactionCommand
            ??= new LambdaCommand(OnAddTransactionCommandExecuted, CanAddTransactionCommandExecute);

        private bool CanAddTransactionCommandExecute() => true;

        private void OnAddTransactionCommandExecuted()
        {
            var transaction = new Transaction();

            if (!_dialogService.EditTransaction(transaction, _booksRepository, _sellersRepository, _buyersRepository)) return;

            _transactionsRepository.Add(transaction);
            Transactions?.Add(transaction);

            SelectedTransaction = transaction;
        }
        #endregion

        #region RemoveTransactionCommand
        private ICommand? _RemoveTransactionCommand;

        /// <summary>
        /// Remove transaction command
        /// </summary>
        public ICommand? RemoveTransactionCommand => _RemoveTransactionCommand
            ??= new LambdaCommand<Transaction>(OnRemoveTransactionCommandExecuted, CanRemoveTransactionCommandExecute);

        private bool CanRemoveTransactionCommandExecute(Transaction? transaction) => transaction != null || SelectedTransaction != null;

        private void OnRemoveTransactionCommandExecuted(Transaction? transaction)
        {
            var removableTransaction = transaction ?? SelectedTransaction;
            if (removableTransaction is null) return;

            //todo: Переделать диалог с подтверждением удаления
            if (!_dialogService.Confirmation(
                $"Do you confirm the permanent deletion of the transaction \"{removableTransaction.Id}\"?",
                "Transaction deleting")) return;

            if (_transactionsRepository.Entities != null
                && _transactionsRepository.Entities.Any(c => c == transaction || c == SelectedTransaction))
                _transactionsRepository.Remove(removableTransaction.Id);


            Transactions?.Remove(removableTransaction);
            if (ReferenceEquals(SelectedTransaction, removableTransaction))
                SelectedTransaction = null;
        }
        #endregion
        
        public TransactionsViewModel() : this(
            new DebugTransactionsRepository(),
            new DebugBooksRepository(),
            new DebugSellersRepository(),
            new DebugBuyersRepository(),
            new UserDialogService())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnLoadDataCommandExecuted();
        }

        public TransactionsViewModel(
            IRepository<Transaction> transactionRepository,
            IRepository<Book> books,
            IRepository<Seller> sellers,
            IRepository<Buyer> buyers,
            IUserDialogService dialogService)
        {
            _transactionsRepository = transactionRepository;
            _booksRepository = books;
            _sellersRepository = sellers;
            _buyersRepository = buyers;
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
