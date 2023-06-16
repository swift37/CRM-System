using Microsoft.Extensions.DependencyInjection;

namespace Librarian.ViewModels
{
    public static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<ProductsViewModel>()
            .AddSingleton<EmployeesViewModel>()
            .AddSingleton<CustomersViewModel>()
            .AddSingleton<TransactionsViewModel>()
            .AddSingleton<StatisticViewModel>()
            .AddSingleton<ProductEditorViewModel>()
            .AddSingleton<CategoryEditorViewModel>()
            .AddSingleton<CustomerEditorViewModel>()
            .AddSingleton<EmployeeEditorViewModel>()
            .AddSingleton<TransactionEditorViewModel>()
            .AddSingleton<DashboardViewModel>()
            ;
    }
}
