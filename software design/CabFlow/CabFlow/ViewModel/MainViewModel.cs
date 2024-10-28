using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CabFlow.Services;
using CabFlow.View.Drivers;
using CabFlow.View.Orders;
using CabFlow.View.Vehicles;
using CabFlow.ViewModel.Drivers;
using CabFlow.ViewModel.Orders;
using CabFlow.ViewModel.Vehicles;

namespace CabFlow.ViewModel
{
    public class MainViewModel : Core.ViewModel
    {
        public MainViewModel(DriverListViewModel driverListViewModel,
            VehicleListViewModel vehicleListViewModel,
            OrderListViewModel orderListViewModel,
            TabService tabService)
        {
            _tabService = tabService;
            _tabService.AddTab("Drivers", driverListViewModel);
            _tabService.AddTab("Vehicles", vehicleListViewModel);
            _tabService.AddTab("Orders", orderListViewModel);
            Tabs = _tabService.Tabs;
        }

        private TabService _tabService;
        public ObservableCollection<TabItem> Tabs { get; init; }
    }
}
