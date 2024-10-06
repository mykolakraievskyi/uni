using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiDbFirst.Model.TaxiDbContext;

namespace TaxiDbFirst
{
    public class MainWindowViewModel : ViewModel
    {
        private ObservableCollection<Category> _categories;

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Customer> _customers;

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged();
            }
        }

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

        private ObservableCollection<DriverQualification> _driverQualifications;

        public ObservableCollection<DriverQualification> DriverQualifications
        {
            get => _driverQualifications;
            set
            {
                _driverQualifications = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<NewDriver> _newDrivers;

        public ObservableCollection<NewDriver> NewDrivers
        {
            get => _newDrivers;
            set
            {
                _newDrivers = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Point> _points;

        public ObservableCollection<Point> Points
        {
            get => _points;
            set
            {
                _points = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Trip> _trips;

        public ObservableCollection<Trip> Trips
        {
            get => _trips;
            set
            {
                _trips = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TripStatus> _tripStatuses;

        public ObservableCollection<TripStatus> TripStatuses
        {
            get => _tripStatuses;
            set
            {
                _tripStatuses = value;
                OnPropertyChanged();
            }
        }

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

        private TaxiContext _context;

        public MainWindowViewModel()
        {
            _context = new TaxiContext();
            AddRandomData();
            _categories = new ObservableCollection<Category>(_context.Categories);
            _customers = new ObservableCollection<Customer>(_context.Customers);
            _drivers = new ObservableCollection<Driver>(_context.Drivers);
            _driverQualifications = new ObservableCollection<DriverQualification>(_context.DriverQualifications);
            _newDrivers = new ObservableCollection<NewDriver>(_context.NewDrivers);
            _points = new ObservableCollection<Point>(_context.Points);
            _trips = new ObservableCollection<Trip>(_context.Trips);
            _tripStatuses = new ObservableCollection<TripStatus>(_context.TripStatuses);
            _vehicles = new ObservableCollection<Vehicle>(_context.Vehicles);
        }

        private void AddRandomData()
        {
            var random = new Random();

            for (int i = 0; i < 5; i++)
            {
                var newCustomer = new Customer
                {
                    Number = random.Next(1006, 9999), 
                    Firstname = GenerateRandomFirstName(),
                    Lastname = GenerateRandomLastName(),
                    DateOfBirth = GenerateRandomDateOfBirth(),
                    PhoneNumber = GenerateUniquePhoneNumber(),
                    Email = GenerateUniqueEmail(),
                    ClientFrom =
                        DateOnly.FromDateTime(
                            DateTime.Now.AddDays(-random.Next(100, 1000))) 
                };

                _context.Customers.Add(newCustomer);
            }

            for (int i = 0; i < 5; i++)
            {
                var newDriver = new Driver
                {
                    Number = random.Next(2006, 9999), 
                    Firstname = GenerateRandomFirstName(),
                    Lastname = GenerateRandomLastName(),
                    DateOfBirth = GenerateRandomDateOfBirth(),
                    PhoneNumber = GenerateUniquePhoneNumber(),
                    Email = GenerateUniqueEmail(),
                    Rating = Math.Round((float)(random.NextDouble() * (5 - 3.5) + 3.5), 2),
                    StartedWorkingOn =
                        DateOnly.FromDateTime(DateTime.Now.AddYears(-random.Next(1, 5)))
                };

                _context.Drivers.Add(newDriver);
            }

            _context.SaveChanges();

            Categories = new ObservableCollection<Category>(_context.Categories.ToList());
            Customers = new ObservableCollection<Customer>(_context.Customers.ToList());
            Drivers = new ObservableCollection<Driver>(_context.Drivers.ToList());
            DriverQualifications =
                new ObservableCollection<DriverQualification>(_context.DriverQualifications.ToList());
            NewDrivers = new ObservableCollection<NewDriver>(_context.NewDrivers.ToList());
            Points = new ObservableCollection<Point>(_context.Points.ToList());
            Trips = new ObservableCollection<Trip>(_context.Trips.ToList());
            TripStatuses = new ObservableCollection<TripStatus>(_context.TripStatuses.ToList());
            Vehicles = new ObservableCollection<Vehicle>(_context.Vehicles.ToList());
        }

        private string GenerateRandomFirstName()
        {
            var firstNames = new List<string>
                { "John", "Jane", "Alice", "Robert", "Emily", "Michael", "Olga", "Mykola", "Oksana", "Yaroslav" };
            return firstNames[new Random().Next(firstNames.Count)];
        }

        private string GenerateRandomLastName()
        {
            var lastNames = new List<string>
            {
                "Doe", "Smith", "Johnson", "Williams", "Davis", "Brown", "Vorobets", "Kraievskyi", "Shevchenko",
                "Kovalenko"
            };
            return lastNames[new Random().Next(lastNames.Count)];
        }

        private DateOnly GenerateRandomDateOfBirth()
        {
            var random = new Random();
            int year = random.Next(1950, 2005); 
            int month = random.Next(1, 13);
            int day = random.Next(1, 29); 
            return new DateOnly(year, month, day);
        }

        private string GenerateUniquePhoneNumber()
        {
            var phoneNumber = $"097{new Random().Next(1000000, 9999999)}";

            while (_context.Customers.Any(c => c.PhoneNumber == phoneNumber) ||
                   _context.Drivers.Any(d => d.PhoneNumber == phoneNumber))
            {
                phoneNumber = $"097{new Random().Next(1000000, 9999999)}";
            }

            return phoneNumber;
        }

        private string GenerateUniqueEmail()
        {
            var random = new Random();
            var email = $"user{random.Next(1000, 9999)}@example.com";

            while (_context.Customers.Any(c => c.Email == email) ||
                   _context.Drivers.Any(d => d.Email == email))
            {
                email = $"user{random.Next(1000, 9999)}@example.com";
            }

            return email;
        }
    }
}
