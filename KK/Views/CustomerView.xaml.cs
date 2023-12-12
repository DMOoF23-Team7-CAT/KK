using KK.Models.Entities;
using KK.Models.Entities.Enum;
using KK.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace KK.Views
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class CustomerView : Window
    {


        private readonly CustomerViewModel customerVM = new();
        private StartView startView;

        public CustomerView(Window parentWindow)
        {
            Owner = parentWindow;
            DataContext = customerVM;
            InitializeComponent();
        }

        public CustomerView(MainWindow mainWindow)
        {
            this.Owner = mainWindow;
            DataContext = customerVM;
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void bt_AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            int qualification = 0;

            if ((bool)cb_top.IsChecked)
            {
                qualification = 1;
            }
            else if ((bool)cb_lead.IsChecked)
            {
                qualification = 2;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(tb_TxtName.Text))
                {
                    MessageBox.Show("Navn skal udfyldes", "Advarsel", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; 
                }
                if (dp_DateOfBirth.SelectedDate == null)
                {
                    MessageBox.Show("Fødselsdato skal udfyldes", "Advarsel", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                customerVM.NewCustomerName = tb_TxtName.Text;
                customerVM.NewCustomerDateOfBirth = (System.DateTime)dp_DateOfBirth.SelectedDate;
                customerVM.NewCustomerPhone = tb_TxtPhone.Text;
                customerVM.NewCustomerEmail = tb_TxtMail.Text;
                customerVM.NewCustomerHasSignedDisclaimer = (bool)cb_disclaimer.IsChecked;
                customerVM.NewCustomerQualification = qualification;

                MessageBoxResult result = MessageBox.Show("Kunden blev oprettet", "Kunde oprettet", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    customerVM.Add();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fej ved oprettelse af ny kunde generede fejlkode \n\n\n{ex.Message}", "Fejl ved oprettelse", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



    }
}
