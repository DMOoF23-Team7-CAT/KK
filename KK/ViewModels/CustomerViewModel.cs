using KK.Models.Entities;
using KK.Models.Helpers;
using KK.Models.Repositories;

namespace KK.ViewModels
{
    internal class CustomerViewModel : ObservableObject
    {

        private readonly CustomerRepository _customerRepo;
        private Customer _newCustomer;

        public Customer NewCustomer
        {
            get { return _newCustomer ?? (_newCustomer = new Customer()); }
            set
            {
                _newCustomer = value;
                OnPropertyChanged(nameof(NewCustomer));
            }
        }

        public CustomerViewModel()
        {
            _customerRepo = new CustomerRepository();
        }

        public void Add()
        {
            if (_customerRepo != null && NewCustomer != null)
            {
                _customerRepo.Add(NewCustomer);
            }
            else
            {
                
            }
        }







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
