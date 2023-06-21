﻿using Microsoft.Extensions.DependencyInjection;

namespace Librarian.ViewModels
{
    public static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<DashboardViewModel>()
            .AddSingleton<ProductsViewModel>()
            .AddSingleton<EmployeesViewModel>()
            .AddSingleton<CustomersViewModel>()
            .AddSingleton<OrdersViewModel>()
            .AddSingleton<StatisticsViewModel>()
            .AddSingleton<ProductEditorViewModel>()
            .AddSingleton<CategoryEditorViewModel>()
            .AddSingleton<CustomerEditorViewModel>()
            .AddSingleton<EmployeeEditorViewModel>()
            .AddSingleton<OrderEditorViewModel>()
            .AddSingleton<OrderDetailsEditorViewModel>()
            ;
    }
}
