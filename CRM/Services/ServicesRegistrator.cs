using FluentValidation;
using CRM.Models;
using CRM.Services.Interfaces;
using CRM.Services.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Services
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
