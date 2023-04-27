using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Librarian.ViewModels
{
    public class BooksViewModel : ViewModel
    {
        private readonly IRepository<Book> _booksRepository;

        #region BooksNameFilter
        private string _BooksNameFilter;

        /// <summary>
        /// Filter books by name
        /// </summary>
        public string BooksNameFilter { 
            get => _BooksNameFilter;
            set
            {
                if (Set(ref _BooksNameFilter, value))
                    _booksViewSource.View.Refresh();
            } 
        }
        #endregion

        private CollectionViewSource _booksViewSource;

        public ICollectionView BooksView => _booksViewSource.View;
        
        public IEnumerable<Book>? Books => _booksRepository.Entities;

        public BooksViewModel() : this(new DebugBooksRepository())
        {
            if(!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public BooksViewModel(IRepository<Book> booksRepository)
        {
            _booksRepository = booksRepository;

            _booksViewSource = new CollectionViewSource
            {
                Source = _booksRepository.Entities?.ToArray(),
                SortDescriptions =
                {
                    new SortDescription(nameof(Book.Name), ListSortDirection.Ascending)
                }
            };

            _booksViewSource.Filter += OnBooksNameFilter;
        }

        private void OnBooksNameFilter(object sender, FilterEventArgs e)
        {
            if(!(e.Item is Book book) || string.IsNullOrWhiteSpace(BooksNameFilter)) return;

            if (book.Name is null || !book.Name.Contains(BooksNameFilter))
                e.Accepted = false;
        }
    }
}
