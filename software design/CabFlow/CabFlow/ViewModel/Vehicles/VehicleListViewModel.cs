using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CabFlow.Core;
using CabFlow.Model;
using CabFlow.Services;

namespace CabFlow.ViewModel.Vehicles
{
    public class VehicleListViewModel : Core.ViewModel
    {
        private readonly VehicleService _vehicleService;
        private ObservableCollection<Vehicle> _vehicles;
        public ObservableCollection<Vehicle> Vehicles
        {
            get => _vehicles;
            set
            {
                _vehicles = value;
                OnPropertyChanged();
            }
        }

        public VehicleListViewModel(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
            Vehicles =
            [
                new Vehicle()
                {
                    LicensePlate = "ABCDEF",
                    Model = "A4",
                    Manufacturer = "Audi",
                    Year = 2020,
                    Seats = 3,
                    Number = 1
                },
                new Vehicle()
                {
                    LicensePlate = "QWERTY",
                    Model = "RS7",
                    Manufacturer = "Audi",
                    Year = 2020,
                    Seats = 1,
                    Number = 2
                },
                new Vehicle()
                {
                    LicensePlate = "QWERTY",
                    Model = "Q8",
                    Manufacturer = "Audi",
                    Year = 2023,
                    Seats = 4,
                    Number = 3
                }
            ];
            //InitAsync();
        }

        public async Task InitAsync()
        {
            Vehicles = new ObservableCollection<Vehicle>(await _vehicleService.GetVehiclesAsync());
        }
    }
}
