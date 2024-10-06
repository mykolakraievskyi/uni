using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CabFlow.View.Drivers;
using CabFlow.View.Orders;
using CabFlow.View.Vehicles;
using CabFlow.ViewModel.Drivers;
using CabFlow.ViewModel.Orders;
using CabFlow.ViewModel.Vehicles;

namespace CabFlow.ViewModel
{
    public class MainViewModel(
        DriverListViewModel driverListViewModel,
        VehicleListViewModel vehicleListViewModel,
        OrderListViewModel orderListViewModel)
        : Core.ViewModel
    {
        public ObservableCollection<TabItem> Tabs { get; set; } =
        [
            new TabItem()
            {
                Header = "Drivers",
                Content = new Frame()
                {
                    Content = driverListViewModel
                }
            },
            new TabItem()
            {
                Header = "Vehicles",
                Content = new Frame()
                {
                    Content = vehicleListViewModel
                }
            },
            new TabItem()
            {
                Header = "Orders",
                Content = new Frame()
                {
                    Content = orderListViewModel
                }
            }
        ];
    }
}
