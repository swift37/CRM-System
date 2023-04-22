using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<Book>? _booksRepository;

        #region Tilte
        private string? _Title = "Librarian";

        /// <summary>
        /// Window title
        /// </summary>
        public string? Title { get => _Title; set => Set(ref _Title, value); }
        #endregion

        public MainWindowViewModel(IRepository<Book> booksRepository)
        {
            _booksRepository = booksRepository;

            var books = booksRepository?.Entities?.Take(10).ToArray();
        }
    }       
}
