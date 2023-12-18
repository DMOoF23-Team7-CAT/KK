using KK.Models.Entities;
using KK.Models.Entities.Enum;
using KK.Models.Helpers;
using KK.Models.Repositories;
using System;

namespace KK.ViewModels
{
    internal class CustomerViewModel
    {

        private readonly CustomerRepository _customerRepo;

        public string NewCustomerName { get; set; }
        public string NewCustomerPhone { get; set; }
        public string NewCustomerEmail { get; set; }
        public DateTime NewCustomerDateOfBirth { get; set; }
        public bool NewCustomerHasSignedDisclaimer { get; set; }
        public int NewCustomerQualification {  get; set; }

        public CustomerViewModel()
        {
            _customerRepo = new CustomerRepository();
        }

        public void Add()
        {
            _customerRepo.Add(SetNewCustomer());
        }

        private Customer SetNewCustomer()
        {
            Qualification qualification = new Qualification();

            if (Enum.TryParse(typeof(Qualification), NewCustomerQualification.ToString(), out var parsedQualification))
            {
                qualification = (Qualification)parsedQualification;
            }
            else
            {
                qualification = Qualification.None;
            }

            Customer customer = new Customer
            {
                Name = NewCustomerName,
                Phone = NewCustomerPhone,
                Email = NewCustomerEmail,
                DateOfBirth = NewCustomerDateOfBirth,
                HasSignedDisclaimer = NewCustomerHasSignedDisclaimer,
                Qualification = qualification
            };

            return customer;
        }


    }
}
