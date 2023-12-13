using KK.Models.Entities;
using KK.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace KK.Views
{
    /// <summary>
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : UserControl
    {
        StartViewModel startVM;
        public StartView()
        {
            startVM = new StartViewModel();
            InitializeComponent();
            ExpandersEnabled();
        }

        // Uses Model.Entity to set Customer
        private void lv_Overview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_Overview.SelectedItem != null)
            {
                Customer selectedCustomer = (Customer)lv_Overview.SelectedItem;

                if (selectedCustomer.Membership == null)
                {
                    // If the customer does not have a membership, expand the expanders
                    ExpandersEnabled();
                }
                else
                {
                    // If the customer has a membership, do not expand the expanders
                    ep_Member.IsExpanded = false;
                    ep_Ticket.IsExpanded = false;
                    ep_Equipment.IsExpanded = false;
                }

                startVM.SelectedCustomer = selectedCustomer;
            }
        }

        private void ExpandersEnabled()
        {
            ep_Member.IsExpanded = true;
            ep_Ticket.IsExpanded = true;
            ep_Equipment.IsExpanded = true;
        }

        // Uses Model.Entity to set Customer
        private void tb_SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(lv_Overview.ItemsSource);

            if (view != null)
            {
                view.Filter = string.IsNullOrWhiteSpace(tb_SearchBox.Text)
                    ? (Predicate<object>)null
                    : item => (item is Customer customer) && customer.Name.Contains(tb_SearchBox.Text, StringComparison.OrdinalIgnoreCase);
            }
        }

        private void bt_CheckIn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bt_NewCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerView customerView = new CustomerView((MainWindow)Application.Current.MainWindow);
            Opacity = 0.6;
            customerView.ShowDialog();
            Opacity = 1;
        }

        private void cb_12months_Checked(object sender, RoutedEventArgs e)
        {
            cb_3months.IsChecked = false;
            cb_1month.IsChecked = false;
        }

        private void cb_3months_Checked(object sender, RoutedEventArgs e)
        {
            cb_12months.IsChecked = false;
            cb_1month.IsChecked = false;
        }

        private void cb_1month_Checked(object sender, RoutedEventArgs e)
        {
            cb_12months.IsChecked = false;
            cb_3months.IsChecked = false;
        }
    }
}

