using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Microsoft.EntityFrameworkCore;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class TransactionEditorViewModel : ViewModel
    {
        private readonly IRepository<Product> _booksRepository;
        private readonly IRepository<Employee> _sellersRepository;
        private readonly IRepository<Customer> _buyersRepository;

        private CollectionViewSource _booksViewSource;
        private CollectionViewSource _buyersViewSource;

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
        private DateTime _TransactionDate;

        /// <summary>
        /// Transaction date
        /// </summary>
        public DateTime TransactionDate { get => _TransactionDate; set => Set(ref _TransactionDate, value); }
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
                    if (Book != null) TransactionAmount = Book.UnitPrice - value;
            }
        }
        #endregion

        #region Book
        private Product? _Book;

        /// <summary>
        /// Book
        /// </summary>
        public Product? Book { 
            get => _Book; 
            set
            {
                if (Set(ref _Book, value)) 
                    if (Book != null) TransactionAmount = Book.UnitPrice - TransactionDiscount;
            } 
        }
        #endregion

        #region Seller
        private Employee? _Seller;

        /// <summary>
        /// Seller
        /// </summary>
        public Employee? Seller { get => _Seller; set => Set(ref _Seller, value); }
        #endregion

        #region Buyer
        private Customer? _Buyer;

        /// <summary>
        /// Buyer
        /// </summary>
        public Customer? Buyer { get => _Buyer; set => Set(ref _Buyer, value); }
        #endregion

        #region BooksView
        /// <summary>
        /// Books collection view.
        /// </summary>
        public ICollectionView BooksView => _booksViewSource.View;
        #endregion 

        #region Books
        private IEnumerable<Product>? _Books;

        /// <summary>
        /// Books collection
        /// </summary>
        public IEnumerable<Product>? Books 
        { 
            get => _Books;
            set 
            {
                if (Set(ref _Books, value))
                    _booksViewSource.Source = value;
                OnPropertyChanged(nameof(BooksView));
            } 
        }
        #endregion 

        #region BooksFilter
        private string? _BooksFilter;

        /// <summary>
        /// Filter books by name
        /// </summary>
        public string? BooksFilter
        {
            get => _BooksFilter;
            set
            {
                if (Set(ref _BooksFilter, value))
                    _booksViewSource.View.Refresh();
            }
        }
        #endregion

        #region Sellers
        private IEnumerable<Employee>? _Sellers;

        /// <summary>
        /// Sellers collection
        /// </summary>
        public IEnumerable<Employee>? Sellers { get => _Sellers; set => Set(ref _Sellers, value); }
        #endregion 

        #region BuyersView
        /// <summary>
        /// Buyers collection view.
        /// </summary>
        public ICollectionView BuyersView => _buyersViewSource.View;
        #endregion 

        #region Buyers
        private IEnumerable<Customer>? _Buyers;

        /// <summary>
        /// Buyers collection
        /// </summary>
        public IEnumerable<Customer>? Buyers 
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

        #region BuyersFilter
        private string? _BuyersFilter;

        /// <summary>
        /// Filter buyers by name, number and mail
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
            new Order { Id = 1, OrderDate = DateTime.Now },
            new DebugProductsRepository(),
            new DebugEmployeesRepository(),
            new DebugCustomersRepository())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public TransactionEditorViewModel(
            Order transaction, 
            IRepository<Product> books, 
            IRepository<Employee> sellers, 
            IRepository<Customer> buyers)
        {
            _booksRepository = books;
            _sellersRepository = sellers;
            _buyersRepository = buyers;

            _booksViewSource = new CollectionViewSource();
            _buyersViewSource = new CollectionViewSource();
            
            _booksViewSource.Filter += OnBooksFilter;
            _buyersViewSource.Filter += OnBuyersFilter;

            TransactionId = transaction.Id;
            TransactionDate = transaction.OrderDate;
            //TransactionAmount = transaction.Amount;
            //TransactionDiscount = transaction.Discount;
            //Book = transaction.Product;
            Seller = transaction.Employee;
            Buyer = transaction.Customer;
        }

        private void OnBooksFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Product book) || string.IsNullOrWhiteSpace(BooksFilter)) return;

            if (book.Name is null || !book.Name.Contains(BooksFilter))
                e.Accepted = false;
        }

        private void OnBuyersFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Customer buyer) || string.IsNullOrWhiteSpace(BuyersFilter)) return;

            if (!buyer.ToString().Contains(BuyersFilter) &&
                (buyer.Name is null || !buyer.Name.Contains(BuyersFilter)) &&
                (buyer.Surname is null || !buyer.Surname.Contains(BuyersFilter)) &&
                (buyer.ContactNumber is null || !buyer.ContactNumber.Contains(BuyersFilter)) &&
                (buyer.ContactMail is null || !buyer.ContactMail.Contains(BuyersFilter)))
                e.Accepted = false;
        }
    }
}
