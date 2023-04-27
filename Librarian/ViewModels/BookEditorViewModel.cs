using Librarian.DAL.Entities;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.ViewModels
{
    public class BookEditorViewModel : ViewModel
    {
        #region BookId
        /// <summary>
        /// Book id
        /// </summary>
        public int BookId { get; }
        #endregion

        #region BookTitle
        private string? _BookTitle;

        /// <summary>
        /// Book title
        /// </summary>
        public string? BookTitle { get => _BookTitle; set => Set(ref _BookTitle, value); }
        #endregion

        public BookEditorViewModel() : this (new Book { Id = 1, Name = "Sherlock Holmes" })
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public BookEditorViewModel(Book book)
        {
            BookId = book.Id;
            BookTitle = book.Name;
        }
    }
}
