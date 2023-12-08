using KK.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace KK.Views
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Disclaimer { get; set; }
        public string Lead { get; set; }
        public string Top { get; set; }



        private readonly CustomerViewModel customerVM = new();

        public Test(Window parentWindow)
        {
            Owner = parentWindow;
            DataContext = customerVM;
            InitializeComponent();
        }


        // Think we should changes this to style triggers and move it into Themes and xaml
        private void BorderYear_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            DateTime today = DateTime.Now;
            DateTime expiryDate = today.AddDays(365);

            tb_txtMembershipExpiry.Text = $"{expiryDate:dd/MM/yyyy}";
            tb_TxtStartDate.Text = $"{today:dd/MM/yyyy}";
            tb_txtMembershipStart.Text = $"{today:dd/MM/yyyy}";

            if (bb_YearMembership.BorderThickness == new Thickness(0, 0, 4, 4))
            {
                bb_YearMembership.BorderThickness = new Thickness(3, 3, 3, 3);
                bb_YearMembership.BorderBrush = new SolidColorBrush(Colors.Beige);

                bb_QuaterMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_QuaterMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_QuaterMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;

                bb_MonthMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_MonthMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_MonthMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
            }
            else
            {
                tb_txtMembershipExpiry.Text = string.Empty;
                tb_TxtStartDate.Text = string.Empty;
                tb_txtMembershipStart.Text = string.Empty;

                bb_YearMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_YearMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_YearMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
            }
        }

        private void BorderQuarter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            DateTime today = DateTime.Now;


            DateTime expiryDate = today.AddDays(90);


            tb_txtMembershipExpiry.Text = $"{expiryDate:dd/MM/yyyy}";
            tb_TxtStartDate.Text = $"{today:dd/MM/yyyy}";
            tb_txtMembershipStart.Text = $"{today:dd/MM/yyyy}";

            if (bb_QuaterMembership.BorderThickness == new Thickness(0, 0, 4, 4))
            {
                bb_QuaterMembership.BorderThickness = new Thickness(3, 3, 3, 3);
                bb_QuaterMembership.BorderBrush = new SolidColorBrush(Colors.Beige);

                bb_YearMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_YearMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_YearMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;

                bb_MonthMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_MonthMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_MonthMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
            }
            else
            {
                tb_txtMembershipExpiry.Text = string.Empty;
                tb_TxtStartDate.Text = string.Empty;
                tb_txtMembershipStart.Text = string.Empty;

                bb_QuaterMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_QuaterMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_QuaterMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
            }
        }

        private void BorderMonth_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DateTime today = DateTime.Now;


            DateTime expiryDate = today.AddDays(30);


            tb_txtMembershipExpiry.Text = $"{expiryDate:dd/MM/yyyy}";
            tb_TxtStartDate.Text = $"{today:dd/MM/yyyy}";
            tb_txtMembershipStart.Text = $"{today:dd/MM/yyyy}";

            if (bb_MonthMembership.BorderThickness == new Thickness(0, 0, 4, 4))
            {
                bb_MonthMembership.BorderThickness = new Thickness(3, 3, 3, 3);
                bb_MonthMembership.BorderBrush = new SolidColorBrush(Colors.Beige);

                bb_QuaterMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_QuaterMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_QuaterMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;

                bb_YearMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_YearMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_YearMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
            }
            else
            {
                tb_txtMembershipExpiry.Text = string.Empty;
                tb_TxtStartDate.Text = string.Empty;
                tb_txtMembershipStart.Text = string.Empty;

                bb_MonthMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_MonthMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_MonthMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Name = tb_TxtName.Text;
            DateOfBirth = tb_TxtBirthday.Text;
            Phone = tb_TxtPhone.Text;
            Email = tb_TxtMail.Text;
        }


        private void TopState()
        {
            if (cb_top.IsChecked == true)
            {
                Top = "Top-reb";
            }
            else
            {
                Top = string.Empty;
            }
        }

        private void LeadState()
        {
            if (cb_lead.IsChecked == true)
            {
                Lead = "Lead sikret";
            }
            else
            {
                Lead = string.Empty;
            }
        }

        private void DisclaimerState()
        {
            if (cb_disclaimer.IsChecked == true) 
            {
                Disclaimer = "Signeret";
            }
            else
            {
                Disclaimer = "Ikke Signeret";
            }
        }
    }
}
