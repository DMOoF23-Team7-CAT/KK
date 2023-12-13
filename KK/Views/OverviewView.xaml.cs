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
                //Listview for entries is sat to selected item 
                lv_ChekIn.ItemsSource = overviewVM.SelectedCustomer.Entries;
            }
            
        }      



        private void bt_Update_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Kunden blev opdateret", "Kunde opdateret", MessageBoxButton.OK, MessageBoxImage.Information);

            if (result == MessageBoxResult.OK)
            {
                overviewVM.UpdateCustomer();
            }

        }

        private void bt_Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Denne handling kan ikke fortrydes. Er du sikker på du vil slette kunden?", "Slet kunden", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                overviewVM.DeleteCustomer();
                ClearAllTextBoxes();
            }

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
            //tb_DOB.Clear();
            tb_Email.Clear();
            tb_Phone.Clear();
            tb_Qualification.Clear();
        }

       
     
    }
}
