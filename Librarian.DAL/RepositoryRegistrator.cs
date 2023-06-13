using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Librarian.DAL
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesDb(this IServiceCollection services) => services
            .AddTransient<IRepository<Category>, DbRepository<Category>>()
            .AddTransient<IRepository<Product>, ProductsRepository>()
            .AddTransient<IRepository<Employee>, EmployeesRepository>()
            .AddTransient<IRepository<Customer>, DbRepository<Customer>>()
            .AddTransient<IRepository<Order>, OrdersRepository>()
            .AddTransient<IRepository<OrderDetails>, OrdersDetailsRepository>()
            ;
    }
}
