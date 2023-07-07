using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Swftx.Wpf.Commands;
using System.Windows.Input;
using Swftx.Wpf.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Librarian.ViewModels
{
    public class SupplyFullInfoViewModel : ViewModel
    {
        #region Properties

        #region SupplyId
        /// <summary>
        /// Supply id
        /// </summary>
        public int SupplyId { get; set; }
        #endregion

        #region SupplyDate
        private DateTime _SupplyDate;

        /// <summary>
        /// Supply date
        /// </summary>
        public DateTime SupplyDate { get => _SupplyDate; set => Set(ref _SupplyDate, value); }
        #endregion

        #region SupplyCost
        private decimal _SupplyCost;

        /// <summary>
        /// Supply cost
        /// </summary>
        public decimal SupplyCost { get => _SupplyCost; set => Set(ref _SupplyCost, value); }
        #endregion

        #region SupplyProductsQuantity
        private int _SupplyProductsQuantity;

        /// <summary>
        /// Supply products quantity
        /// </summary>
        public int SupplyProductsQuantity { get => _SupplyProductsQuantity; set => Set(ref _SupplyProductsQuantity, value); }
        #endregion

        #region SupplySupplier
        private Supplier? _SupplySupplier;

        /// <summary>
        /// Supplier
        /// </summary>
        public Supplier? SupplySupplier { get => _SupplySupplier; set => Set(ref _SupplySupplier, value); }
        #endregion

        #region SupplyDetails
        private ObservableCollection<SupplyDetails>? _SupplyDetails;

        /// <summary>
        /// Supply details collection 
        /// </summary>
        public ObservableCollection<SupplyDetails>? SupplyDetails { get => _SupplyDetails; set => Set(ref _SupplyDetails, value); }
        #endregion

        #endregion

        //public SupplyFullInfoViewModel() : this(
        //    new Supply { Id = 1, SupplyDate = DateTime.Now })
        //{
        //    if (!App.IsDesignMode)
        //        throw new InvalidOperationException(nameof(App.IsDesignMode));
        //}

        public SupplyFullInfoViewModel() { }

        public void InitProps(Supply supply)
        {
            SupplyId = supply.Id;
            SupplyDate = supply.SupplyDate;
            SupplyCost = supply.SupplyCost;
            SupplyProductsQuantity = supply.ProductsQuantity;
            SupplySupplier = supply.Supplier;
            SupplyDetails = supply.SupplyDetails?.ToObservableCollection();
        }
    }
}
