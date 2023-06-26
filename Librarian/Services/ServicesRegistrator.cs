using Librarian.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Librarian.Services
{
    public static class ServicesRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<ITradingService, TradingService>()
            .AddTransient<IUserDialogService, UserDialogService>()
            .AddTransient<IStatisticsCollectionService, StatisticsCollectionService>()
            ;
    }
}
