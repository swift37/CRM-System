using Microsoft.Extensions.DependencyInjection;

namespace Librarian.ViewModels
{
    public static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<AuthorizationViewModel>()
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<DashboardViewModel>()
            .AddSingleton<ProductsViewModel>()
            .AddSingleton<EmployeesViewModel>()
            .AddSingleton<CustomersViewModel>()
            .AddSingleton<OrdersViewModel>()
            .AddSingleton<SuppliesViewModel>()
            .AddSingleton<StatisticsViewModel>()
            .AddSingleton<ProductEditorViewModel>()
            .AddSingleton<CategoryEditorViewModel>()
            .AddSingleton<CustomerEditorViewModel>()
            .AddSingleton<EmployeeEditorViewModel>()
            .AddSingleton<OrderEditorViewModel>()
            .AddSingleton<OrderDetailsEditorViewModel>()
            .AddSingleton<SupplyEditorViewModel>()
            .AddSingleton<SupplyDetailsEditorViewModel>()
            .AddSingleton<ShipperEditorViewModel>()
            .AddSingleton<SupplierEditorViewModel>()
            .AddSingleton<StatisticsDetailsViewModel>()
            .AddSingleton<OrderFullInfoViewModel>()
            .AddSingleton<CustomerFullInfoViewModel>()
            .AddSingleton<EmployeeFullInfoViewModel>()
            .AddSingleton<SupplyFullInfoViewModel>()
            .AddSingleton<SupplierFullInfoViewModel>()
            .AddSingleton<PrivateSecurityChangeViewModel>()
            ;
    }
}
