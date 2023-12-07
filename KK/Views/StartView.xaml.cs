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


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(dg_CheckIn.ItemsSource);

            if (view is not null)
            {
                view.Filter = item =>
                {
                    if (item is Membership membership)
                    {
                        // Replace with the actual property path in your Membership class
                        string customerName = membership.Customer?.Name ?? string.Empty;

                        return customerName.IndexOf(tb_CheckIn.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                    }

                    return false;
                };
            }
        }


    }
}

