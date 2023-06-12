using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Librarian.Services.Interfaces;
using Librarian.ViewModels;
using Librarian.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Librarian.Services
{
    public class UserDialogService : IUserDialogService
    {
        public bool EditBook(Book book, IRepository<Category> categoriesRepository)
        {
            var bookEditorModel = new BookEditorViewModel(book, categoriesRepository);
            var bookEditorWindow = new BookEditorWindow
            {
                DataContext = bookEditorModel,
            };

            if (bookEditorWindow.ShowDialog() != true) return false;

            book.Name = bookEditorModel.BookTitle;
            book.Category = bookEditorModel.BookCategory;
            book.Price = bookEditorModel.BookPrice;

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

        public bool EditBuyer(Buyer buyer)
        {
            var buyerEditorModel = new BuyerEditorViewModel(buyer);
            var buyerEditWindow = new BuyerEditorWindow
            {
                DataContext = buyerEditorModel,
            };

            if(buyerEditWindow.ShowDialog() != true) return false;

            buyer.Name = buyerEditorModel.BuyerName;
            buyer.Surname = buyerEditorModel.BuyerSurname;
            buyer.ContactNumber = buyerEditorModel.BuyerNumber;
            buyer.ContactMail = buyerEditorModel.BuyerMail;

            return true;
        }

        public bool EditSeller(Seller seller)
        {
            var sellerEditorModel = new SellerEditorViewModel(seller);
            var sellerEditorWindow = new SellerEditorWindow
            {
                DataContext = sellerEditorModel
            };

            if (sellerEditorWindow.ShowDialog() != true) return false;

            seller.Name = sellerEditorModel.SellerName;
            seller.Surname = sellerEditorModel.SellerSurname;
            seller.DeteOfBirth = sellerEditorModel.SellerDateOfBirth;
            seller.ContactNumber = sellerEditorModel.SellerContactNumber;
            seller.ContactMail = sellerEditorModel.SellerMail;
            seller.IndeidentityDocumentNumber = sellerEditorModel.SellerIdentityDocumentNumber;
            seller.WorkingRate = sellerEditorModel.SellerWorkingRate;

            return true;
        }

        public bool EditTransaction(Transaction transaction, IRepository<Book> books, IRepository<Seller> sellers, IRepository<Buyer> buyers)
        {
            var transactionEditorModel = new TransactionEditorViewModel(transaction, books, sellers, buyers);
            var transactionEditorWindow = new TransactionEditorWindow
            {
                DataContext = transactionEditorModel
            };

            if (transactionEditorWindow.ShowDialog() != true) return false;

            transaction.TransactionDate = transactionEditorModel.TransactionDate;
            transaction.Amount = transactionEditorModel.TransactionAmount;
            transaction.Discount = transactionEditorModel.TransactionDiscount;
            transaction.Book = transactionEditorModel.Book;
            transaction.Seller = transactionEditorModel.Seller;
            transaction.Buyer = transactionEditorModel.Buyer;

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
