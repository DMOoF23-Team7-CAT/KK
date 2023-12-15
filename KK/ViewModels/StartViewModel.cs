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
        private readonly CustomerRepository _customerRepo;
        private Membership _selectedMembership;
        private Customer _selectedCustomer;
        private Entry _selectedEntry;
        private ServiceItem _selectedServiceItem;
        private ObservableCollection<Customer> _customers;
        private ObservableCollection<ServiceItem> _entryItemsList;

        // Public
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
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value; 
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        } 
        public Membership SelectedMembership
        {
            get { return _selectedMembership; }
            set
            {
                _selectedMembership = value; 
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
        public ObservableCollection<ServiceItem> EntryItemsList
        {
            get { return _entryItemsList; }
            set
            {
                _entryItemsList = value;
                OnPropertyChanged(nameof(EntryItemsList));
            }
        }

        // Constructor for StartViewModel
        public StartViewModel()
        {
            ResetSelectedObjects();
            _customerRepo = new CustomerRepository();
           // _customerRepo.GetAllCustomersMemberships();
            Customers = _customerRepo.Customers;
        }

        // Method to set the observable collection of ServiceItems
        private void SetEntryItemsList()
        {
            if (SelectedEntry.Items == null)
            {
                EntryItemsList = new ObservableCollection<ServiceItem>();
                return;
            }
            EntryItemsList = new ObservableCollection<ServiceItem>(SelectedEntry.Items);
        }
        // Method to reset all selected objects to null
        public void ResetSelectedObjects()
        {
            SelectedServiceItem = null;
            SelectedEntry = null;
            SelectedCustomer = null;
            SelectedMembership = null;
        }
        // Method to Set make new entry and set it as SelectedEntry
        public void SetSelectedEntry()
        {
            if (SelectedEntry == null || SelectedCustomer != null)
            {
                SelectedEntry = new Entry
                {
                    Customer = SelectedCustomer,
                    CustomerId = SelectedCustomer.Id,
                };
                SetEntryItemsList();
            }
        }
        // Method to add ServiceItem to SelectedEntry list of items
        public void AddServiceItem(string name)
        {
            ServiceItem item = new ServiceItem(name, SelectedEntry.Id);
            SelectedEntry.AddServiceItem(item);
            SetEntryItemsList();
        }
        // Method to remove ServiceItem from SelectedEntry list of items
        public void RemoveServiceItem(string name)
        {
            if (SelectedEntry.Items == null)
            {
                var item = SelectedEntry.Items.FirstOrDefault(x => x.Name == name);
                SelectedEntry.RemoveServiceItem(item);
                SetEntryItemsList();
            }
        }
        // Method to Check Customer with activ membership in without extra ServiceItems
        public void CheckCustomerIn()
        {
            if (SelectedCustomer.Membership != null || SelectedCustomer.Membership.IsActive)
            {
                SetSelectedEntry();
                SelectedCustomer.AddEntry(SelectedEntry);
            }
        }
        // Method to retrive data all coresponding data to a customer from database and set it to the Selected Customer
        public void GetDataForSelectedCustomer()
        {
            SelectedCustomer = _customerRepo.GetCustomer(SelectedCustomer.Id);
        }

    }
}
