using KK.Models.Entities;
using KK.Models.Helpers;
using KK.Models.Interfaces;
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
        // private repositories
        private readonly CustomerRepository _customerRepo;
        private readonly EntryRepository _entryRepository;
        private readonly MembershipRepository _membershipRepository;
        private readonly ServiceItemRepository _serviceItemRepository;
        // Private backing fields
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
            _customerRepo = new CustomerRepository();
            _entryRepository = new EntryRepository();
            _membershipRepository = new MembershipRepository();
            _serviceItemRepository = new ServiceItemRepository();

            ResetSelectedObjects();
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
        // Method to make new entry and set it as SelectedEntry
        public void SetSelectedEntry()
        {
            if (SelectedEntry == null || SelectedCustomer != null)
            {
                SelectedEntry = new Entry
                {
                    Customer = SelectedCustomer,
                    CustomerId = SelectedCustomer.Id,
                };
                _entryRepository.Add(SelectedEntry); // Adds it to repository to get Id so it can be set to Serviceitems
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
        // Method to Check Customer with activ membership in without extra ServiceItems, usses SetEntryItemsList to set entry and piush it to repository
        public void CheckMemberIn()
        {
            if (SelectedCustomer.Membership != null || SelectedCustomer.Membership.IsActive)
            {
                SetSelectedEntry();
            }
        }
        // Method to check Customer in with Selected items
        public void CheckCustomerInWithItems()
        {
            _entryRepository.Update(SelectedEntry);
        }

        // Method to update or add Membership
        private void AddMembershipToRepo()
        {

            if (SelectedCustomer.Membership != null)
            {
                SelectedCustomer.Membership.EndDate = SelectedMembership.EndDate;
                _membershipRepository.Update(SelectedCustomer.Membership);
            }
            else
            {
                _membershipRepository.Add(SelectedMembership);
            }
        }

        public void SetMembership()
        {
            int months = 0;
            if (EntryItemsList != null)
            {
                var y = EntryItemsList.FirstOrDefault(x => x.Name == "YEAR");
                var q = EntryItemsList.FirstOrDefault(x => x.Name == "QUARTER");
                var m = EntryItemsList.FirstOrDefault(x => x.Name == "MONTH");

                if (y != null) { months = 12;  return; }
                else if (q != null) { months = 3; return; }
                else if (m != null) { months = 1; return; }              
            }
            DateTime EndDate = DateTime.Now.AddMonths(months);
            SelectedMembership.EndDate = EndDate;

            if (months != 0)
            {
                AddMembershipToRepo();
            }
        }

        // Method to retrive data all coresponding data to a customer from database and set it to the Selected Customer
        public void GetDataForSelectedCustomer()
        {
            SelectedCustomer = _customerRepo.GetCustomer(SelectedCustomer.Id);
        }

    }
}
