using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Librarian.DAL
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesDb(this IServiceCollection services) => services
            .AddTransient<IRepository<Category>, DbRepository<Category>>()
            .AddTransient<IRepository<Book>, BooksRepository>()
            .AddTransient<IRepository<Seller>, DbRepository<Seller>>()
            .AddTransient<IRepository<Buyer>, DbRepository<Buyer>>()
            .AddTransient<IRepository<Transaction>, TransactionsRepository>()
            ;
    }
}
