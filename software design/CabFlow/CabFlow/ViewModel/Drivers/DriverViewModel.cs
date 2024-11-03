﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CabFlow.Core;
using CabFlow.Model;
using CabFlow.Services;
using Microsoft.IdentityModel.Tokens;

namespace CabFlow.ViewModel.Drivers
{
    public class DriverViewModel : EditableViewModel
    {
        public DriverViewModel(Driver driver, DriverService driverService)
        {
            _driverService = driverService;
            if (driver is not null)
            {
                Firstname = driver.Firstname;
                Lastname = driver.Lastname;
                Number = driver.Number;
                DateOfBirth = driver.DateOfBirth;
                PhoneNumber = driver.PhoneNumber;
                Email = driver.Email;
                Rating = driver.Rating;
                StartedWorkingOn = driver.StartedWorkingOn;
                Categories = !driver.Categories.IsNullOrEmpty() ? string.Join(", ", driver.Categories.Select(c => c.Name)) : "B";
            }

            _driverBackup = driver;
        }

        private DriverService _driverService;
        private string _firstname;
        private string _lastname;
        private int _number;
        private DateTime _dateOfBirth;
        private string _phoneNumber;
        private string _email;
        private float _rating;
        private DateTime _startedWorkingOn;
        private string _categories;

        private Driver _driverBackup;

        public string Firstname
        {
            get => _firstname;
            set { _firstname = value; OnPropertyChanged(); }
        }

        public string Lastname
        {
            get => _lastname;
            set { _lastname = value; OnPropertyChanged(); }
        }

        public int Number
        {
            get => _number;
            set { _number = value; OnPropertyChanged(); }
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set { _dateOfBirth = value; OnPropertyChanged(); }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set { _phoneNumber = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public float Rating
        {
            get => _rating;
            set { _rating = value; OnPropertyChanged(); }
        }

        public DateTime StartedWorkingOn
        {
            get => _startedWorkingOn;
            set { _startedWorkingOn = value; OnPropertyChanged(); }
        }

        public string Categories
        {
            get => _categories;
            set { _categories = value; OnPropertyChanged(); }
        }

        public override async void SaveData()
        {
            var isAdding = _driverBackup == null;

            if (isAdding)
            {
                _driverBackup = new Driver();
            }

            _driverBackup.Firstname = Firstname;
            _driverBackup.Lastname = Lastname;
            _driverBackup.Number = Number;
            _driverBackup.DateOfBirth = DateOfBirth;
            _driverBackup.PhoneNumber = PhoneNumber;
            _driverBackup.Email = Email;
            _driverBackup.Rating = Rating;
            _driverBackup.StartedWorkingOn = StartedWorkingOn;
            _driverBackup.Categories = Categories.Split(", ").Select(c => new Category() { Name = c }).ToList();
            _driverBackup.Rating = Rating;

            if (isAdding)
            {  
                _driverService.AddDriverAsync(_driverBackup);
            }
            else
            {
                _driverService.UpdateDriverAsync(_driverBackup);
            }

            
        }

        public override void Cancel()
        {

        }
    }
}
