using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Librarian.Models;
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
    public class StatisticViewModel : ViewModel
    {
        private readonly IRepository<Book> _booksRepository;
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IRepository<Seller> _sellersRepository;
        private readonly IRepository<Buyer> _buyersRepository;
        private readonly IRepository<Transaction> _transactionsRepository;
        public double[] testval1 = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        public double[] testval2 = new double[] { 1, 2, 3, 4, 5, 6, 7 };
        private CollectionViewSource _topBooksViewSource;

        #region Properties

        #region TopBooks
        private ObservableCollection<TopBookInfo>? _TopBooks = new ObservableCollection<TopBookInfo>();

        /// <summary>
        /// Top _booksRepository collection.
        /// </summary>
        public ObservableCollection<TopBookInfo>? TopBooks
        {
            get => _TopBooks;
            set
            {
                if (Set(ref _TopBooks, value))
                    _topBooksViewSource.Source = value;
                OnPropertyChanged(nameof(TopBooksView));
            }
        }
        #endregion

        #region BooksView
        /// <summary>
        /// Top _booksRepository collection view.
        /// </summary>
        public ICollectionView TopBooksView => _topBooksViewSource.View; 
        #endregion

        #region BooksNameFilter
        private string? _BooksNameFilter;

        /// <summary>
        /// Books name filter.
        /// </summary>
        public string? BooksNameFilter 
        { 
            get => _BooksNameFilter; 
            set
            {
                if (Set(ref _BooksNameFilter, value))
                    _topBooksViewSource.View.Refresh();
            } 
        }
        #endregion

        #region TopCategories
        /// <summary>
        /// Top categories collection view.
        /// </summary>
        public ObservableCollection<TopCategoryInfo> TopCategories { get; set; } = new ObservableCollection<TopCategoryInfo>();
        #endregion

        #region TopSellers
        /// <summary>
        /// Top _sellersRepository collection view.
        /// </summary>
        public ObservableCollection<TopSellerInfo> TopSellers { get; set; } = new ObservableCollection<TopSellerInfo>();
        #endregion

        #region BooksCount
        private int _BooksCount;

        /// <summary>
        /// Books count.
        /// </summary>
        public int BooksCount { get => _BooksCount; set => Set(ref _BooksCount, value); }
        #endregion

        #endregion

        #region Commands

        #region CollectStatisticsCommand
        private ICommand? _CollectStatisticsCommand;

        /// <summary>
        /// Collect book statistics 
        /// </summary>
        public ICommand? CollectStatisticsCommand => _CollectStatisticsCommand ??= new LambdaCommand(OnCollectStatisticsCommandExecuted, CanCollectStatisticsCommandnExecute);

        private bool CanCollectStatisticsCommandnExecute() => true;

        private async void OnCollectStatisticsCommandExecuted()
        {
            if (_booksRepository.Entities is null) return;
            BooksCount = await _booksRepository.Entities.CountAsync();

            await CollectBooksTransactionsStatisticAsync();
            await CollectCategoriesTransactionsStatisticAsync();
            await CollectSellersDealsStatisticAsync();
        }

        private async Task CollectBooksTransactionsStatisticAsync()
        {
            var transactions = _transactionsRepository.Entities;

            if (transactions is null) return;
            if (_booksRepository.Entities is null) return;

            var topBooksQuery = transactions.GroupBy(transaction => transaction.Book.Id)
                .Select(bookStatistic => new { BookId = bookStatistic.Key, TransactionsCount = bookStatistic.Count(), TransactionsAmount = bookStatistic.Sum(t => t.Amount) })
                .OrderByDescending(book => book.TransactionsCount)
                .Take(50)
                .Join(_booksRepository.Entities,
                    transactions => transactions.BookId,
                    book => book.Id,
                    (transactions, book) => new TopBookInfo { Book = book, TransactionsCount = transactions.TransactionsCount, TransactionsAmount = transactions.TransactionsAmount });

            TopBooks = (await topBooksQuery.ToArrayAsync()).ToObservableCollection();    
        }

        private async Task CollectCategoriesTransactionsStatisticAsync()
        {
            var transactions = _transactionsRepository.Entities;

            if (transactions is null) return;
            if (_categoriesRepository.Entities is null) return;

            var topCategoriesQuery = transactions.GroupBy(transaction => transaction.Book.Category.Id)
                .Select(categoryStatistic => new { CategoryId = categoryStatistic.Key, TransactionsCount = categoryStatistic.Count(), TransactionsAmount = categoryStatistic.Sum(t => t.Amount) })
                .OrderByDescending(category => category.TransactionsCount)
                .Take(15)
                .Join(_categoriesRepository.Entities,
                    transactions => transactions.CategoryId,
                    category => category.Id,
                    (transactions, category) => new TopCategoryInfo { Category = category, TransactionsCount = transactions.TransactionsCount, TransactionsAmount = transactions.TransactionsAmount });

            TopCategories.ClearAdd(await topCategoriesQuery.ToArrayAsync());
        }

        private async Task CollectSellersDealsStatisticAsync()
        {
            var transactions = _transactionsRepository.Entities;

            if (transactions is null) return;
            if (_sellersRepository.Entities is null) return;

            var topSellersQuery = transactions.GroupBy(transaction => transaction.Seller.Id)
                .Select(sellerStatistic => new { SellerId = sellerStatistic.Key, DealsCount = sellerStatistic.Count(), DealsAmount = sellerStatistic.Sum(d => d.Amount) })
                .OrderByDescending(seller => seller.DealsCount)
                .Join(_sellersRepository.Entities,
                    deal => deal.SellerId,
                    seller => seller.Id,
                    (deal, seller) => new TopSellerInfo { Seller = seller, DealsCount = deal.DealsCount, DealsAmount = deal.DealsAmount });

            TopSellers.ClearAdd(await topSellersQuery.ToArrayAsync());
        }
        #endregion

        #endregion

        public StatisticViewModel(
            IRepository<Book> booksRepository,
            IRepository<Category> categoriesRepository,
            IRepository<Seller> sellersRepository, 
            IRepository<Buyer> buyersRepository, 
            IRepository<Transaction> transactionsRepository)
        {
            _booksRepository = booksRepository;
            _categoriesRepository = categoriesRepository;
            _sellersRepository = sellersRepository;
            _buyersRepository = buyersRepository;
            _transactionsRepository = transactionsRepository;

            _topBooksViewSource = new CollectionViewSource();
            _topBooksViewSource.Filter += OnBookNameFilter;
        }

        private void OnBookNameFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is TopBookInfo topbook) || string.IsNullOrWhiteSpace(BooksNameFilter)) return;

            if (topbook.Book is null || topbook.Book.Name is null || !topbook.Book.Name.Contains(BooksNameFilter)) 
                e.Accepted = false;
        }
    }
}
