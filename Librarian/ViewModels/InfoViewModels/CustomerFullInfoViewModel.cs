using Librarian.DAL.Entities;
using Swftx.Wpf.ViewModels;

namespace Librarian.ViewModels
{
    public class CustomerFullInfoViewModel : ViewModel
    {
        #region CustomerId
        /// <summary>
        /// Customer id
        /// </summary>
        public int CustomerId { get; set; }
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

        #region CustomerCashbackBalance
        private decimal? _CustomerCashbackBalance;

        /// <summary>
        /// Customer cashback balance
        /// </summary>
        public decimal? CustomerCashbackBalance { get => _CustomerCashbackBalance; set => Set(ref _CustomerCashbackBalance, value); }
        #endregion

        //public CustomerFullInfoViewModel() : this(new Customer { Id = 1, Name = "John", Surname = "Winston", ContactNumber = "557345635", ContactMail = "john.winston@gmail.com" })
        //{
        //    if (!App.IsDesignMode)
        //        throw new InvalidOperationException(nameof(App.IsDesignMode));
        //}

        public CustomerFullInfoViewModel() { }

        public void InitProps(Customer customer)
        {
            CustomerId = customer.Id;
            CustomerName = customer.Name;
            CustomerSurname = customer.Surname;
            CustomerContactName = customer.ContactName;
            CustomerContactTitle = customer.ContactTitle;
            CustomerContactNumber = customer.ContactNumber;
            CustomerContactMail = customer.ContactMail;
            CustomerAddress = customer.Address;
            CustomerCashbackBalance = customer.CashbackBalance;
        }
    }
}
