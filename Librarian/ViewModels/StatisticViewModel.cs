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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #region Properties

        #region TopBooks
        public ObservableCollection<TopBookInfo> TopBooks { get; set; } = new ObservableCollection<TopBookInfo>();
        #endregion

        #region TopCategories
        public ObservableCollection<TopCategoryInfo> TopCategories { get; set; } = new ObservableCollection<TopCategoryInfo>(); 
        #endregion

        #region BooksCount
        private int _BooksCount;

        /// <summary>
        /// Books count
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

            TopBooks.ClearAdd(await topBooksQuery.ToArrayAsync());    
        }

        private async Task CollectCategoriesTransactionsStatisticAsync()
        {
            var transactions = _transactionsRepository.Entities;

            if (transactions is null) return;
            if (_categoriesRepository.Entities is null) return;

            var topCategoriesQuery = transactions.GroupBy(transaction => transaction.Book.Category.Id)
                .Select(categoryStatistic => new { CategoryId = categoryStatistic.Key, TransactionsCount = categoryStatistic.Count(), TransactionsAmount = categoryStatistic.Sum(t => t.Amount) })
                .OrderByDescending(category => category.TransactionsCount)
                .Take(20)
                .Join(_categoriesRepository.Entities,
                    transactions => transactions.CategoryId,
                    category => category.Id,
                    (transactions, category) => new TopCategoryInfo { Category = category, TransactionsCount = transactions.TransactionsCount, TransactionsAmount = transactions.TransactionsAmount });

            TopCategories.ClearAdd(await topCategoriesQuery.ToArrayAsync());
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
        }
    }
}
