using Librarian.DAL.Entities;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.ViewModels
{
    public class BuyerEditorViewModel : ViewModel
    {
        #region Tilte
        private string? _Title = "Buyer Editor";

        /// <summary>
        /// Window title
        /// </summary>
        public string? Title { get => _Title; set => Set(ref _Title, value); }
        #endregion

        #region BuyerId
        /// <summary>
        /// Buyer id
        /// </summary>
        public int BuyerId { get; }
        #endregion

        #region BuyerName
        private string? _BuyerName;

        /// <summary>
        /// Buyer name
        /// </summary>
        public string? BuyerName { get => _BuyerName; set => Set(ref _BuyerName, value); }
        #endregion

        #region BuyerSurname
        private string? _BuyerSurname;

        /// <summary>
        /// Buyer surname
        /// </summary>
        public string? BuyerSurname { get => _BuyerSurname; set => Set(ref _BuyerSurname, value); }
        #endregion

        #region BuyerNumber
        private string? _BuyerNumber;

        /// <summary>
        /// Buyer contact number
        /// </summary>
        public string? BuyerNumber { get => _BuyerNumber; set => Set(ref _BuyerNumber, value); }
        #endregion

        #region BuyerSurname
        private string? _BuyerMail;

        /// <summary>
        /// Buyer mail
        /// </summary>
        public string? BuyerMail { get => _BuyerMail; set => Set(ref _BuyerMail, value); }
        #endregion

        public BuyerEditorViewModel() : this(new Buyer { Id = 1, Name = "John", Surname = "Winston", ContactNumber = "557345635", ContactMail = "john.winston@gmail.com" })
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public BuyerEditorViewModel(Buyer buyer)
        {
            BuyerId = buyer.Id;
            BuyerName = buyer.Name;
            BuyerSurname = buyer.Surname;
            BuyerNumber = buyer.ContactNumber;
            BuyerMail = buyer.ContactMail;
        }
    }
}
