using Librarian.DAL.Entities;
using Swftx.Wpf.ViewModels;
using System;

namespace Librarian.ViewModels
{
    public class CustomerEditorViewModel : ViewModel
    {
        #region Tilte
        private string? _Title = "Buyer Editor";

        /// <summary>
        /// Window title
        /// </summary>
        public string? Title { get => _Title; set => Set(ref _Title, value); }
        #endregion

        #region CustomerId
        /// <summary>
        /// Customer id
        /// </summary>
        public int CustomerId { get; }
        #endregion

        #region CustomerName
        private string? _CustomerName;

        /// <summary>
        /// Customer name
        /// </summary>
        public string? CustomerName { get => _CustomerName; set => Set(ref _CustomerName, value); }
        #endregion

        #region CustomerSurname
        private string? _CustomerSurname;

        /// <summary>
        /// Customer surname
        /// </summary>
        public string? CustomerSurname { get => _CustomerSurname; set => Set(ref _CustomerSurname, value); }
        #endregion

        #region CustomerContactName
        private string? _CustomerContactName;

        /// <summary>
        /// Customer contact name
        /// </summary>
        public string? CustomerContactName { get => _CustomerContactName; set => Set(ref _CustomerContactName, value); }
        #endregion

        #region CustomerContactTitle
        private string? _CustomerContactTitle;

        /// <summary>
        /// Customer contact title
        /// </summary>
        public string? CustomerContactTitle { get => _CustomerContactTitle; set => Set(ref _CustomerContactTitle, value); }
        #endregion

        #region CustomerContactNumber
        private string? _CustomerContactNumber;

        /// <summary>
        /// Customer contact number
        /// </summary>
        public string? CustomerContactNumber { get => _CustomerContactNumber; set => Set(ref _CustomerContactNumber, value); }
        #endregion

        #region CustomerContactMail
        private string? _CustomerContactMail;

        /// <summary>
        /// Customer contact mail
        /// </summary>
        public string? CustomerContactMail { get => _CustomerContactMail; set => Set(ref _CustomerContactMail, value); }
        #endregion

        #region CustomerAddress
        private string? _CustomerAddress;

        /// <summary>
        /// Customer address
        /// </summary>
        public string? CustomerAddress { get => _CustomerAddress; set => Set(ref _CustomerAddress, value); }
        #endregion

        public CustomerEditorViewModel() : this(new Customer { Id = 1, Name = "John", Surname = "Winston", ContactNumber = "557345635", ContactMail = "john.winston@gmail.com" })
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public CustomerEditorViewModel(Customer customer)
        {
            CustomerId = customer.Id;
            CustomerName = customer.Name;
            CustomerSurname = customer.Surname;
            CustomerContactName = customer.ContactName;
            CustomerContactTitle = customer.ContactTitle;
            CustomerContactNumber = customer.ContactNumber;
            CustomerContactMail = customer.ContactMail;
            CustomerAddress = customer.Address;
        }
    }
}
