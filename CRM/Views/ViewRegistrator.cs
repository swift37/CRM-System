using CRM.ViewModels;
using CRM.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CRM.Views
{
    public static class ViewRegistrator
    {
        public static IServiceCollection AddViews(this IServiceCollection services) => services
            .AddSingleton(s =>
            {
                var model = s.GetRequiredService<AuthorizationViewModel>();
                var window = new AuthorizationWindow { DataContext = model };
                model.DialogComplete += (_, _) => window.Close();

                return window;
            })
            .AddSingleton(s =>
            {
                var model = s.GetRequiredService<MainWindowViewModel>();
                var window = new MainWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<ProductEditorViewModel>();
                var window = new ProductEditorWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<CategoryEditorViewModel>();
                var window = new CategoryEditorWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<CustomerEditorViewModel>();
                var window = new CustomerEditorWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<EmployeeEditorViewModel>();
                var window = new EmployeeEditorWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<OrderEditorViewModel>();
                var window = new OrderEditorWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<SupplyEditorViewModel>();
                var window = new SupplyEditorWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<ShipperEditorViewModel>();
                var window = new ShipperEditorWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<SupplierEditorViewModel>();
                var window = new SupplierEditorWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<OrderDetailsEditorViewModel>();
                var window = new OrderDetailsEditorWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<SupplyDetailsEditorViewModel>();
                var window = new SupplyDetailsEditorWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<StatisticsDetailsViewModel>();
                var window = new StatisticsDetailsWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<StatisticsDetailsViewModel>();
                var window = new StatisticsDetailsWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<OrderFullInfoViewModel>();
                var window = new OrderFullInfoWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<CustomerFullInfoViewModel>();
                var window = new CustomerFullInfoWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<EmployeeFullInfoViewModel>();
                var window = new EmployeeFullInfoWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<SupplyFullInfoViewModel>();
                var window = new SupplyFullInfoWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<SupplierFullInfoViewModel>();
                var window = new SupplierFullInfoWindow { DataContext = model };

                return window;
            })
            .AddTransient(s =>
            {
                var model = s.GetRequiredService<PrivateSecurityChangeViewModel>();
                var window = new PrivateSecurityChangeWindow { DataContext = model };

                return window;
            })
            ;
    }
}
