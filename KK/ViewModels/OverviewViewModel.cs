using KK.Models.Entities;
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
            _customerRepo.GetAllCustomersMemberships();
            Customers = _customerRepo.Customers;
        }

        public void UpdateCustomer()
        {
            _customerRepo.Update(SelectedCustomer);            
        }

        public void DeleteCustomer()
        {
            _customerRepo.Remove(SelectedCustomer);
            Customers = new ObservableCollection<Customer>(_customerRepo.GetCustomersWithMembershipsAndEntries());
        }

        public void GetDataForSelectedCustomer()
        {
            _selectedCustomer = _customerRepo.GetCustomer(SelectedCustomer.Id);
        }
    }
}
