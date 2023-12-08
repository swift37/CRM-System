using CRM.DAL.Entities;
using CRM.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.DAL
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesDb(this IServiceCollection services) => services
            .AddTransient<IRepository<Category>, DbRepository<Category>>()
            .AddTransient<IRepository<Product>, ProductsRepository>()
            .AddTransient<IRepository<Employee>, EmployeesRepository>()
            .AddTransient<IRepository<WorkingRate>, DbRepository<WorkingRate>>()
            .AddTransient<IRepository<Customer>, DbRepository<Customer>>()
            .AddTransient<IRepository<Supplier>, DbRepository<Supplier>>()
            .AddTransient<IRepository<Shipper>, DbRepository<Shipper>>()
            .AddTransient<IRepository<Order>, OrdersRepository>()
            .AddTransient<IRepository<OrderDetails>, OrdersDetailsRepository>()
            .AddTransient<IRepository<Supply>, SuppliesRepository>()
            .AddTransient<IRepository<SupplyDetails>, SuppliesDetailsRepository>()
            ;
    }
}
