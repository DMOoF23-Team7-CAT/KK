using KK.Models.Entities;
using KK.Models.Entities.Enum;
using KK.Models.Helpers;
using KK.Models.Repositories;
using System;

namespace KK.ViewModels
{
    internal class CustomerViewModel : ObservableObject
    {

        private readonly CustomerRepository _customerRepo;
        private Customer _newCustomer;

        public Customer NewCustomer
        {
            get { return _newCustomer; }
            set
            {
                _newCustomer = value;
                OnPropertyChanged(nameof(NewCustomer));
            }
        }

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
            SetNewCustomer();
            _customerRepo.Add(NewCustomer);
        }

        private void SetNewCustomer()
        {
            _newCustomer.Name = NewCustomerName;
            _newCustomer.Phone = NewCustomerPhone;
            _newCustomer.Email = NewCustomerEmail;
            _newCustomer.DateOfBirth = NewCustomerDateOfBirth;
            _newCustomer.HasSignedDisclaimer = NewCustomerHasSignedDisclaimer;

            if (Enum.TryParse(typeof(Qualification), NewCustomerQualification.ToString(), out var parsedQualification))
            {
                NewCustomer.Qualification = (Qualification)parsedQualification;
            }
            else
            {
                NewCustomer.Qualification = Qualification.None;
            }
        }

        
        /*        public void Add(string name, DateTime dateOfBirth, string phone, string email, bool disclaimer, Qualification qualification)
                {
                    NewCustomer.Name = name;
                    NewCustomer.Phone = phone;
                    NewCustomer.Email = email;
                    NewCustomer.HasSignedDisclaimer = disclaimer;
                    NewCustomer.Qualification = qualification;

                    _customerRepo.Add(NewCustomer);

                }*/




        //      private string _boundCustomerName;

        //      public string BoundCustomerName
        //      {
        //          get { return _boundCustomerName; }
        //          set
        //          {
        //              _boundCustomerName = value;
        //              OnPropertyChanged();
        //          }
        //}

        //      private string _boundCustomerDateOfBirth;

        //      public string BoundCustomerDateOfBirth
        //      {
        //          get { return _boundCustomerDateOfBirth; }
        //          set
        //          {
        //              _boundCustomerDateOfBirth = value;
        //              OnPropertyChanged();
        //          }
        //      }

        //      private string _boundCustomerPhone;

        //      public string BoundCustomerPhone
        //      {
        //          get { return _boundCustomerPhone; }
        //          set
        //          {
        //              _boundCustomerPhone = value;
        //              OnPropertyChanged();
        //          }
        //      }

        //      private string _boundCustomerEmail;

        //      public string BoundCustomerEmail
        //      {
        //          get { return _boundCustomerEmail; }
        //          set
        //          {
        //              _boundCustomerEmail = value;
        //              OnPropertyChanged();
        //          }
        //      }

        //      private bool _boundCustomerHasSignedDisclaimer;

        //      public bool BoundCustomerHasSignedDisclaimer
        //      {
        //          get { return _boundCustomerHasSignedDisclaimer; }
        //          set
        //          {
        //              _boundCustomerHasSignedDisclaimer = value;
        //              OnPropertyChanged(nameof(BoundCustomerHasSignedDisclaimer));
        //          }
        //      }












    }
}
