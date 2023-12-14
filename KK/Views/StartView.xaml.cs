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
            DataContext = startVM;
            InitializeComponent();
            ExpandersDisabled();
            ResetValues();
        }

        // Uses Model.Entity to set Customer
        private void lv_Customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_Customers.SelectedItem != null)
            {
                ResetValues();
                startVM.SelectedCustomer = (Customer)lv_Customers.SelectedItem;
                startVM.GetDataForSelectedCustomer();

                // Opens and closes expanders
                if (startVM.SelectedCustomer.Membership == null)
                {
                    // If the customer does not have a membership, expand the expanders
                    ExpandersEnabled();
                }
                else
                {
                    // If the customer has a membership, do not expand the expanders
                    ExpandersDisabled();
                }
            }
        }

        // Expander methods
        private void ExpandersEnabled()
        {
            ep_Member.IsExpanded = true;
            ep_Ticket.IsExpanded = true;
            ep_Equipment.IsExpanded = true;
        }
        private void ExpandersDisabled()
        {
            ep_Member.IsExpanded = false;
            ep_Ticket.IsExpanded = false;
            ep_Equipment.IsExpanded = false;
        }

        // Search method for the Customers listview
        private void tb_SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(lv_Customers.ItemsSource);

            if (view != null)
            {
                view.Filter = string.IsNullOrWhiteSpace(tb_SearchBox.Text)
                    ? (Predicate<object>)null
                    : item => (item is Customer customer) && customer.Name.Contains(tb_SearchBox.Text, StringComparison.OrdinalIgnoreCase);
            }
        }


        // Checkind
        private void bt_CheckIn_Click(object sender, RoutedEventArgs e)
        {

        }

        // Resets all gui values
        private void ResetValues()
        {
            cb_12months.IsChecked = false;
            cb_3months.IsChecked = false;
            cb_1month.IsChecked = false;
            lbl_Tentimes.Content = 0;
            lbl_Child.Content = 0;
            lbl_Day.Content = 0;
            lbl_Shoes.Content = 0;
            lbl_Rope.Content = 0;
            lbl_Harness.Content = 0;
        }

        // Click event to open new customer dialog
        private void bt_NewCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerView customerView = new CustomerView((MainWindow)Application.Current.MainWindow);
            Opacity = 0.6;
            customerView.ShowDialog();
            Opacity = 1;
        }

        // Add Membership
        private void cb_12months_Checked(object sender, RoutedEventArgs e)
        {
            cb_3months.IsChecked = false;
            cb_1month.IsChecked = false;
            startVM.AddServiceItem("YEAR");
        }
        private void cb_3months_Checked(object sender, RoutedEventArgs e)
        {
            cb_12months.IsChecked = false;
            cb_1month.IsChecked = false;
            startVM.AddServiceItem("QUARTER");
        }
        private void cb_1month_Checked(object sender, RoutedEventArgs e)
        {
            cb_12months.IsChecked = false;
            cb_3months.IsChecked = false;
            startVM.AddServiceItem("MONTH");

        }

        // Remove membership
        private void cb_12months_Unchecked(object sender, RoutedEventArgs e)
        {
            startVM.RemoveServiceItem("YEAR");
        }
        private void cb_3months_Unchecked(object sender, RoutedEventArgs e)
        {
            startVM.RemoveServiceItem("QUARTER");
        }
        private void cb_1month_Unchecked(object sender, RoutedEventArgs e)
        {
            startVM.RemoveServiceItem("MONTH");
        }

        // Add Tickets
        private void rb_TentimesPlus_Click(object sender, RoutedEventArgs e)
        {
            startVM.AddServiceItem("TENTIMES");
            lbl_Tentimes.Content = PlusCount(lbl_Tentimes.Content.ToString());
        }
        private void rb_ChildPlus_Click(object sender, RoutedEventArgs e)
        {
            startVM.AddServiceItem("CHILD");
            lbl_Child.Content = PlusCount(lbl_Child.Content.ToString());
        }
        private void rb_DayPlus_Click(object sender, RoutedEventArgs e)
        {
            startVM.AddServiceItem("DAY");
            lbl_Day.Content = PlusCount(lbl_Day.Content.ToString());
        }

        // Remove Tickets
        private void rb_TentimesMinus_Click(object sender, RoutedEventArgs e)
        {
            startVM.RemoveServiceItem("TENTIMES");
            lbl_Tentimes.Content = MinusCount(lbl_Tentimes.Content.ToString());
        }
        private void rb_ChildMinus_Click(object sender, RoutedEventArgs e)
        {
            startVM.RemoveServiceItem("CHILD");
            lbl_Child.Content = MinusCount(lbl_Child.Content.ToString());
        }
        private void rb_DayMinus_Click(object sender, RoutedEventArgs e)
        {
            startVM.RemoveServiceItem("DAY");
            lbl_Day.Content = MinusCount(lbl_Day.Content.ToString());
        }

        // Add Equptment
        private void rb_ShoesPlus_Click(object sender, RoutedEventArgs e)
        {
            startVM.AddServiceItem("SHOES");
            lbl_Shoes.Content = PlusCount(lbl_Shoes.Content.ToString());
        }
        private void rb_RopePlus_Click(object sender, RoutedEventArgs e)
        {
            startVM.AddServiceItem("ROPE");
            lbl_Rope.Content = PlusCount(lbl_Rope.Content.ToString());
        }
        private void rb_HarnessPlus_Click(object sender, RoutedEventArgs e)
        {
            startVM.AddServiceItem("HARNESS");
            lbl_Harness.Content = PlusCount(lbl_Harness.Content.ToString());
        }

        // Remove Equipment
        private void rb_ShoesMinus_Click(object sender, RoutedEventArgs e)
        {
            startVM.RemoveServiceItem("SHOES");
            lbl_Shoes.Content = MinusCount(lbl_Shoes.Content.ToString());
        }
        private void rb_RopeMinus_Click(object sender, RoutedEventArgs e)
        {
            startVM.RemoveServiceItem("ROPE");
            lbl_Rope.Content = MinusCount(lbl_Rope.Content.ToString());
        }
        private void rb_HarnessMinus_Click(object sender, RoutedEventArgs e)
        {
            startVM.RemoveServiceItem("HARNESS");
            lbl_Harness.Content = MinusCount(lbl_Harness.Content.ToString());
        }

        // count methods
        private string MinusCount(string labelCount)
        {
            int count = Convert.ToInt32(labelCount);

            if(count > 0)
            {
                count--;
            }
            return count.ToString();
        }
        private string PlusCount(string labelCount)
        {
            int count = Convert.ToInt32(labelCount);

            count++;
            
            return count.ToString();
        }


    }
}

