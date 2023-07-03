using FluentValidation;
using Librarian.Models;
using Librarian.Services.Interfaces;
using Librarian.Services.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Librarian.Services
{
    public static class ServicesRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<ITradingService, TradingService>()
            .AddTransient<IUserDialogService, UserDialogService>()
            .AddTransient<IStatisticsCollectionService, StatisticsCollectionService>()
            .AddTransient<IPasswordHashingService, PasswordHashingService>()
            .AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>()
            .AddTransient<IAuthorizationService, AuthorizationService>()
            ;
    }
}
