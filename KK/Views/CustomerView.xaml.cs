using KK.Models.Entities;
using KK.Models.Entities.Enum;
using KK.ViewModels;
using System;
using System.Windows;

namespace KK.Views
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class CustomerView : Window
    {


        private readonly CustomerViewModel customerVM = new();

        public CustomerView(Window parentWindow)
        {
            Owner = parentWindow;
            DataContext = customerVM;
            InitializeComponent();
        }


        //// Think we should changes this to style triggers and move it into Themes and xaml
        //private void BorderYear_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{

        //    DateTime today = DateTime.Now;
        //    DateTime expiryDate = today.AddDays(365);

        //    tb_txtMembershipExpiry.Text = $"{expiryDate:dd/MM/yyyy}";
        //    tb_TxtStartDate.Text = $"{today:dd/MM/yyyy}";
        //    tb_txtMembershipStart.Text = $"{today:dd/MM/yyyy}";

        //    if (bb_YearMembership.BorderThickness == new Thickness(0, 0, 4, 4))
        //    {
        //        bb_YearMembership.BorderThickness = new Thickness(3, 3, 3, 3);
        //        bb_YearMembership.BorderBrush = new SolidColorBrush(Colors.Beige);

        //        bb_QuaterMembership.BorderThickness = new Thickness(0, 0, 4, 4);
        //        bb_QuaterMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
        //        (bb_QuaterMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;

        //        bb_MonthMembership.BorderThickness = new Thickness(0, 0, 4, 4);
        //        bb_MonthMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
        //        (bb_MonthMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
        //    }
        //    else
        //    {
        //        tb_txtMembershipExpiry.Text = string.Empty;
        //        tb_TxtStartDate.Text = string.Empty;
        //        tb_txtMembershipStart.Text = string.Empty;

        //        bb_YearMembership.BorderThickness = new Thickness(0, 0, 4, 4);
        //        bb_YearMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
        //        (bb_YearMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
        //    }
        //}

        //private void BorderQuarter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{

        //    DateTime today = DateTime.Now;


        //    DateTime expiryDate = today.AddDays(90);


        //    tb_txtMembershipExpiry.Text = $"{expiryDate:dd/MM/yyyy}";
        //    tb_TxtStartDate.Text = $"{today:dd/MM/yyyy}";
        //    tb_txtMembershipStart.Text = $"{today:dd/MM/yyyy}";

        //    if (bb_QuaterMembership.BorderThickness == new Thickness(0, 0, 4, 4))
        //    {
        //        bb_QuaterMembership.BorderThickness = new Thickness(3, 3, 3, 3);
        //        bb_QuaterMembership.BorderBrush = new SolidColorBrush(Colors.Beige);

        //        bb_YearMembership.BorderThickness = new Thickness(0, 0, 4, 4);
        //        bb_YearMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
        //        (bb_YearMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;

        //        bb_MonthMembership.BorderThickness = new Thickness(0, 0, 4, 4);
        //        bb_MonthMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
        //        (bb_MonthMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
        //    }
        //    else
        //    {
        //        tb_txtMembershipExpiry.Text = string.Empty;
        //        tb_TxtStartDate.Text = string.Empty;
        //        tb_txtMembershipStart.Text = string.Empty;

        //        bb_QuaterMembership.BorderThickness = new Thickness(0, 0, 4, 4);
        //        bb_QuaterMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
        //        (bb_QuaterMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
        //    }
        //}

        //private void BorderMonth_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    DateTime today = DateTime.Now;


        //    DateTime expiryDate = today.AddDays(30);


        //    tb_txtMembershipExpiry.Text = $"{expiryDate:dd/MM/yyyy}";
        //    tb_TxtStartDate.Text = $"{today:dd/MM/yyyy}";
        //    tb_txtMembershipStart.Text = $"{today:dd/MM/yyyy}";

        //    if (bb_MonthMembership.BorderThickness == new Thickness(0, 0, 4, 4))
        //    {
        //        bb_MonthMembership.BorderThickness = new Thickness(3, 3, 3, 3);
        //        bb_MonthMembership.BorderBrush = new SolidColorBrush(Colors.Beige);

        //        bb_QuaterMembership.BorderThickness = new Thickness(0, 0, 4, 4);
        //        bb_QuaterMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
        //        (bb_QuaterMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;

        //        bb_YearMembership.BorderThickness = new Thickness(0, 0, 4, 4);
        //        bb_YearMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
        //        (bb_YearMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
        //    }
        //    else
        //    {
        //        tb_txtMembershipExpiry.Text = string.Empty;
        //        tb_TxtStartDate.Text = string.Empty;
        //        tb_txtMembershipStart.Text = string.Empty;

        //        bb_MonthMembership.BorderThickness = new Thickness(0, 0, 4, 4);
        //        bb_MonthMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
        //        (bb_MonthMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
        //    }
        //}


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

/*        private void bt_AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Kunden blev oprettet", "Kunde oprettet", MessageBoxButton.OK, MessageBoxImage.Information);
            if (result == MessageBoxResult.OK)
            {
                customerVM.Add();
            }
        }*/

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
            }
        }


    }
}
