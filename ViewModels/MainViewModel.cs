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
            Customers = new ObservableCollection<Customer>(_customerRepo.GetAll());
        }


        public void CreateCustomer()
        {
            var newCustomer = new Customer
            {
                Name = "New Customer",
                DateOfBirth = new System.DateTime(1990, 1, 1),
                HasSignedDisclaimer = true
            };

            _customerRepo.Add(newCustomer);
            Customers.Add(newCustomer);
        }

        public void UpdateCustomer()
        {
            if (Customers.Count > 0)
            {
                var customerToUpdate = Customers[0];
                customerToUpdate.Name = "Updated Customer";
                _customerRepo.Update(customerToUpdate);
            }
        }

        public void DeleteCustomer()
        {
            if (Customers.Count > 0)
            {
                var customerToDelete = Customers[0];
                _customerRepo.Remove(customerToDelete);
                Customers.Remove(customerToDelete);
            }
        }

        public void SearchCustomerById(int customerId)
        {
            var foundCustomer = _customerRepo.GetById(customerId);
            Customers.Clear();
            if (foundCustomer != null)
            {
                Customers.Add(foundCustomer);
            }
        }
    }
}
