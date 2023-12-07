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

        private void dg_Overview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_Overview.SelectedItem != null)
            {
                overviewVM.SelectedCustomer = (Customer)dg_Overview.SelectedItem;
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
            ICollectionView view = CollectionViewSource.GetDefaultView(dg_Overview.ItemsSource);

            if (view is not null)
            {
                // If the search box is empty, show all items
                if (string.IsNullOrWhiteSpace(tb_SearchBox.Text))
                {
                    view.Filter = null; // Show all items
                }
                else
                {
                    // Filters customers based on name
                    view.Filter = item => ((Customer)item).Name.Contains(tb_SearchBox.Text, StringComparison.OrdinalIgnoreCase);
                }
            }
        }

        public void ClearAllTextBoxes()
        {
            tb_SearchBox.Clear();
            tb_Name.Clear();
            tb_DOB.Clear();
            tb_Email.Clear();
            tb_Phone.Clear();
            tb_Qualification.Clear();
        }
    }
}
