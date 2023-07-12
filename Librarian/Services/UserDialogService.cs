using Librarian.DAL.Entities;
using Librarian.Models;
using Librarian.Services.Interfaces;
using Librarian.ViewModels;
using Librarian.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Librarian.Services
{
    public class UserDialogService : IUserDialogService
    {
        private readonly IServiceProvider _services = null!;

        public UserDialogService() 
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public UserDialogService(IServiceProvider services) => _services = services;
            

        public void OpenMainWindow(Employee? employee)
        {
            var mainWindow = _services.GetRequiredService<MainWindow>();

            var mainWindowModel = (MainWindowViewModel) mainWindow.DataContext;
            
            mainWindowModel.CurrentEmployee = employee;

            mainWindow.Show();
        }

        public bool EditProduct(Product product)
        {
            var productEditorWindow = _services.GetRequiredService<ProductEditorWindow>();

            var productEditorModel = (ProductEditorViewModel) productEditorWindow.DataContext;

            productEditorModel.InitProps(product);

            if (productEditorWindow.ShowDialog() != true) return false;

            product.Name = productEditorModel.ProductName;
            product.Category = productEditorModel.ProductCategory;
            product.Supplier = productEditorModel.ProductSupplier;
            product.UnitPrice = productEditorModel.ProductUnitPrice;
            product.UnitsInStock = productEditorModel.ProductUnitsInStock;
            product.UnitsOnOrder = productEditorModel.ProductUnitsOnOrder;

            return true;
        }

        public bool EditCategory(Category category)
        {
            var categoryEditorWindow = _services.GetRequiredService<CategoryEditorWindow>();

            var categoryEditorModel = (CategoryEditorViewModel) categoryEditorWindow.DataContext;

            categoryEditorModel.InitProps(category);

            if (categoryEditorWindow.ShowDialog() != true) return false;

            category.Name = categoryEditorModel.CategoryName;

            return true;
        }

        public bool EditCustomer(Customer customer)
        {
            var customerEditorWindow = _services.GetRequiredService<CustomerEditorWindow>();

            var customerEditorModel = (CustomerEditorViewModel)customerEditorWindow.DataContext;

            customerEditorModel.InitProps(customer);

            if(customerEditorWindow.ShowDialog() != true) return false;

            customer.Name = customerEditorModel.CustomerName;
            customer.Surname = customerEditorModel.CustomerSurname;
            customer.ContactName = customerEditorModel.CustomerContactName;
            customer.ContactTitle = customerEditorModel.CustomerContactTitle;
            customer.ContactNumber = customerEditorModel.CustomerContactNumber;
            customer.ContactMail = customerEditorModel.CustomerContactMail;
            customer.Address = customerEditorModel.CustomerAddress;

            return true;
        }

        public void ShowFullCustomerInfo(Customer customer)
        {
            var customerFullInfoWindow = _services.GetRequiredService<CustomerFullInfoWindow>();

            var customerFullInfoModel = (CustomerFullInfoViewModel)customerFullInfoWindow.DataContext;

            customerFullInfoModel.InitProps(customer);

            customerFullInfoWindow.ShowDialog();
        }

        public bool RegisterEmployee(RegisterRequest registerRequest)
        {
            var employeeEditorWindow = _services.GetRequiredService<EmployeeEditorWindow>();

            var employeeEditorModel = (EmployeeEditorViewModel)employeeEditorWindow.DataContext;;
            employeeEditorModel.IsNewEmployee = true;

            if (employeeEditorWindow.ShowDialog() != true) return false;

            registerRequest.Name = employeeEditorModel.EmployeeName;
            registerRequest.Surname = employeeEditorModel.EmployeeSurname;
            registerRequest.DateOfBirth = employeeEditorModel.EmployeeDateOfBirth;
            registerRequest.HireDate = employeeEditorModel.EmployeeHireDate;
            registerRequest.Extension = employeeEditorModel.EmployeeExtension;
            registerRequest.Title = employeeEditorModel.EmployeeTitle;
            registerRequest.ContactNumber = employeeEditorModel.EmployeeContactNumber;
            registerRequest.ContactMail = employeeEditorModel.EmployeeMail;
            registerRequest.IdentityDocumentNumber = employeeEditorModel.EmployeeIdentityDocumentNumber;
            registerRequest.WorkingRate = employeeEditorModel.EmployeeWorkingRate;
            registerRequest.Address = employeeEditorModel.EmployeeAddress;

            return true;
        }

        public bool EditEmployee(Employee employee)
        {
            var employeeEditorWindow = _services.GetRequiredService<EmployeeEditorWindow>();

            var employeeEditorModel = (EmployeeEditorViewModel)employeeEditorWindow.DataContext;

            employeeEditorModel.InitProps(employee);

            if (employeeEditorWindow.ShowDialog() != true) return false;

            employee.Login = employeeEditorModel.EmployeeLogin;
            employee.PermissionLevel = employeeEditorModel.EmployeePermissionLevel;
            employee.Password = employeeEditorModel.EmployeePassword;
            employee.Name = employeeEditorModel.EmployeeName;
            employee.Surname = employeeEditorModel.EmployeeSurname;
            employee.DateOfBirth = employeeEditorModel.EmployeeDateOfBirth;
            employee.HireDate = employeeEditorModel.EmployeeHireDate;
            employee.Extension = employeeEditorModel.EmployeeExtension;
            employee.Title = employeeEditorModel.EmployeeTitle;
            employee.ContactNumber = employeeEditorModel.EmployeeContactNumber;
            employee.ContactMail = employeeEditorModel.EmployeeMail;
            employee.IdentityDocumentNumber = employeeEditorModel.EmployeeIdentityDocumentNumber;
            employee.WorkingRate = employeeEditorModel.EmployeeWorkingRate;
            employee.Address = employeeEditorModel.EmployeeAddress;

            return true;
        }

        public void ShowFullEmployeeInfo(Employee employee)
        {
            var employeeFullInfoWindow = _services.GetRequiredService<EmployeeFullInfoWindow>();

            var employeeFullInfoModel = (EmployeeFullInfoViewModel)employeeFullInfoWindow.DataContext;

            employeeFullInfoModel.InitProps(employee);

            employeeFullInfoWindow.ShowDialog();
        }

        public bool CreateOrder(Employee? employee, Order order)
        {
            var orderEditorWindow = _services.GetRequiredService<OrderEditorWindow>();

            var orderEditorModel = (OrderEditorViewModel)orderEditorWindow.DataContext;

            orderEditorModel.OrderEmployee = employee;

            if (orderEditorWindow.ShowDialog() != true) return false;

            order.OrderDate = orderEditorModel.OrderDate;
            order.RequiredDate = orderEditorModel.RequiredDate;
            order.ShippedDate = orderEditorModel.ShippedDate;
            order.Amount = orderEditorModel.OrderAmount;
            order.ProductsQuantity = orderEditorModel.OrderProductsQuantity;
            order.Employee = orderEditorModel.OrderEmployee;
            order.Customer = orderEditorModel.OrderCustomer;
            order.ShipVia = orderEditorModel.OrderShipVia;
            order.ShippingCost = orderEditorModel.OrderShippingCost;
            order.ShipName = orderEditorModel.OrderShipName;
            order.ShipAddress = orderEditorModel.OrderShipAddress;
            order.OrderDetails = orderEditorModel.OrderDetails;

            return true;
        }

        public bool EditOrder(Order order)
        {
            var orderEditorWindow = _services.GetRequiredService<OrderEditorWindow>();

            var orderEditorModel = (OrderEditorViewModel)orderEditorWindow.DataContext;

            orderEditorModel.InitProps(order);

            if (orderEditorWindow.ShowDialog() != true) return false;

            order.OrderDate = orderEditorModel.OrderDate;
            order.RequiredDate = orderEditorModel.RequiredDate;
            order.ShippedDate = orderEditorModel.ShippedDate;
            order.Amount = orderEditorModel.OrderAmount;
            order.ProductsQuantity = orderEditorModel.OrderProductsQuantity;
            order.Employee = orderEditorModel.OrderEmployee;
            order.Customer = orderEditorModel.OrderCustomer;
            order.ShipVia = orderEditorModel.OrderShipVia;
            order.ShippingCost = orderEditorModel.OrderShippingCost;
            order.ShipName = orderEditorModel.OrderShipName;
            order.ShipAddress = orderEditorModel.OrderShipAddress;
            order.OrderDetails = orderEditorModel.OrderDetails;

            return true;
        }

        public bool EditOrderDetails(OrderDetails orderDetails)
        {
            var orderDetailsEditorWindow = _services.GetRequiredService<OrderDetailsEditorWindow>();

            var orderDetailsEditorModel = (OrderDetailsEditorViewModel)orderDetailsEditorWindow.DataContext;

            orderDetailsEditorModel.InitProps(orderDetails);

            if (orderDetailsEditorWindow.ShowDialog() != true) return false;

            orderDetails.Product = orderDetailsEditorModel.OrderDetailsProduct;
            orderDetails.Quantity = orderDetailsEditorModel.OrderDetailsQuantity;
            orderDetails.UnitPrice = orderDetailsEditorModel.OrderDetailsUnitPrice;
            orderDetails.Discount = orderDetailsEditorModel.OrderDetailsDiscount;

            return true;
        }

        public void ShowFullOrderInfo(Order order)
        {
            var orderFullInfoWindow = _services.GetRequiredService<OrderFullInfoWindow>();

            var orderFullInfoModel = (OrderFullInfoViewModel)orderFullInfoWindow.DataContext;

            orderFullInfoModel.InitProps(order);

            orderFullInfoWindow.ShowDialog();
        }

        public bool EditSupply(Supply supply)
        {
            var supplyEditorWindow = _services.GetRequiredService<SupplyEditorWindow>();

            var supplyEditorModel = (SupplyEditorViewModel)supplyEditorWindow.DataContext;

            supplyEditorModel.InitProps(supply);

            if (supplyEditorWindow.ShowDialog() != true) return false;

            supply.SupplyDate = supplyEditorModel.SupplyDate;
            supply.SupplyCost = supplyEditorModel.SupplyCost;
            supply.ProductsQuantity = supplyEditorModel.SupplyProductsQuantity;
            supply.Supplier = supplyEditorModel.SupplySupplier;
            supply.SupplyDetails = supplyEditorModel.SupplyDetails;

            return true;
        }

        public bool EditSupplyDetails(SupplyDetails supplyDetails)
        {
            var supplyDetailsEditorWindow = _services.GetRequiredService<SupplyDetailsEditorWindow>();

            var supplyDetailsEditorModel = (SupplyDetailsEditorViewModel)supplyDetailsEditorWindow.DataContext;

            supplyDetailsEditorModel.InitProps(supplyDetails);

            if (supplyDetailsEditorWindow.ShowDialog() != true) return false;

            supplyDetails.Product = supplyDetailsEditorModel.SupplyDetailsProduct;
            supplyDetails.Quantity = supplyDetailsEditorModel.SupplyDetailsQuantity;
            supplyDetails.UnitPrice = supplyDetailsEditorModel.SupplyDetailsUnitPrice;

            return true;
        }

        public void ShowFullSupplyInfo(Supply supply)
        {
            var supplyFullInfoWindow = _services.GetRequiredService<SupplyFullInfoWindow>();

            var supplyFullInfoModel = (SupplyFullInfoViewModel)supplyFullInfoWindow.DataContext;

            supplyFullInfoModel.InitProps(supply);

            supplyFullInfoWindow.ShowDialog();
        }

        public void ShowStatisticsDetails(StatisticsDetails statisticsDetails)
        {
            var statisticsDetailsWindow = _services.GetRequiredService<StatisticsDetailsWindow>();

            var statisticsDetailsModel = (StatisticsDetailsViewModel)statisticsDetailsWindow.DataContext;

            statisticsDetailsModel.InitProps(statisticsDetails);

            statisticsDetailsWindow.ShowDialog();
        }

        public bool EditSupplier(Supplier supplier)
        {
            var supplierEditorWindow = _services.GetRequiredService<SupplierEditorWindow>();

            var supplierEditorModel = (SupplierEditorViewModel)supplierEditorWindow.DataContext;

            supplierEditorModel.InitProps(supplier);

            if (supplierEditorWindow.ShowDialog() != true) return false;

            supplier.Name = supplierEditorModel.SupplierName;
            supplier.ContactName = supplierEditorModel.SupplierContactName;
            supplier.ContactTitle = supplierEditorModel.SupplierContactTitle;
            supplier.ContactNumber = supplierEditorModel.SupplierContactNumber;
            supplier.ContactMail = supplierEditorModel.SupplierContactMail;
            supplier.Address = supplierEditorModel.SupplierAddress;

            return true;
        }

        public void ShowFullSupplierInfo(Supplier supplier)
        {
            var supplierFullInfoWindow = _services.GetRequiredService<SupplierFullInfoWindow>();

            var supplierFullInfoModel = (SupplierFullInfoViewModel)supplierFullInfoWindow.DataContext;

            supplierFullInfoModel.InitProps(supplier);

            supplierFullInfoWindow.ShowDialog();
        }

        public bool EditShipper(Shipper shipper)
        {
            var shipperEditorWindow = _services.GetRequiredService<ShipperEditorWindow>();

            var shipperEditorModel = (ShipperEditorViewModel)shipperEditorWindow.DataContext;

            shipperEditorModel.InitProps(shipper);

            if (shipperEditorWindow.ShowDialog() != true) return false;

            shipper.Name = shipperEditorModel.ShipperName;
            shipper.ContactNumber = shipperEditorModel.ShipperContactNumber;

            return true;
        }

        public bool ChangePassword(out string? newLogin, out string? newPassword)
        {
            var passwordCreatorWindow = _services.GetRequiredService<PrivateSecurityChangeWindow>();

            var passwordCreatorModel = (PrivateSecurityChangeViewModel)passwordCreatorWindow.DataContext;

            if (passwordCreatorWindow.ShowDialog() != true)
            {
                newPassword = null;
                newLogin = null;
                return false;
            }

            newPassword = passwordCreatorModel.Password;
            newLogin = passwordCreatorModel.Login;

            return true;
        }

        public bool Confirmation(string message, string caption) => 
            MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

        public void Warning(string message, string caption) =>
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);

        public void Error(string message, string caption) =>
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
