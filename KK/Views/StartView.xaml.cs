using KK.Models.Entities;
using KK.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
        }

        // Uses Model.Entity to set Customer
        private void lv_Overview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_Overview.SelectedItem != null)
            {
                startVM.SelectedCustomer = (Customer)lv_Overview.SelectedItem;
            }
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


    }
}

