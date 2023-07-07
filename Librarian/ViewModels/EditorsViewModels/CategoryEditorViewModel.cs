using Librarian.DAL.Entities;
using Swftx.Wpf.ViewModels;
using System;

namespace Librarian.ViewModels
{
    public class CategoryEditorViewModel : ViewModel
    {
        #region CategoryId
        /// <summary>
        /// Category id
        /// </summary>
        public int CategoryId { get; set; }
        #endregion

        #region CategoryName
        private string? _CategoryName;

        /// <summary>
        /// Category title
        /// </summary>
        public string? CategoryName { get => _CategoryName; set => Set(ref _CategoryName, value); }
        #endregion

        //public CategoryEditorViewModel()
        //{
        //    if (!App.IsDesignMode)
        //        throw new InvalidOperationException(nameof(App.IsDesignMode));

        //    InitProps(new Category { Id = 1, Name = "Products" });
        //}

        public CategoryEditorViewModel() { }

        public void InitProps(Category category)
        {
            CategoryId = category.Id;
            CategoryName = category.Name;
        }
    }
}
