using KK.Models.Entities;
using KK.ViewModels;
using Microsoft.VisualBasic;
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
        private readonly OverviewViewModel overviewVM;
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

                SetQualificationCheckbox();

                overviewVM.GetDataForSelectedCustomer();

                lv_ChekIn.ItemsSource = overviewVM.SelectedCustomer.Entries;
            }
            ep_Qualification.IsExpanded = false;

        }

        private void bt_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int qualification = 0;

                if (overviewVM.SelectedCustomer != null)
                {
                    if (cb_QualificationTop.IsChecked == true)
                    {
                        qualification = 1;
                    }
                    else if (cb_QualificationLead.IsChecked == true)
                    {
                        qualification = 2;
                    }

                    string name = tb_Name.Text;
                    DateTime dob = Convert.ToDateTime(tb_DateOfBirth.Text);
                    string phone = tb_Phone.Text;
                    string email = tb_Email.Text;
                    bool disclaimer = Convert.ToBoolean(cb_disclaimer.IsChecked);

                    MessageBoxResult result = MessageBox.Show("Er du sikker på du vil opdatere kunden", "Opdater Kunde", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        overviewVM.UpdateCustomer(name, dob, phone, email, qualification, disclaimer);
                    }
                    return;
                }
                else
                {
                    MessageBox.Show("Vælg først en kunde du vil opdatere", "Vælg kunde", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    $"Der skete en fejl ved opdatering af kunden \n\n\n\n{ex.Message}",
                    "fejl ved opdatering", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void bt_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (overviewVM.SelectedCustomer != null)
                {
                    MessageBoxResult result = MessageBox.Show("Denne handling kan ikke fortrydes. Er du sikker på du vil slette kunden?", "Slet Kunden", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        overviewVM.DeleteCustomer();
                        ClearAllTextBoxes();
                    }
                    return;
                }
                else
                {
                    MessageBox.Show("Vælg først en kunde du vil slette", "Vælg kunde", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Der skete en fejl ved sletning af kunden \n\n\n\n{ex.Message}",
                    "fejl ved sletning", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void ClearAllTextBoxes()
        {
            tb_SearchBox.Clear();
            tb_Name.Clear();
            tb_DateOfBirth.SelectedDate = DateTime.MinValue;
            tb_Email.Clear();
            tb_Phone.Clear();
            tb_Qualification.Clear();
        }


        private void SetQualificationCheckbox()
        {
            int qualification = (int)overviewVM.SelectedCustomer.Qualification;

            cb_QualificationNone.IsChecked = qualification == 0;
            cb_QualificationTop.IsChecked = qualification == 1;
            cb_QualificationLead.IsChecked = qualification == 2;

            tb_Qualification.Text = qualification switch
            {
                0 => "Ingen",
                1 => "TopReb",
                2 => "Lead",
                _ => throw new ArgumentOutOfRangeException(nameof(qualification), "Invalid qualification value"),
            };
        }

        private void cb_QualificationNone_Checked(object sender, RoutedEventArgs e)
        {
            cb_QualificationTop.IsChecked = false;
            cb_QualificationLead.IsChecked = false;
            tb_Qualification.Text = "Ingen";
        }

        private void cb_QualificationTop_Checked(object sender, RoutedEventArgs e)
        {
            cb_QualificationNone.IsChecked = false;
            cb_QualificationLead.IsChecked = false;
            tb_Qualification.Text = "TopReb";
        }

        private void cb_QualificationLead_Checked(object sender, RoutedEventArgs e)
        {
            cb_QualificationNone.IsChecked = false;
            cb_QualificationTop.IsChecked = false;
            tb_Qualification.Text = "Lead";
        }



        private void ep_Qualification_LostFocus(object sender, RoutedEventArgs e)
        {
            ep_Qualification.IsExpanded = false;
        }

    }
}
