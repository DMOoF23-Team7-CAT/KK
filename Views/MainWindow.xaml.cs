using KK.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace KK.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel mainVM;
        public MainWindow()
        {
            mainVM = new MainViewModel();
            DataContext = mainVM;
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
           // mainVM.CreateCustomer();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
           // mainVM.UpdateCustomer();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
           // mainVM.DeleteCustomer();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            mainVM.GetCustomer(Convert.ToInt32(tb_Search.Text.ToString()));
        }
    }
}
