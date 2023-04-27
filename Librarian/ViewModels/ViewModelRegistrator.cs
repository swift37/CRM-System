using Microsoft.Extensions.DependencyInjection;

namespace Librarian.ViewModels
{
    public static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<BooksViewModel>()
            .AddSingleton<BuyersViewModel>()
            .AddSingleton<StatisticViewModel>()
            .AddSingleton<BookEditorViewModel>()
            ;
    }
}
