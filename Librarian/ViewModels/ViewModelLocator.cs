using Microsoft.Extensions.DependencyInjection;

namespace Librarian.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel? MainWindowModel => App.Services?.GetRequiredService<MainWindowViewModel>();

        public DashboardViewModel? DashboardViewModel => App.Services?.GetRequiredService<DashboardViewModel>();

        public ProductsViewModel? ProductsViewModel => App.Services?.GetRequiredService<ProductsViewModel>();

        public EmployeesViewModel? EmployeeViewModel => App.Services?.GetRequiredService<EmployeesViewModel>();

        public CustomersViewModel? CustomersViewModel => App.Services?.GetRequiredService<CustomersViewModel>();

        public OrdersViewModel? OrdersViewModel => App.Services?.GetRequiredService<OrdersViewModel>();

        public StatisticsViewModel? StatisticsViewModel => App.Services?.GetRequiredService<StatisticsViewModel>();

        public ProductEditorViewModel? ProductEditorViewModel => App.Services?.GetRequiredService<ProductEditorViewModel>();

        public CategoryEditorViewModel? CategoryEditorViewModel => App.Services?.GetRequiredService<CategoryEditorViewModel>();

        public CustomerEditorViewModel? CustomersEditorViewModel => App.Services?.GetRequiredService<CustomerEditorViewModel>();

        public EmployeeEditorViewModel? EmployeeEditorViewModel => App.Services?.GetRequiredService<EmployeeEditorViewModel>();

        public TransactionEditorViewModel? TransactionEditorViewModel => App.Services?.GetRequiredService<TransactionEditorViewModel>();

    }
}
