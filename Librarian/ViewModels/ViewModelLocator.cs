using Microsoft.Extensions.DependencyInjection;

namespace Librarian.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel? MainWindowModel => App.Services?.GetRequiredService<MainWindowViewModel>();

        public BooksViewModel? BooksViewModel => App.Services?.GetRequiredService<BooksViewModel>();

        public BuyersViewModel? BuyersViewModel => App.Services?.GetRequiredService<BuyersViewModel>();

        public StatisticViewModel? StatisticViewModel => App.Services?.GetRequiredService<StatisticViewModel>();

        public BookEditorViewModel? BookEditorViewModel => App.Services?.GetRequiredService<BookEditorViewModel>();
    }
}
