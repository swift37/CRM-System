using Microsoft.Extensions.DependencyInjection;

namespace Librarian.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel? MainWindowModel => App.Services?.GetRequiredService<MainWindowViewModel>();
    }
}
