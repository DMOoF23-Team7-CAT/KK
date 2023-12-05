using KK.Models.Entities;
using KK.Models.Helpers;
using KK.Models.Repositories;
using System.Collections.ObjectModel;
using System.Windows;

namespace KK.ViewModels
{
    public class MainViewModel : ObservableObject
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

        public MainViewModel()
        {
            _customerRepo = new CustomerRepository();

            // Create a new ObservableCollection and add items from the List
            Customers = new ObservableCollection<Customer>(_customerRepo.GetAll());
        }

        public void GetCustomer(int id)
        {
            SelectedCustomer = _customerRepo.GetCustomer(id);

        }

    }
}

