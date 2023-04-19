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

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            if (host == null) throw new ArgumentNullException(nameof(host));
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
