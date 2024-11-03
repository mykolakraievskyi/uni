using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Input;
using CabFlow.Core;
using CabFlow.Model;
using CabFlow.Services;

namespace CabFlow.ViewModel.Drivers
{
    public class DriverListViewModel : Core.ViewModel
    {
        private readonly DriverService _driverService;
        private readonly TabService _tabService;

        private ObservableCollection<Driver> _drivers;

        public ObservableCollection<Driver> Drivers
        {
            get => _drivers;
            set
            {
                _drivers = value;
                OnPropertyChanged();
            }
        }

        private Driver _selectedDriver;
        private RelayCommand _openDriverCommand;

        public Driver SelectedDriver
        {
            get => _selectedDriver;
            set
            {
                _selectedDriver = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand OpenDriverCommand
        {
            get => _openDriverCommand;
            set
            {
                _openDriverCommand = value;
                OnPropertyChanged();
            }
        }

        public DriverListViewModel(DriverService driverService, TabService tabService)
        {
            OpenDriverCommand = new RelayCommand(execute: x => OpenDriver(), canExecute: x => true);

            _driverService = driverService;
            _tabService = tabService;

            _tabService.TabChanged += FetchData;
            
             InitAsync();
        }

        private async Task InitAsync()
        {
            Drivers = new ObservableCollection<Driver>(await _driverService.GetDriversAsync());
        }

        public void OpenDriver(Driver driver = null)
        {
            var header = driver == null ? "New Driver" : driver.Fullname;
            _tabService.AddOrOpenTab(header, (new DriverViewModel(driver, _driverService)));
        }

        public void OpenDriver()
        {
            OpenDriver(SelectedDriver);
            SelectedDriver = null;
        }
        
        public async void FetchData(object? sender, EventArgs e)
        {
            Drivers = new ObservableCollection<Driver>(await _driverService.GetDriversAsync());
        }
    }
}
