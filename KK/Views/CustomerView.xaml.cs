using KK.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : UserControl
    {
        private CustomerViewModel customerVM = new CustomerViewModel();

        public CustomerView()
        {
            DataContext = customerVM;
            InitializeComponent();
        }


        // Think we should changes this to style triggers and move it into Themes and xaml
        private void BorderYear_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (bb_YearMembership.BorderThickness == new Thickness(0, 0, 4, 4))
            {
                bb_YearMembership.BorderThickness = new Thickness(3, 3, 3, 3);
                bb_YearMembership.BorderBrush = new SolidColorBrush(Colors.Beige);

                bb_QuaterMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_QuaterMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_QuaterMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;

                bb_MonthMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_MonthMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_MonthMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
            }
            else
            {
                bb_YearMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_YearMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_YearMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
            }
        }

        private void BorderQuarter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (bb_QuaterMembership.BorderThickness == new Thickness(0, 0, 4, 4))
            {
                bb_QuaterMembership.BorderThickness = new Thickness(3, 3, 3, 3);
                bb_QuaterMembership.BorderBrush = new SolidColorBrush(Colors.Beige);

                bb_YearMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_YearMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_YearMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;

                bb_MonthMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_MonthMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_MonthMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
            }
            else
            {
                bb_QuaterMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_QuaterMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_QuaterMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
            }
        }

        private void BorderMonth_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (bb_MonthMembership.BorderThickness == new Thickness(0, 0, 4, 4))
            {
                bb_MonthMembership.BorderThickness = new Thickness(3, 3, 3, 3);
                bb_MonthMembership.BorderBrush = new SolidColorBrush(Colors.Beige);

                bb_QuaterMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_QuaterMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_QuaterMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;

                bb_YearMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_YearMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_YearMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
            }
            else
            {
                bb_MonthMembership.BorderThickness = new Thickness(0, 0, 4, 4);
                bb_MonthMembership.BorderBrush = new SolidColorBrush(Colors.Gray);
                (bb_MonthMembership.BorderBrush as SolidColorBrush).Opacity = 0.2;
            }
        }
    }
}
