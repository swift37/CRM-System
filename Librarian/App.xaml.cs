using Librarian.Data;
using Librarian.Services;
using Librarian.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace Librarian
{
    public partial class App
    {
        private static IHost? _host;

        public static IHost? Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        
        public static IServiceProvider? Services => Host?.Services;

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddServices()
            .AddViewModels()
            .AddDatabase(host.Configuration.GetSection("Database"));

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            if (host == null) throw new ArgumentNullException(nameof(host));

            using (var scope = Services?.CreateScope())
                await scope?.ServiceProvider.GetRequiredService<DbInitializer>().InitializeAsync();

                base.OnStartup(e);
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
