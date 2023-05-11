using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Microsoft.EntityFrameworkCore;
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
    public class TransactionEditorViewModel : ViewModel
    {
        private readonly IRepository<Book> _booksRepository;
        private readonly IRepository<Seller> _sellersRepository;
        private readonly IRepository<Buyer> _buyersRepository;

        #region Properties

        #region Tilte
        private string? _Title = "Transaction Editor";

        /// <summary>
        /// Window title
        /// </summary>
        public string? Title { get => _Title; set => Set(ref _Title, value); }
        #endregion

        #region TransactionId
        /// <summary>
        /// Transaction id
        /// </summary>
        public int TransactionId { get; }
        #endregion

        #region TransactionDate
        private DateTime? _TransactionDate;

        /// <summary>
        /// Transaction date
        /// </summary>
        public DateTime? TransactionDate { get => _TransactionDate; set => Set(ref _TransactionDate, value); }
        #endregion

        #region TransactionAmount
        private decimal _TransactionAmount;

        /// <summary>
        /// Transaction amount
        /// </summary>
        public decimal TransactionAmount { get => _TransactionAmount; set => Set(ref _TransactionAmount, value); }
        #endregion

        #region TransactionDiscount
        private decimal _TransactionDiscount;

        /// <summary>
        /// Transaction discount
        /// </summary>
        public decimal TransactionDiscount { 
            get => _TransactionDiscount; 
            set
            {
                if (Set(ref _TransactionDiscount, value))
                    if (Book != null) TransactionAmount = Book.Price - value;
            }
        }
        #endregion

        #region Book
        private Book? _Book;

        /// <summary>
        /// Book
        /// </summary>
        public Book? Book { 
            get => _Book; 
            set
            {
                if (Set(ref _Book, value)) 
                    if (Book != null) TransactionAmount = Book.Price - TransactionDiscount;
            } 
        }
        #endregion

        #region Seller
        private Seller? _Seller;

        /// <summary>
        /// Seller
        /// </summary>
        public Seller? Seller { get => _Seller; set => Set(ref _Seller, value); }
        #endregion

        #region Buyer
        private Buyer? _Buyer;

        /// <summary>
        /// Buyer
        /// </summary>
        public Buyer? Buyer { get => _Buyer; set => Set(ref _Buyer, value); }
        #endregion

        #region Books
        private IEnumerable<Book>? _Books;

        /// <summary>
        /// Books collection
        /// </summary>
        public IEnumerable<Book>? Books { get => _Books; set => Set(ref _Books, value); }
        #endregion 

        #region Sellers
        private IEnumerable<Seller>? _Sellers;

        /// <summary>
        /// Sellers collection
        /// </summary>
        public IEnumerable<Seller>? Sellers { get => _Sellers; set => Set(ref _Sellers, value); }
        #endregion 

        #region Buyers
        private IEnumerable<Buyer>? _Buyers;

        /// <summary>
        /// Buyers collection
        /// </summary>
        public IEnumerable<Buyer>? Buyers { get => _Buyers; set => Set(ref _Buyers, value); }
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
            if (_booksRepository.Entities is null) 
                throw new ArgumentNullException("Books list is empty or failed to load", nameof(_booksRepository.Entities));
            if (_sellersRepository.Entities is null)
                throw new ArgumentNullException("Sellers list is empty or failed to load", nameof(_sellersRepository.Entities));
            if (_buyersRepository.Entities is null)
                throw new ArgumentNullException("Buyers list is empty or failed to load", nameof(_buyersRepository.Entities));

            Books = await _booksRepository.Entities.ToArrayAsync();
            Sellers = await _sellersRepository.Entities.ToArrayAsync();
            Buyers = await _buyersRepository.Entities.ToArrayAsync();
        }
        #endregion

        public TransactionEditorViewModel() : this(
            new Transaction { Id = 1, TransactionDate = DateTime.Now, Amount = 300, Discount = 20 },
            new DebugBooksRepository(),
            new DebugSellersRepository(),
            new DebugBuyersRepository())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public TransactionEditorViewModel(
            Transaction transaction, 
            IRepository<Book> books, 
            IRepository<Seller> sellers, 
            IRepository<Buyer> buyers)
        {
            _booksRepository = books;
            _sellersRepository = sellers;
            _buyersRepository = buyers;

            TransactionId = transaction.Id;
            TransactionDate = transaction.TransactionDate;
            TransactionAmount = transaction.Amount;
            TransactionDiscount = transaction.Discount;
            Book = transaction.Book;
            Seller = transaction.Seller;
            Buyer = transaction.Buyer;
        }
    }
}
