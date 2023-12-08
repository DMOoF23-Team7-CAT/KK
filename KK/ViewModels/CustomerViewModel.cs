using KK.Models.Helpers;

namespace KK.ViewModels
{
    public class CustomerViewModel : ObservableObject
    {
        private string _boundCustomerName;

        public string BoundCustomerName
        {
            get { return _boundCustomerName; }
            set
            {
                _boundCustomerName = value;
                OnPropertyChanged();
            }
		}

        private string _boundCustomerDateOfBirth;

        public string BoundCustomerDateOfBirth
        {
            get { return _boundCustomerDateOfBirth; }
            set
            {
                _boundCustomerDateOfBirth = value;
                OnPropertyChanged();
            }
        }

        private string _boundCustomerPhone;

        public string BoundCustomerPhone
        {
            get { return _boundCustomerPhone; }
            set
            {
                _boundCustomerPhone = value;
                OnPropertyChanged();
            }
        }

        private string _boundCustomerEmail;

        public string BoundCustomerEmail
        {
            get { return _boundCustomerEmail; }
            set
            {
                _boundCustomerEmail = value;
                OnPropertyChanged();
            }
        }

        private bool _boundCustomerHasSignedDisclaimer;

        public bool BoundCustomerHasSignedDisclaimer
        {
            get { return _boundCustomerHasSignedDisclaimer; }
            set
            {
                _boundCustomerHasSignedDisclaimer = value;
                OnPropertyChanged(nameof(BoundCustomerHasSignedDisclaimer));
            }
        }












    }
}
