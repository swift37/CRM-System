﻿using Librarian.DAL.Entities;
using Librarian.Interfaces;

namespace Librarian.Services.Interfaces
{
    public interface IUserDialogService
    {
        bool EditBook(Book book, IRepository<Category> categoriesRepository);

        bool EditCategory(Category category);

        bool EditBuyer(Buyer buyer);

        bool EditTransaction(
            Transaction transaction, 
            IRepository<Book> books, 
            IRepository<Seller> sellers, 
            IRepository<Buyer> buyers);

        bool Confirmation(string message, string caption);

        void Warning(string message, string caption);

        void Error(string message, string caption);
        bool EditSeller(Seller seller);
    }
}
