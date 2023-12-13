﻿using KK.Models.Entities;
using KK.Models.Helpers;
using KK.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK.ViewModels
{
    public class OverviewViewModel : ObservableObject
    {
        private readonly CustomerRepository _customerRepo;
        private ObservableCollection<Customer> _customers;           
        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                OnPropertyChanged(nameof(Customers));
            }
        }

        public OverviewViewModel()
        {
            _customerRepo = new CustomerRepository();
            SetCustomersList();
        }

        public void UpdateCustomer(string name, DateTime dob, string phone, string email, bool disclaimer)
        {
            Customer customer = new Customer
            {
                Id = SelectedCustomer.Id,
                Name = name,
                DateOfBirth = dob,
                Phone = phone,
                Email = email,
                HasSignedDisclaimer = disclaimer
            };
            _customerRepo.Update(customer);
            //SetCustomersList();
        }

        public void DeleteCustomer()
        {
            _customerRepo.Remove(SelectedCustomer);
            //SetCustomersList();
        }

        public void GetDataForSelectedCustomer()
        {
            _selectedCustomer = _customerRepo.GetCustomer(SelectedCustomer.Id);
        }

        private void SetCustomersList()
        {
            _customerRepo.GetAllCustomersMemberships();
            Customers = _customerRepo.Customers;
        }
    }
}
