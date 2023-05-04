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
    public class CategoryEditorViewModel : ViewModel
    {
        #region Tilte
        private string? _Title = "Category Editor";

        /// <summary>
        /// Window title
        /// </summary>
        public string? Title { get => _Title; set => Set(ref _Title, value); }
        #endregion

        #region CategoryId
        /// <summary>
        /// Category id
        /// </summary>
        public int CategoryId { get; }
        #endregion

        #region CategoryName
        private string? _CategoryName;

        /// <summary>
        /// Category title
        /// </summary>
        public string? CategoryName { get => _CategoryName; set => Set(ref _CategoryName, value); }
        #endregion

        public CategoryEditorViewModel() : this(new Category { Id = 1, Name = "Criminal" })
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public CategoryEditorViewModel(Category category)
        {
            CategoryId = category.Id;
            CategoryName = category.Name;
        }
    }
}
