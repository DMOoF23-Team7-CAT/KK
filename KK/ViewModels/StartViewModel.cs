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
        private readonly MembershipRepository _membershipRepo;
        private readonly CustomerRepository _customerRepo;
        private ObservableCollection<Membership> _memberships { get; set; }
        private Membership _SelectedMembership;
        private Customer _SelectedCustomer;

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
        public ObservableCollection<Membership> Memberships
        {
            get { return _memberships; }
            set 
            {
                _memberships = value; 
                OnPropertyChanged(nameof(Membership));
            }
        }

        public StartViewModel()
        {
            _membershipRepo = new MembershipRepository();
            _customerRepo = new CustomerRepository();
            _membershipRepo.GetAllWithCustomers();
            Memberships = _membershipRepo.Memberships;
        }

        public void GetCustomerDetails(int id)
        {
            SelectedCustomer = _customerRepo.GetCustomer(id);
        }

    }
}
