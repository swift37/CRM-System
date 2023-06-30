using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Librarian.Services.Interfaces;
using Librarian.Services;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace Librarian.ViewModels
{
    public class OrderFullInfoViewModel : ViewModel
    {
        #region Properties

        #region CurrentOrder
        private Order? _CurrentOrder;

        /// <summary>
        /// Current Order
        /// </summary>
        public Order? CurrentOrder { get => _CurrentOrder; set => Set(ref _CurrentOrder, value); }
        #endregion

        #region SelectedOrderDetails
        private OrderDetails? _SelectedOrderDetails;

        /// <summary>
        /// Selected order details
        /// </summary>
        public OrderDetails? SelectedOrderDetails { get => _SelectedOrderDetails; set => Set(ref _SelectedOrderDetails, value); }
        #endregion

        #region OrderId
        /// <summary>
        /// Order id
        /// </summary>
        public int OrderId { get; }
        #endregion

        #region OrderDate
        private DateTime _OrderDate;

        /// <summary>
        /// Order date
        /// </summary>
        public DateTime OrderDate { get => _OrderDate; set => Set(ref _OrderDate, value); }
        #endregion

        #region RequiredDate
        private DateTime? _RequiredDate;

        /// <summary>
        /// Required date
        /// </summary>
        public DateTime? RequiredDate { get => _RequiredDate; set => Set(ref _RequiredDate, value); }
        #endregion

        #region ShippedDate
        private DateTime? _ShippedDate;

        /// <summary>
        /// Shipped date
        /// </summary>
        public DateTime? ShippedDate { get => _ShippedDate; set => Set(ref _ShippedDate, value); }
        #endregion

        #region OrderAmount
        private decimal _OrderAmount;

        /// <summary>
        /// Order amount
        /// </summary>
        public decimal OrderAmount { get => _OrderAmount; set => Set(ref _OrderAmount, value); }
        #endregion

        #region OrderProductsQuantity
        private int _OrderProductsQuantity;

        /// <summary>
        /// Order amount
        /// </summary>
        public int OrderProductsQuantity { get => _OrderProductsQuantity; set => Set(ref _OrderProductsQuantity, value); }
        #endregion

        #region OrderEmployee
        private Employee? _OrderEmployee;

        /// <summary>
        /// Employee
        /// </summary>
        public Employee? OrderEmployee { get => _OrderEmployee; set => Set(ref _OrderEmployee, value); }
        #endregion

        #region OrderCustomer
        private Customer? _OrderCustomer;

        /// <summary>
        /// Customer
        /// </summary>
        public Customer? OrderCustomer { get => _OrderCustomer; set => Set(ref _OrderCustomer, value); }
        #endregion

        #region OrderShipVia
        private Shipper? _OrderShipVia;

        /// <summary>
        /// ShipVia
        /// </summary>
        public Shipper? OrderShipVia { get => _OrderShipVia; set => Set(ref _OrderShipVia, value); }
        #endregion

        #region OrderShippingCost
        private decimal _OrderShippingCost;

        /// <summary>
        /// Order shipping cost
        /// </summary>
        public decimal OrderShippingCost { get => _OrderShippingCost; set => Set(ref _OrderShippingCost, value); }
        #endregion

        #region OrderShipName
        private string? _OrderShipName;

        /// <summary>
        /// Receiver name
        /// </summary>
        public string? OrderShipName { get => _OrderShipName; set => Set(ref _OrderShipName, value); }
        #endregion

        #region OrderShipAddress
        private string? _OrderShipAddress;

        /// <summary>
        /// Receiver address
        /// </summary>
        public string? OrderShipAddress { get => _OrderShipAddress; set => Set(ref _OrderShipAddress, value); }
        #endregion

        #region OrderDetails
        private ObservableCollection<OrderDetails>? _OrderDetails;

        /// <summary>
        /// Order details collection 
        /// </summary>
        public ObservableCollection<OrderDetails>? OrderDetails { get => _OrderDetails; set => Set(ref _OrderDetails, value); }
        #endregion

        #endregion

        public OrderFullInfoViewModel() : this(
            new Order { Id = 1, OrderDate = DateTime.Now, RequiredDate = DateTime.Now, ShippedDate = DateTime.Now, Amount = 157, ProductsQuantity = 15 })
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public OrderFullInfoViewModel(
            Order order)
        {
            OrderId = order.Id;
            OrderDate = order.OrderDate;
            RequiredDate = order.RequiredDate;
            ShippedDate = order.ShippedDate;
            OrderAmount = order.Amount;
            OrderProductsQuantity = order.ProductsQuantity;
            OrderEmployee = order.Employee;
            OrderCustomer = order.Customer;
            OrderShipVia = order.ShipVia;
            OrderShippingCost = order.ShippingCost;
            OrderShipName = order.ShipName;
            OrderShipAddress = order.ShipAddress;
            OrderDetails = order.OrderDetails?.ToObservableCollection();
        }
    }
}
