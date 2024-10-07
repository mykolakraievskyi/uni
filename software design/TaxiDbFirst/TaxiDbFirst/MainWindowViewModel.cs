using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
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

        private readonly TaxiContext _context;

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
            var faker = new Faker();
            var sqlScripts = new List<string>();

            for (var i = 0; i < 100; i++)
            {
                var newCustomer = new Customer
                {
                    Number = faker.Random.Int(1000, 9999),
                    Firstname = faker.Name.FirstName(),
                    Lastname = faker.Name.LastName(),
                    DateOfBirth = DateOnly.FromDateTime(faker.Date.Past(50, DateTime.Now.AddYears(-18))),
                    PhoneNumber = GenerateUniquePhoneNumber(),
                    Email = GenerateUniqueEmail(),
                    ClientFrom = DateOnly.FromDateTime(faker.Date.Past(10))
                };

                _context.Customers.Add(newCustomer);
                sqlScripts.Add(CreateInsertScript(newCustomer));
            }

            for (var i = 0; i < 100; i++)
            {
                var newDriver = new Driver
                {
                    Number = faker.Random.Int(2000, 9999),
                    Firstname = faker.Name.FirstName(),
                    Lastname = faker.Name.LastName(),
                    DateOfBirth = DateOnly.FromDateTime(faker.Date.Past(50, DateTime.Now.AddYears(-18))),
                    PhoneNumber = GenerateUniquePhoneNumber(),
                    Email = GenerateUniqueEmail(),
                    Rating = faker.Random.Float(3.5f, 5),
                    StartedWorkingOn = DateOnly.FromDateTime(faker.Date.Past(5))
                };

                _context.Drivers.Add(newDriver);
                sqlScripts.Add(CreateInsertScript(newDriver));
            }

            for (var i = 0; i < 100; i++)
            {
                var newVehicle = new Vehicle
                {
                    Number = $"VH{i + 1:D3}",
                    LicensePlate = faker.Vehicle.Vin(),
                    Model = faker.Vehicle.Model(),
                    Manufacturer = faker.Vehicle.Manufacturer(),
                    Year = faker.Date.Past(10).Year,
                    Seats = faker.Random.Int(2, 7),
                    CategoryId = 2
                };

                _context.Vehicles.Add(newVehicle);
                sqlScripts.Add(CreateInsertScript(newVehicle));
            }
            for (var i = 0; i < 100; i++)
            {
                var newPoint = new Point
                {
                    Longitude = faker.Address.Longitude(),
                    Latitude = faker.Address.Latitude(),
                    City = faker.Address.City(),
                    Street = faker.Address.StreetName(),
                    Number = faker.Address.BuildingNumber(),
                    Name = faker.Address.SecondaryAddress()
                };

                _context.Points.Add(newPoint);
                sqlScripts.Add(CreateInsertScript(newPoint));
            }
            _context.SaveChanges();

            for (var i = 0; i < 300; i++)
            {
                var vehicle = _context.Vehicles.Select(x => x.Id).ElementAt(faker.Random.Int(0, _context.Vehicles.Count() - 1));
                var driver = _context.Drivers.Select(x => x.Id).ElementAt(faker.Random.Int(0, _context.Drivers.Count() - 1));
                var customer = _context.Customers.Select(x => x.Id).ElementAt(faker.Random.Int(0, _context.Customers.Count() - 1));
                var startPoint = _context.Points.Select(x => x.Id).ElementAt(faker.Random.Int(0, _context.Points.Count() - 1));
                var endPoint = _context.Points.Select(x => x.Id).ElementAt(faker.Random.Int(0, _context.Points.Count() - 1));
                var newTrip = new Trip
                {
                    Number = $"TRP{i + 1:D3}",
                    VehicleId = vehicle, 
                    DriverId = driver, 
                    CustomerId = customer, 
                    StatusId = faker.Random.Int(1, 5), 
                    PickUpTime = DateOnly.FromDateTime(faker.Date.Past(1)),
                    StartPointId = startPoint,
                    EndPointId = endPoint,
                    Cost = faker.Random.Float(50, 500),
                    Rating = faker.Random.Int(1, 5)
                };

                _context.Trips.Add(newTrip);
                sqlScripts.Add(CreateInsertScript(newTrip));
            }

            _context.SaveChanges();
            SaveSqlScriptsToFile(sqlScripts);

            Customers = new ObservableCollection<Customer>(_context.Customers.ToList());
            Drivers = new ObservableCollection<Driver>(_context.Drivers.ToList());
            Vehicles = new ObservableCollection<Vehicle>(_context.Vehicles.ToList());
            Points = new ObservableCollection<Point>(_context.Points.ToList());
            Trips = new ObservableCollection<Trip>(_context.Trips.ToList());
        }

        private string GenerateUniquePhoneNumber()
        {
            var faker = new Faker();
            string phoneNumber;
            do
            {
                phoneNumber = faker.Random.String2(10, "0123456789"); 
            } while (_context.Customers.Any(c => c.PhoneNumber == phoneNumber) ||
                     _context.Drivers.Any(d => d.PhoneNumber == phoneNumber));

            return phoneNumber;
        }

        private string GenerateUniqueEmail()
        {
            var faker = new Faker();
            string email;
            do
            {
                email = faker.Internet.Email();
            } while (_context.Customers.Any(c => c.Email == email) ||
                     _context.Drivers.Any(d => d.Email == email));

            return email;
        }

        private static string CreateInsertScript(object entity)
        {
            return entity switch
            {
                Customer customer =>
                    $"INSERT INTO Customers (Number, Firstname, Lastname, DateOfBirth, PhoneNumber, Email, ClientFrom) " +
                    $"VALUES ({customer.Number}, '{customer.Firstname}', '{customer.Lastname}', '{customer.DateOfBirth}', " +
                    $"'{customer.PhoneNumber}', '{customer.Email}', '{customer.ClientFrom}');",
                Driver driver =>
                    $"INSERT INTO Drivers (Number, Firstname, Lastname, DateOfBirth, PhoneNumber, Email, Rating, StartedWorkingOn) " +
                    $"VALUES ({driver.Number}, '{driver.Firstname}', '{driver.Lastname}', '{driver.DateOfBirth}', " +
                    $"'{driver.PhoneNumber}', '{driver.Email}', {driver.Rating}, '{driver.StartedWorkingOn}');",
                Vehicle vehicle =>
                    $"INSERT INTO Vehicles (Number, LicensePlate, Model, Manufacturer, Year, Seats, CategoryId) " +
                    $"VALUES ('{vehicle.Number}', '{vehicle.LicensePlate}', '{vehicle.Model}', '{vehicle.Manufacturer}', " +
                    $"{vehicle.Year}, {vehicle.Seats}, {vehicle.CategoryId});",
                Point point => $"INSERT INTO Points (Longitude, Latitude, City, Street, Number, Name) " +
                               $"VALUES ({point.Longitude}, {point.Latitude}, '{point.City}', '{point.Street}', '{point.Number}', '{point.Name}');",
                Trip trip =>
                    $"INSERT INTO Trips (Number, VehicleId, DriverId, CustomerId, StatusId, PickUpTime, StartPointId, EndPointId, Cost, Rating) " +
                    $"VALUES ('{trip.Number}', {trip.VehicleId}, {trip.DriverId}, {trip.CustomerId}, {trip.StatusId}, " +
                    $"'{trip.PickUpTime}', {trip.StartPointId}, {trip.EndPointId}, {trip.Cost}, {trip.Rating});",
                _ => string.Empty
            };
        }

        private static void SaveSqlScriptsToFile(List<string> sqlScripts)
        {
            File.WriteAllLines("GeneratedData.sql", sqlScripts);
        }
    }
}
