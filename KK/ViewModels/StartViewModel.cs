using KK.Models.Entities;
using KK.Models.Helpers;
using KK.Models.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK.ViewModels
{
    internal class StartViewModel : ObservableObject
    {

        // Private backing fields
        private readonly MembershipRepository _membershipRepo;
        private readonly CustomerRepository _customerRepo;
        private Membership _SelectedMembership;
        private Customer _SelectedCustomer;
        private Entry _selectedEntry;
        private ServiceItem _selectedServiceItem;
        private ObservableCollection<Customer> _customers;

        public ServiceItem SelectedServiceItem
        {
            get { return _selectedServiceItem; }
            set
            {
                _selectedServiceItem = value;
                OnPropertyChanged(nameof(SelectedServiceItem));
            }
        }
        public Entry SelectedEntry
        {
            get { return _selectedEntry; }
            set
            {
                _selectedEntry = value;
                OnPropertyChanged(nameof(SelectedEntry));
            }
        }
        public Customer SelectedCustomer
        {
            get { return _SelectedCustomer; }
            set
            {
                _SelectedCustomer = value; 
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }
        public Membership SelectedMembership
        {
            get { return _SelectedMembership; }
            set
            {
                _SelectedMembership = value; 
                OnPropertyChanged(nameof(SelectedMembership));
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



        public StartViewModel()
        {
            _membershipRepo = new MembershipRepository();
            _customerRepo = new CustomerRepository();
            _customerRepo.GetAll();
            Customers = _customerRepo.Customers;
        }

        private void GetCustomerDetails()
        {
            SelectedCustomer = _customerRepo.GetCustomer(SelectedCustomer.Id);
        }

        public void AddServiceItem()
        {
            SelectedEntry.AddServiceItem(SelectedServiceItem);
        }
        public void CheckCustomerIn()
        {
            GetCustomerDetails();
            SelectedCustomer.AddEntry(SelectedEntry);

        }

    }
}
