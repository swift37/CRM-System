using Microsoft.Extensions.DependencyInjection;

namespace Librarian.ViewModels
{
    public static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<BooksViewModel>()
            .AddSingleton<CategoriesViewModel>()
            .AddSingleton<SellersViewModel>()
            .AddSingleton<BuyersViewModel>()
            .AddSingleton<TransactionsViewModel>()
            .AddSingleton<StatisticViewModel>()
            .AddSingleton<BookEditorViewModel>()
            .AddSingleton<CategoryEditorViewModel>()
            .AddSingleton<BuyerEditorViewModel>()
            .AddSingleton<SellerEditorViewModel>()
            .AddSingleton<TransactionEditorViewModel>()
            .AddSingleton<DashboardViewModel>()
            ;
    }
}
