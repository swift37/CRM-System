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
            var buyerEditModel = new BuyerEditorViewModel(buyer);
            var buyerEditWindow = new BuyerEditorWindow
            {
                DataContext = buyerEditModel,
            };

            if(buyerEditWindow.ShowDialog() != true) return false;

            buyer.Name = buyerEditModel.BuyerName;
            buyer.Surname = buyerEditModel.BuyerSurname;
            buyer.ContactNumber = buyerEditModel.BuyerNumber;
            buyer.ContactMail = buyerEditModel.BuyerMail;

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
