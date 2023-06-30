using Librarian.DAL.Entities;
using Swftx.Wpf.ViewModels;
using System;

namespace Librarian.ViewModels
{
    public class ShipperEditorViewModel : ViewModel
    {
        #region ShipperId
        /// <summary>
        /// Shipper id
        /// </summary>
        public int ShipperId { get; }
        #endregion

        #region ShipperName
        private string? _ShipperName;

        /// <summary>
        /// Shipper Name
        /// </summary>
        public string? ShipperName { get => _ShipperName; set => Set(ref _ShipperName, value); }
        #endregion

        #region ShipperContactNumber
        private string? _ShipperContactNumber;

        /// <summary>
        /// Shipper ContactNumber
        /// </summary>
        public string? ShipperContactNumber { get => _ShipperContactNumber; set => Set(ref _ShipperContactNumber, value); }
        #endregion

        public ShipperEditorViewModel() : this(new Shipper { Id = 1, Name = "DHL", ContactNumber = "345234532" })
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public ShipperEditorViewModel(Shipper shipper)
        {
            ShipperId = shipper.Id;
            ShipperName = shipper.Name;
            ShipperContactNumber = shipper.ContactNumber;
        }
    }
}
