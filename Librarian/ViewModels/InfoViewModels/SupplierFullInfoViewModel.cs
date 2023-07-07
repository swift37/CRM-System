using Librarian.DAL.Entities;
using Swftx.Wpf.ViewModels;
using System;

namespace Librarian.ViewModels
{
    public class SupplierFullInfoViewModel : ViewModel
    {
        #region SupplierId
        /// <summary>
        /// Supplier id
        /// </summary>
        public int SupplierId { get; set; }
        #endregion

        #region SupplierName
        private string? _SupplierName;

        /// <summary>
        /// Supplier name
        /// </summary>
        public string? SupplierName { get => _SupplierName; set => Set(ref _SupplierName, value); }
        #endregion

        #region SupplierContactName
        private string? _SupplierContactName;

        /// <summary>
        /// Supplier contact name
        /// </summary>
        public string? SupplierContactName { get => _SupplierContactName; set => Set(ref _SupplierContactName, value); }
        #endregion

        #region SupplierContactTitle
        private string? _SupplierContactTitle;

        /// <summary>
        /// Supplier contact title
        /// </summary>
        public string? SupplierContactTitle { get => _SupplierContactTitle; set => Set(ref _SupplierContactTitle, value); }
        #endregion

        #region SupplierContactNumber
        private string? _SupplierContactNumber;

        /// <summary>
        /// Supplier contact number
        /// </summary>
        public string? SupplierContactNumber { get => _SupplierContactNumber; set => Set(ref _SupplierContactNumber, value); }
        #endregion

        #region SupplierContactMail
        private string? _SupplierContactMail;

        /// <summary>
        /// Supplier contact mail
        /// </summary>
        public string? SupplierContactMail { get => _SupplierContactMail; set => Set(ref _SupplierContactMail, value); }
        #endregion

        #region SupplierAddress
        private string? _SupplierAddress;

        /// <summary>
        /// Supplier address
        /// </summary>
        public string? SupplierAddress { get => _SupplierAddress; set => Set(ref _SupplierAddress, value); }
        #endregion

        //public SupplierFullInfoViewModel() : this(new Supplier { Id = 1, Name = "John", ContactNumber = "557345635", ContactMail = "john.winston@gmail.com" })
        //{
        //    if (!App.IsDesignMode)
        //        throw new InvalidOperationException(nameof(App.IsDesignMode));
        //}

        public SupplierFullInfoViewModel() { }

        public void InitProps(Supplier supplier)
        {
            SupplierId = supplier.Id;
            SupplierName = supplier.Name;
            SupplierContactName = supplier.ContactName;
            SupplierContactTitle = supplier.ContactTitle;
            SupplierContactNumber = supplier.ContactNumber;
            SupplierContactMail = supplier.ContactMail;
            SupplierAddress = supplier.Address;
        }
    }
}
