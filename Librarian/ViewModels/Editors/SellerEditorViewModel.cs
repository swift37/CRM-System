using Librarian.DAL.Entities;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.ViewModels
{
    public class SellerEditorViewModel : ViewModel
    {
        #region Tilte
        private string? _Title = "Seller Editor";

        /// <summary>
        /// Window title
        /// </summary>
        public string? Title { get => _Title; set => Set(ref _Title, value); }
        #endregion

        #region SellerId
        /// <summary>
        /// Seller id
        /// </summary>
        public int SellerId { get; }
        #endregion

        #region SellerName
        private string? _SellerName;

        /// <summary>
        /// Seller name
        /// </summary>
        public string? SellerName { get => _SellerName; set => Set(ref _SellerName, value); }
        #endregion

        #region SellerSurname
        private string? _SellerSurname;

        /// <summary>
        /// Seller surname
        /// </summary>
        public string? SellerSurname { get => _SellerSurname; set => Set(ref _SellerSurname, value); }
        #endregion

        public SellerEditorViewModel() : this(new Seller { Id = 1, Name = "John", Surname = "Winston" })
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public SellerEditorViewModel(Seller seller)
        {
            SellerId = seller.Id;
            SellerName = seller.Name;
            SellerSurname = seller.Surname;
        }
    }
}
