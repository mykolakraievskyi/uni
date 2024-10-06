using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using CabFlow.Data.CabFlowDbContext;
using CabFlow.Services;
using CabFlow.View.Drivers;
using CabFlow.View.Orders;
using CabFlow.View.Vehicles;
using CabFlow.ViewModel;
using CabFlow.ViewModel.Drivers;
using CabFlow.ViewModel.Orders;
using CabFlow.ViewModel.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace CabFlow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                services.AddDbContext<ICabFlowContext, CabFlowContext>(options => options.UseSqlServer(ConfigurationManager.ConnectionStrings["CabFlow"].ConnectionString));

                // ViewModels
                services.AddTransient<MainViewModel>();
                services.AddSingleton<DriverListViewModel>();
                services.AddSingleton<OrderListViewModel>();
                services.AddSingleton<VehicleListViewModel>();

                // Views
                services.AddSingleton<MainWindow>();
                services.AddSingleton<DriverListView>();
                services.AddSingleton<OrderListView>();
                services.AddSingleton<VehicleListView>();

                // Services
                services.AddSingleton<DriverService>();
                services.AddSingleton<VehicleService>();
                services.AddSingleton<OrderService>();
            }).Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.DataContext = _host.Services.GetRequiredService<MainViewModel>();
            mainWindow.Show();

            base.OnStartup(e);
        }
    }

}
