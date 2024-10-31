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

namespace CabFlow.ViewModel.Vehicles
{
    public class VehicleListViewModel : Core.ViewModel
    {
        private readonly VehicleService _vehicleService;
        private readonly TabService _tabService;
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

        public ICommand OpenVehicleCommand { get; set; }

        public VehicleListViewModel(VehicleService vehicleService, TabService tabService)
        {
            _tabService = tabService;
            _vehicleService = vehicleService;

            OpenVehicleCommand = new RelayCommand(execute: x => OpenVehicle((Vehicle)x), canExecute: _ => true);

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

        private void OpenVehicle(Vehicle vehicle)
        {
            var vm = new VehicleViewModel(vehicle);
            _tabService.AddTab($"{vehicle.Number}", vm);
        }
    }
}
