using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
            OpenVehicleCommand = new RelayCommand(execute: x => OpenVehicle((Vehicle)x), canExecute: _ => true);

            _tabService = tabService;
            _vehicleService = vehicleService;

            _tabService.TabChanged += FetchData;

            InitAsync();
        }

        public async Task InitAsync()
        {
            Vehicles = new ObservableCollection<Vehicle>(await _vehicleService.GetVehiclesAsync());
        }

        private void OpenVehicle(Vehicle vehicle)
        {
            var vm = new VehicleViewModel(vehicle, _vehicleService);
            _tabService.AddTab($"{vehicle.Number}", vm);
        }

        public async void FetchData(object? sender, EventArgs e)
        {
            Vehicles = new ObservableCollection<Vehicle>(await _vehicleService.GetVehiclesAsync());
        }
    }
}
