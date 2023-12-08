using CRM.Data;
using CRM.Services;
using CRM.ViewModels;
using CRM.Views;
using CRM.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Windows;

namespace CRM
{
    public partial class App
    {
        public static Window? CurrentWindow => FocusedWindow ?? ActiveWindow;

        public static Window? ActiveWindow => Current.Windows.OfType<Window>().FirstOrDefault(window => window.IsActive);

        public static Window? FocusedWindow => Current.Windows.OfType<Window>().FirstOrDefault(window => window.IsFocused);

        public static bool IsDesignMode { get; private set; } = true;

        private static IHost? _host;

        public static IHost? Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        
        public static IServiceProvider? Services => Host?.Services;

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddDatabase(host.Configuration.GetSection("Database"))
            .AddServices()
            .AddViewModels()
            .AddViews();

        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;

            var host = Host;
            if (host == null) throw new ArgumentNullException(nameof(host));

            using (var scope = Services?.CreateScope())
                scope?.ServiceProvider.GetRequiredService<DbInitializer>().InitializeAsync().Wait();

            base.OnStartup(e);

            Services?.GetRequiredService<AuthorizationWindow>().Show();

            await host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            var host = Host;
            if (host == null) throw new ArgumentNullException(nameof(host));
            base.OnExit(e);
            await host.StopAsync();
        }
    }
}
