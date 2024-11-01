using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CabFlow.Core;
using CabFlow.Model;

namespace CabFlow.ViewModel.Vehicles
{
    public class VehicleViewModel: EditableViewModel
    {
        public VehicleViewModel(Vehicle vehicle)
        {
            Number = vehicle.Number;
            LicensePlate = vehicle.LicensePlate;
            Model = vehicle.Model;
            Manufacturer = vehicle.Manufacturer;
            Year = vehicle.Year;
            Seats = vehicle.Seats;
            Category = vehicle.Category.Name;
        }

        private int _number;
        private string _licensePlate;
        private string _model;
        private string _manufacturer;
        private int _year;
        private int _seats;
        private string _category;

        public int Number
        {
            get => _number;
            set { _number = value; OnPropertyChanged(); }
        }

        public string LicensePlate
        {
            get => _licensePlate;
            set { _licensePlate = value; OnPropertyChanged(); }
        }

        public string Model
        {
            get => _model;
            set { _model = value; OnPropertyChanged(); }
        }

        public string Manufacturer
        {
            get => _manufacturer;
            set { _manufacturer = value; OnPropertyChanged(); }
        }

        public int Year
        {
            get => _year;
            set { _year = value; OnPropertyChanged(); }
        }

        public int Seats
        {
            get => _seats;
            set { _seats = value; OnPropertyChanged(); }
        }

        public string Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(); }
        }

        public override void SaveData()
        {
            throw new NotImplementedException();
        }

        public override void Cancel()
        {
            throw new NotImplementedException();
        }
    }
}
