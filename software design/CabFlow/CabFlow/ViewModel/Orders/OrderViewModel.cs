using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CabFlow.Core;
using CabFlow.Model;

namespace CabFlow.ViewModel.Orders
{
    public class OrderViewModel: EditableViewModel
    {
        public OrderViewModel(Order order)
        {
            Number = order.Number;
            Vehicle = order.Vehicle.Info;
            Driver = order.Driver.Fullname;
            OrderStatus = order.OrderStatus.Name;
            PickUpTime = order.PickUpTime;
        }

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
    }
}
