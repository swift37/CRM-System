using Microsoft.Extensions.DependencyInjection;

namespace Librarian.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel? MainWindowModel => App.Services?.GetRequiredService<MainWindowViewModel>();

        public BooksViewModel? BooksViewModel => App.Services?.GetRequiredService<BooksViewModel>();

        public CategoriesViewModel? CategoriesViewModel => App.Services?.GetRequiredService<CategoriesViewModel>();

        public SellersViewModel? SellersViewModel => App.Services?.GetRequiredService<SellersViewModel>();

        public BuyersViewModel? BuyersViewModel => App.Services?.GetRequiredService<BuyersViewModel>();

        public TransactionsViewModel? TransactionsViewModel => App.Services?.GetRequiredService<TransactionsViewModel>();

        public StatisticViewModel? StatisticViewModel => App.Services?.GetRequiredService<StatisticViewModel>();

        public BookEditorViewModel? BookEditorViewModel => App.Services?.GetRequiredService<BookEditorViewModel>();

        public CategoryEditorViewModel? CategoryEditorViewModel => App.Services?.GetRequiredService<CategoryEditorViewModel>();

        public BuyerEditorViewModel? BuyerEditorViewModel => App.Services?.GetRequiredService<BuyerEditorViewModel>();

        public SellerEditorViewModel? SellerEditorViewModel => App.Services?.GetRequiredService<SellerEditorViewModel>();

        public TransactionEditorViewModel? TransactionEditorViewModel => App.Services?.GetRequiredService<TransactionEditorViewModel>();

        public DashboardViewModel? DashboardViewModel => App.Services?.GetRequiredService<DashboardViewModel>();

    }
}
