using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Librarian.Services.Interfaces;
using Librarian.ViewModels;
using Librarian.Views.Windows;
using System.Windows;

namespace Librarian.Services
{
    public class UserDialogService : IUserDialogService
    {
        public bool EditProduct(Product product, IRepository<Category> categoriesRepository)
        {
            var productEditorModel = new BookEditorViewModel(product, categoriesRepository);
            var productEditorWindow = new BookEditorWindow
            {
                DataContext = productEditorModel,
            };

            if (productEditorWindow.ShowDialog() != true) return false;

            product.Name = productEditorModel.BookTitle;
            product.Category = productEditorModel.BookCategory;
            product.UnitPrice = productEditorModel.BookPrice;

            return true;
        }

        public bool EditCategory(Category category)
        {
            var categoryEditorModel = new CategoryEditorViewModel(category);
            var categoryEditorWindow = new CategoryEditorWindow
            {
                DataContext = categoryEditorModel,
            };

            if (categoryEditorWindow.ShowDialog() != true) return false;

            category.Name = categoryEditorModel.CategoryName;

            return true;
        }

        public bool EditCustomer(Customer customer)
        {
            var buyerEditorModel = new BuyerEditorViewModel(customer);
            var buyerEditWindow = new BuyerEditorWindow
            {
                DataContext = buyerEditorModel,
            };

            if(buyerEditWindow.ShowDialog() != true) return false;

            customer.Name = buyerEditorModel.BuyerName;
            customer.Surname = buyerEditorModel.BuyerSurname;
            customer.ContactNumber = buyerEditorModel.BuyerNumber;
            customer.ContactMail = buyerEditorModel.BuyerMail;

            return true;
        }

        public bool EditEmployee(Employee employee)
        {
            var sellerEditorModel = new SellerEditorViewModel(employee);
            var sellerEditorWindow = new SellerEditorWindow
            {
                DataContext = sellerEditorModel
            };

            if (sellerEditorWindow.ShowDialog() != true) return false;

            employee.Name = sellerEditorModel.SellerName;
            employee.Surname = sellerEditorModel.SellerSurname;
            employee.DateOfBirth = sellerEditorModel.SellerDateOfBirth;
            employee.ContactNumber = sellerEditorModel.SellerContactNumber;
            employee.ContactMail = sellerEditorModel.SellerMail;
            employee.IdentityDocumentNumber = sellerEditorModel.SellerIdentityDocumentNumber;
            employee.WorkingRate = sellerEditorModel.SellerWorkingRate;

            return true;
        }

        public bool EditOrder(Order order, IRepository<Product> products, IRepository<Employee> employees, IRepository<Customer> customers)
        {
            var transactionEditorModel = new TransactionEditorViewModel(order, products, employees, customers);
            var transactionEditorWindow = new TransactionEditorWindow
            {
                DataContext = transactionEditorModel
            };

            if (transactionEditorWindow.ShowDialog() != true) return false;

            order.OrderDate = transactionEditorModel.TransactionDate;
            //order.Amount = transactionEditorModel.TransactionAmount;
            //order.Discount = transactionEditorModel.TransactionDiscount;
            //order.Product = transactionEditorModel.Book;
            order.Employee = transactionEditorModel.Seller;
            order.Customer = transactionEditorModel.Buyer;

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
