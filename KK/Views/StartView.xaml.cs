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
        // private backing fields
        private readonly StartViewModel startVM;

        // Constructor
        public StartView()
        {
            startVM = new StartViewModel();
            DataContext = startVM;
            InitializeComponent();
            ExpandersDisabled();
            ResetAllValuesInUI();
        }

        // Event for Customers listview Selection Changed that resets all and sets the Selected customer 
        private void lv_Customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_Customers.SelectedItem != null)
            {
                startVM.ResetSelectedObjects();
                ResetAllValuesInUI();
                startVM.SelectedCustomer = (Customer)lv_Customers.SelectedItem;
                startVM.GetDataForSelectedCustomer();
            }
        }

        // ServiceItems Expander opening methods, sets new entry
        private void ExpandersEnabled()
        {
            ep_Member.IsExpanded = true;
            ep_Ticket.IsExpanded = true;
            ep_Equipment.IsExpanded = true;
        }
        // ServiceItems Expander closing methods
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

        // Methods to enable or disable buttons
        private void EnableDisableCheckInButton()
        {
            if (startVM.EntryItemsList.Count > 0)
            {
                bt_CheckMemberIn.IsEnabled = false;
            }
            else
            {
                bt_CheckMemberIn.IsEnabled = true;
            }
        }
        private void EnableDisablePayButton()
        {
            if (startVM.EntryItemsList.Count > 0)
            {
                bt_pay.IsEnabled = true;
            }
            else
            {
                bt_pay.IsEnabled = false;
            }
        }

        // Check customer in button click event
        private void bt_CheckMemberIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckMemberIn();
                MessageBox.Show(
                    "Medlem er tjekked ind",
                    "Medlem tjek ind", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                     $"Der skete en fejl ved intjekningen af medlem \n\n\n\n{ex.Message}",
                     "fejl ved indtjekning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            
        }
        // Pay button click event
        private void bt_pay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddServiceItem();
                AddMembership();
                CheckCustomerOrMemberIn();
                MessageBox.Show(
                    "Betaling er gennemført og Kunden er tjekked ind",
                    "Kunde tjek ind", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                     $"Der skete en fejl ved intjekningen af kunden \n\n\n\n{ex.Message}",
                     "fejl ved indtjekning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ResetAllValuesInUI();
                startVM.ResetSelectedObjects();
            }
        }

        // Resets all gui values
        private void ResetAllValuesInUI()
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
            tb_Total.Text = string.Empty;
            bt_pay.IsEnabled = false;
            bt_CheckMemberIn.IsEnabled = false;
            lv_Items.Items.Clear();
        }

        // Set PaymentFields
        private void SetPaymentFields()
        {
            tb_Total.Text = startVM.SelectedEntry.Price.ToString();
            EnableDisableCheckInButton();
            EnableDisablePayButton();
        }

        // Click event to open new customer dialog
        private void bt_NewCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerView customerView = new CustomerView((MainWindow)Application.Current.MainWindow);
            Opacity = 0.6;
            customerView.ShowDialog();
            Opacity = 1;
        }

        // Method to Add checkcustomer or member in
        private void CheckCustomerOrMemberIn()
        {
            try
            {
                startVM.CheckCustomerInWithItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Der skete en fejl ved intjekningen af kunden \n\n\n\n{ex.Message}",
                    "fejl ved indtjekning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Method to Add checkcustomer or member in
        private void CheckMemberIn()
        {
            try
            {
                startVM.CheckMemberIn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Der skete en fejl ved intjekningen af Medlem \n\n\n\n{ex.Message}",
                    "fejl ved indtjekning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Method that checks if membership boxes are checked and then adds membership
        private void AddMembership()
        {
            try
            {
                if (cb_12months.IsChecked == true || cb_3months.IsChecked == true || cb_1month.IsChecked == true)
                {
                    startVM.SetMembership();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Der skete en fejl ved tilføjelsen medlemskab\n\n\n\n{ex.Message}",
                    "fejl ved Tilføjelse af medlemskab", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        // Add ServiceItem
        private void AddServiceItem()
        {
            try
            {
                startVM.AddServiceItemToRepo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Der skete en fejl ved tilføjelsen af ekstra tjenester\n\n\n\n{ex.Message}",
                    "fejl ved Tilføjelse af udstyr", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Add Membership
        private void cb_12months_Checked(object sender, RoutedEventArgs e)
        {
            cb_3months.IsChecked = false;
            cb_1month.IsChecked = false;
            startVM.AddServiceItem("YEAR");
            SetPaymentFields();
        }
        private void cb_3months_Checked(object sender, RoutedEventArgs e)
        {
            cb_12months.IsChecked = false;
            cb_1month.IsChecked = false;
            startVM.AddServiceItem("QUARTER");
            SetPaymentFields();
        }
        private void cb_1month_Checked(object sender, RoutedEventArgs e)
        {
            cb_12months.IsChecked = false;
            cb_3months.IsChecked = false;
            startVM.AddServiceItem("MONTH");
            SetPaymentFields();

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
            SetPaymentFields();
            int count = Convert.ToInt32(labelCount);

            if(count > 0)
            {
                count--;
            }
            return count.ToString();
        }
        private string PlusCount(string labelCount)
        {
            SetPaymentFields();
            int count = Convert.ToInt32(labelCount);

            count++;
            
            return count.ToString();
        }

        // Expander events
        private void ep_Member_Expanded(object sender, RoutedEventArgs e)
        {
            ExpandersEnabled();
        }
        private void ep_Member_Collapsed(object sender, RoutedEventArgs e)
        {
            ExpandersDisabled();
        }
        private void ep_Ticket_Expanded(object sender, RoutedEventArgs e)
        {
            ExpandersEnabled();
        }
        private void ep_Ticket_Collapsed(object sender, RoutedEventArgs e)
        {
            ExpandersDisabled();
        }
        private void ep_Equipment_Expanded(object sender, RoutedEventArgs e)
        {
            ExpandersEnabled();
        }
        private void ep_Equipment_Collapsed(object sender, RoutedEventArgs e)
        {
            ExpandersDisabled();
        }
        // Click event for Add item button
        private void bt_AddItem_Click(object sender, RoutedEventArgs e)
        {
            if (startVM.SelectedCustomer != null)
            {
                try
                {
                    startVM.SetSelectedEntry();
                    ExpandersEnabled();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Der skete en fejl sætningen af indgang \n\n\n\n{ex.Message}",
                        "fejl ved sætte indgang", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        // click event for Cancel button
        private void bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                startVM.ResetSelectedObjects();
                ResetAllValuesInUI();
                startVM.RemoveEntry();
                ExpandersDisabled();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Der skete en da indagangen blev fjernet \n\n\n\n{ex.Message}",
                    "fejl ved indgang fjernelse", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

