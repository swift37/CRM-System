using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Librarian.Services.Interfaces;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly ITradingService _tradingService;
        private readonly IUserDialogService _dialogService;
        private readonly IRepository<Book> _booksRepository;
        private readonly IRepository<Seller> _sellersRepository;
        private readonly IRepository<Buyer> _buyersRepository;
        private readonly IRepository<Transaction> _transactionsRepository;

        #region Properties

        #region Tilte
        private string? _Title = "Librarian";

        /// <summary>
        /// Window title
        /// </summary>
        public string? Title { get => _Title; set => Set(ref _Title, value); }
        #endregion

        #region CurrentViewModel
        private ViewModel? _CurrentViewModel;

        /// <summary>
        /// Current ViewModel
        /// </summary>
        public ViewModel? CurrentViewModel { get => _CurrentViewModel; private set => Set(ref _CurrentViewModel, value); }
        #endregion

        #endregion

        #region Commands

        #region ShowBooksViewCommand
        private ICommand? _ShowBooksViewCommand;

        /// <summary>
        /// Show BooksView
        /// </summary>
        public ICommand? ShowBooksViewCommand => _ShowBooksViewCommand ??= new LambdaCommand(OnShowBooksViewCommandExecuted, CanShowBooksViewCommandnExecute);

        private bool CanShowBooksViewCommandnExecute() => true;

        private void OnShowBooksViewCommandExecuted()
        {
            CurrentViewModel = new BooksViewModel(_booksRepository, _dialogService);
        }
        #endregion

        #region ShowBuyersViewCommand
        private ICommand? _ShowBuyersViewCommand;

        /// <summary>
        /// Show BuyersView
        /// </summary>
        public ICommand? ShowBuyersViewCommand => _ShowBuyersViewCommand ??= new LambdaCommand(OnShowBuyersViewCommandExecuted, CanShowBuyersViewCommandnExecute);

        private bool CanShowBuyersViewCommandnExecute() => true;

        private void OnShowBuyersViewCommandExecuted()
        {
            CurrentViewModel = new BuyersViewModel(_buyersRepository);
        }
        #endregion

        #region ShowStatisticViewCommand
        private ICommand? _ShowStatisticViewCommand;

        /// <summary>
        /// Show StatisticView
        /// </summary>
        public ICommand? ShowStatisticViewCommand => _ShowStatisticViewCommand ??= new LambdaCommand(OnShowStatisticViewCommandExecuted, CanShowStatisticViewCommandnExecute);

        private bool CanShowStatisticViewCommandnExecute() => true;

        private void OnShowStatisticViewCommandExecuted()
        {
            CurrentViewModel = new StatisticViewModel(_booksRepository, _sellersRepository, _buyersRepository, _transactionsRepository);
        }
        #endregion

        #endregion

        public MainWindowViewModel(
            IRepository<Book> booksRepository, 
            IRepository<Seller> sellersRepository, 
            IRepository<Buyer> buyersRepository,
            IRepository<Transaction> transactionsRepository,
            ITradingService tradingService,
            IUserDialogService dialogService)
        {
            _booksRepository = booksRepository;
            _sellersRepository = sellersRepository;
            _buyersRepository = buyersRepository;
            _transactionsRepository = transactionsRepository;
            _tradingService = tradingService;
            _dialogService = dialogService;
        }

        //public async void TestTransactionAsync()
        //{
        //    var transactionsCount = _tradingService.Transactions?.Count();

        //    var book = await _booksRepository.GetAsync(5);
        //    var seller = await _sellersRepository.GetAsync(3);
        //    var buyer = await _buyersRepository.GetAsync(7);

        //    if (book is null || book.Name is null || seller is null || buyer is null) return;

        //    var transact = await _tradingService.CrateTransactionAsync(book.Name, seller, buyer, 235m);

        //    var transactionsCount2 = _tradingService.Transactions?.Count();
        //}
    }       
}
