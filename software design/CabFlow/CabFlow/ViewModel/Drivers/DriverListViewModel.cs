using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            Drivers = [new Driver()
            {
                Firstname = "Mykola",
                Lastname = "Kraievskyi",
                DateOfBirth = new DateTime(2005, 05, 22),
                Email = "m.kraievskyi@gmail.com",
                PhoneNumber = "0970080375",
                Rating = 5.0f
            }, new Driver()
            {
                Firstname = "Katia",
                Lastname = "Hilfanova",
                DateOfBirth = new DateTime(2005, 01, 12),
                Email = "k.hilfanova@gmail.com",
                PhoneNumber = "0970080376",
                Rating = 5.0f
            },new Driver()
            {
                Firstname = "Mykola",
                Lastname = "Kraievskyi",
                DateOfBirth = new DateTime(2005, 05, 22),
                Email = "m.kraievskyi@gmail.com",
                PhoneNumber = "0970080375",
                Rating = 5.0f
            }, new Driver()
            {
                Firstname = "Katia",
                Lastname = "Hilfanova",
                DateOfBirth = new DateTime(2005, 01, 12),
                Email = "k.hilfanova@gmail.com",
                PhoneNumber = "0970080376",
                Rating = 5.0f
            },new Driver()
            {
                Firstname = "Mykola",
                Lastname = "Kraievskyi",
                DateOfBirth = new DateTime(2005, 05, 22),
                Email = "m.kraievskyi@gmail.com",
                PhoneNumber = "0970080375",
                Rating = 5.0f
            }, new Driver()
            {
                Firstname = "Katia",
                Lastname = "Hilfanova",
                DateOfBirth = new DateTime(2005, 01, 12),
                Email = "k.hilfanova@gmail.com",
                PhoneNumber = "0970080376",
                Rating = 5.0f
            }];

            //
            //InitAsync();
        }

        private async Task InitAsync()
        {
            Drivers = new ObservableCollection<Driver>(await _driverService.GetDriversAsync());
        }

        public void OpenDriver(Driver driver = null)
        {
            var header = driver == null ? "New Driver" : driver.Fullname;
            _tabService.AddTab(header, (new DriverViewModel(driver)));
        }

        public void OpenDriver()
        {
            OpenDriver(SelectedDriver);
            SelectedDriver = null;
        }
    }
}
