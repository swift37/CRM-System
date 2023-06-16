using Microsoft.Extensions.DependencyInjection;

namespace Librarian.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel? MainWindowModel => App.Services?.GetRequiredService<MainWindowViewModel>();

        public ProductsViewModel? ProductsViewModel => App.Services?.GetRequiredService<ProductsViewModel>();

        public EmployeesViewModel? EmployeeViewModel => App.Services?.GetRequiredService<EmployeesViewModel>();

        public CustomersViewModel? CustomersViewModel => App.Services?.GetRequiredService<CustomersViewModel>();

        public TransactionsViewModel? TransactionsViewModel => App.Services?.GetRequiredService<TransactionsViewModel>();

        public StatisticViewModel? StatisticViewModel => App.Services?.GetRequiredService<StatisticViewModel>();

        public ProductEditorViewModel? ProductEditorViewModel => App.Services?.GetRequiredService<ProductEditorViewModel>();

        public CategoryEditorViewModel? CategoryEditorViewModel => App.Services?.GetRequiredService<CategoryEditorViewModel>();

        public CustomerEditorViewModel? CustomersEditorViewModel => App.Services?.GetRequiredService<CustomerEditorViewModel>();

        public EmployeeEditorViewModel? EmployeeEditorViewModel => App.Services?.GetRequiredService<EmployeeEditorViewModel>();

        public TransactionEditorViewModel? TransactionEditorViewModel => App.Services?.GetRequiredService<TransactionEditorViewModel>();

        public DashboardViewModel? DashboardViewModel => App.Services?.GetRequiredService<DashboardViewModel>();

    }
}
