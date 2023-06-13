using Librarian.DAL.Entities;
using Librarian.Interfaces;

namespace Librarian.Services.Interfaces
{
    public interface IUserDialogService
    {
        bool EditProduct(Product book, IRepository<Category> categoriesRepository);

        bool EditCategory(Category category);

        bool EditCustomer(Customer buyer);

        bool EditOrder(
            Order transaction, 
            IRepository<Product> books, 
            IRepository<Employee> sellers, 
            IRepository<Customer> buyers);

        bool Confirmation(string message, string caption);

        void Warning(string message, string caption);

        void Error(string message, string caption);
        bool EditEmployee(Employee seller);
    }
}
