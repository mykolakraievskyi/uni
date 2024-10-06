using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CabFlow.Core;
using CabFlow.Model;
using CabFlow.Services;

namespace CabFlow.ViewModel.Orders
{
    public class OrderListViewModel : Core.ViewModel
    {
        private readonly OrderService _orderService;
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

        public OrderListViewModel(OrderService orderService)
        {
            _orderService = orderService;
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

        private async Task InitAsync()
        {
            Orders = new ObservableCollection<Order>(await _orderService.GetOrdersAsync());
        }
    }
}
