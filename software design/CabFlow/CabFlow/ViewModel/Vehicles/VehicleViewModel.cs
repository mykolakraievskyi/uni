using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CabFlow.Core;
using CabFlow.Model;
using CabFlow.Services;

namespace CabFlow.ViewModel.Vehicles
{
    public class VehicleViewModel: EditableViewModel
    {
        public VehicleViewModel(Vehicle vehicle, VehicleService vehicleService)
        {
            if (vehicle is not null)
            {
                Number = vehicle.Number;
                LicensePlate = vehicle.LicensePlate;
                Model = vehicle.Model;
                Manufacturer = vehicle.Manufacturer;
                Year = vehicle.Year;
                Seats = vehicle.Seats;
                Category = vehicle.Category.Name;
            }
            _backupVehicle = vehicle;
        }

        private Vehicle _backupVehicle;
        private VehicleService _vehicleService;
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
            var IsAdding = _backupVehicle is null;

            if (IsAdding)
            {
                _backupVehicle = new Vehicle();
            }

            _backupVehicle.Number = Number;
            _backupVehicle.LicensePlate = LicensePlate;
            _backupVehicle.Model = Model;
            _backupVehicle.Manufacturer = Manufacturer;
            _backupVehicle.Year = Year;
            _backupVehicle.Seats = Seats;
            _backupVehicle.Category = new Category { Name = Category };

            if (IsAdding)
            {
                _vehicleService.AddVehicleAsync(_backupVehicle);
            }
            else
            {
                _vehicleService.UpdateVehicleAsync(_backupVehicle);
            }
        }

        public override void Cancel()
        {
        }
    }
}
