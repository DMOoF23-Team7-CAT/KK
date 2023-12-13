using KK.Models.Entities;
using KK.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KK.Views
{
    /// <summary>
    /// Interaction logic for OverviewView.xaml
    /// </summary>
    public partial class OverviewView : UserControl
    {
        public OverviewViewModel overviewVM;
        public OverviewView()
        {
            overviewVM = new OverviewViewModel();
            DataContext = overviewVM;
            InitializeComponent();
            ClearAllTextBoxes();            

        }


        private void lv_Overview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_Overview.SelectedItem != null)
            {
                overviewVM.SelectedCustomer = (Customer)lv_Overview.SelectedItem;

                overviewVM.GetDataForSelectedCustomer();

                lv_ChekIn.ItemsSource = overviewVM.SelectedCustomer.Entries;
            }
            
        }



        private void bt_Update_Click(object sender, RoutedEventArgs e)
        {
            string name = tb_Name.Text;
            DateTime dob = Convert.ToDateTime(tb_DateOfBirth.Text);
            string phone = tb_Phone.Text;
            string email = tb_Email.Text;
            bool disclaimer = Convert.ToBoolean(cb_disclaimer.IsChecked);

            MessageBoxResult result = MessageBox.Show("Er du sikker på du vil opdatere kunden", "Opdater Kunde", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                overviewVM.UpdateCustomer(name, dob, phone, email, disclaimer);
            }
            return;

        }

        private void bt_Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Denne handling kan ikke fortrydes. Er du sikker på du vil slette kunden?", "Slet Kunden", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                overviewVM.DeleteCustomer();
                ClearAllTextBoxes();
            }
            return; 
        }

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

        public void ClearAllTextBoxes()
        {
            tb_SearchBox.Clear();
            tb_Name.Clear();
            tb_DateOfBirth.Clear();
            tb_Email.Clear();
            tb_Phone.Clear();
            tb_Qualification.Clear();
        }

       
     
    }
}
