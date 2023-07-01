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

        public SuppliesViewModel? SuppliesViewModel => App.Services?.GetRequiredService<SuppliesViewModel>();

        public StatisticsViewModel? StatisticsViewModel => App.Services?.GetRequiredService<StatisticsViewModel>();

        public ProductEditorViewModel? ProductEditorViewModel => App.Services?.GetRequiredService<ProductEditorViewModel>();

        public CategoryEditorViewModel? CategoryEditorViewModel => App.Services?.GetRequiredService<CategoryEditorViewModel>();

        public CustomerEditorViewModel? CustomersEditorViewModel => App.Services?.GetRequiredService<CustomerEditorViewModel>();

        public EmployeeEditorViewModel? EmployeeEditorViewModel => App.Services?.GetRequiredService<EmployeeEditorViewModel>();

        public OrderEditorViewModel? OrderEditorViewModel => App.Services?.GetRequiredService<OrderEditorViewModel>();
        
        public OrderDetailsEditorViewModel? OrderDetailsEditorViewModel => App.Services?.GetRequiredService<OrderDetailsEditorViewModel>();

        public SupplyEditorViewModel? SupplyEditorViewModel => App.Services?.GetRequiredService<SupplyEditorViewModel>();

        public SupplyDetailsEditorViewModel? SupplyDetailsEditorViewModel => App.Services?.GetRequiredService<SupplyDetailsEditorViewModel>();

        public ShipperEditorViewModel? ShipperEditorViewModel => App.Services?.GetRequiredService<ShipperEditorViewModel>();
        
        public SupplierEditorViewModel? SupplierEditorViewModel => App.Services?.GetRequiredService<SupplierEditorViewModel>();
        
        public StatisticsDetailsViewModel? StatisticsDetailsViewModel => App.Services?.GetRequiredService<StatisticsDetailsViewModel>();
        
        public OrderFullInfoViewModel? OrderFullInfoViewModel => App.Services?.GetRequiredService<OrderFullInfoViewModel>();
        
        public CustomerFullInfoViewModel? CustomerFullInfoViewModel => App.Services?.GetRequiredService<CustomerFullInfoViewModel>();
        
        public EmployeeFullInfoViewModel? EmployeeFullInfoViewModel => App.Services?.GetRequiredService<EmployeeFullInfoViewModel>();
        
        public SupplyFullInfoViewModel? SupplyFullInfoViewModel => App.Services?.GetRequiredService<SupplyFullInfoViewModel>();
        
        public SupplierFullInfoViewModel? SupplierFullInfoViewModel => App.Services?.GetRequiredService<SupplierFullInfoViewModel>();

    }
}
