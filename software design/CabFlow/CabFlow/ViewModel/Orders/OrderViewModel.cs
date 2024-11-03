using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CabFlow.Core;
using CabFlow.Model;
using CabFlow.Services;

namespace CabFlow.ViewModel.Orders
{
    public class OrderViewModel: EditableViewModel
    {
        public OrderViewModel(Order order, OrderService orderService)
        {
            _orderService = orderService;
            if (order is not null)
            {
                Number = order.Number;
                Vehicle = order.Vehicle.Info;
                Driver = order.Driver.Fullname;
                OrderStatus = order.OrderStatus.Name;
                PickUpTime = order.PickUpTime;
            }

            _backupOrder = order;
        }

        private readonly OrderService _orderService;
        private Order _backupOrder;
        private int _number;
        private string _vehicle;
        private string _driver;
        private string _orderStatus;
        private DateTime _pickUpTime;

        public int Number
        {
            get => _number;
            set { _number = value; OnPropertyChanged(); }
        }

        public string Vehicle
        {
            get => _vehicle;
            set { _vehicle = value; OnPropertyChanged(); }
        }

        public string Driver
        {
            get => _driver;
            set { _driver = value; OnPropertyChanged(); }
        }

        public string OrderStatus
        {
            get => _orderStatus;
            set { _orderStatus = value; OnPropertyChanged(); }
        }

        public DateTime PickUpTime
        {
            get => _pickUpTime;
            set { _pickUpTime = value; OnPropertyChanged(); }
        }

        public override void SaveData()
        {
            var isAdding = _backupOrder is null;

            if (isAdding)
            {
                _backupOrder = new Order();
            }

            _backupOrder.Number = Number;
            //_backupOrder.Vehicle = new Vehicle { Info = Vehicle };
            //_backupOrder.Driver = new Driver { Fullname = Driver };
            //_backupOrder.OrderStatus = new OrderStatus { Name = OrderStatus };
            _backupOrder.PickUpTime = PickUpTime;

            if (isAdding)
            {  
                _orderService.AddOrderAsync(_backupOrder);
            }
            else
            {
                _orderService.UpdateOrderAsync(_backupOrder);
            }
        }

        public override void Cancel()
        {
            throw new NotImplementedException();
        }
    }
}
