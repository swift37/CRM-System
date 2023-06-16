using Librarian.DAL.Entities;
using Librarian.Interfaces;

namespace Librarian.Services.Interfaces
{
    public interface IUserDialogService
    {
        bool EditProduct(
            Product product, 
            IRepository<Category> categoriesRepository, 
            IRepository<Supplier> suppliersRepository);

        bool EditCategory(Category category);

        bool EditCustomer(Customer customer);

        bool EditEmployee(Employee employee, IRepository<WorkingRate> workingRatesRepository);

        bool EditOrder(
            Order order, 
            IRepository<Product> products, 
            IRepository<Employee> employees, 
            IRepository<Customer> customers);

        bool Confirmation(string message, string caption);

        void Warning(string message, string caption);

        void Error(string message, string caption);
    }
}
