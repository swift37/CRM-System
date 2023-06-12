using Librarian.DAL.Entities;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        #region SellerDateOfBirth
        private DateTime _SellerDateOfBirth;

        /// <summary>
        /// Seller date of birth
        /// </summary>
        public DateTime SellerDateOfBirth { get => _SellerDateOfBirth; set => Set(ref _SellerDateOfBirth, value); }
        #endregion

        #region SellerContactNumber
        private string? _SellerContactNumber;

        /// <summary>
        /// Seller contact number
        /// </summary>
        public string? SellerContactNumber { get => _SellerContactNumber; set => Set(ref _SellerContactNumber, value); }
        #endregion

        #region SellerMail
        private string? _SellerMail;

        /// <summary>
        /// Seller mail
        /// </summary>
        public string? SellerMail { get => _SellerMail; set => Set(ref _SellerMail, value); }
        #endregion

        #region SellerIdentityDocumentNumber
        private string? _SellerIdentityDocumentNumber;

        /// <summary>
        /// Seller ideidentity document number
        /// </summary>
        public string? SellerIdentityDocumentNumber { get => _SellerIdentityDocumentNumber; set => Set(ref _SellerIdentityDocumentNumber, value); }
        #endregion

        #region SellerWorkingRate
        private string? _SellerWorkingRate;

        /// <summary>
        /// Seller working rate
        /// </summary>
        public string? SellerWorkingRate { get => _SellerWorkingRate; set => Set(ref _SellerWorkingRate, value); }
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
            SellerMail = seller.ContactMail;
            SellerContactNumber = seller.ContactNumber;
            SellerIdentityDocumentNumber = seller.IndeidentityDocumentNumber;
            SellerDateOfBirth = seller.DeteOfBirth;
        }
    }
}
