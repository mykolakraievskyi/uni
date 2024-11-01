using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CabFlow.Core;
using CabFlow.Model;
using CabFlow.Services;
using CabFlow.ViewModel.Drivers;

namespace CabFlow.ViewModel.Orders
{
    public class OrderListViewModel : Core.ViewModel
    {
        private readonly OrderService _orderService;
        private readonly TabService _tabService;
        public ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged();
            }
        }

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenOrderCommand;

        public OrderListViewModel(OrderService orderService, TabService tabService)
        {
            _orderService = orderService;
            _tabService = tabService;
            Orders =
            [
                new Order()
                {
                    Driver = new Driver()
                    {
                        Firstname = "John",
                        Lastname = "Smith"
                    },
                    Vehicle = new Vehicle()
                    {
                        Manufacturer = "Audi",
                        Model = "A4",
                        LicensePlate = "ABCDE",
                        Year = 2020
                    },
                    OrderStatus = new OrderStatus()
                    {
                        Name = "Planned"
                    },
                    Number = 1
                }
            ];
            //InitAsync();
        }

        public void OpenOrder()
        {
           
        }

        public void OpenOrder(Order order = null)
        {
            var header = order == null ? "New Order" : order.Number.ToString();
            _tabService.AddOrOpenTab(header, (new OrderViewModel(order)));
        }
        private async Task InitAsync()
        {
            Orders = new ObservableCollection<Order>(await _orderService.GetOrdersAsync());
        }
    }
}
