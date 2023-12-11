using KK.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

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

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool IsMaximized = false;

        private void btn_Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (IsMaximized)
            {
                this.WindowState = WindowState.Normal;
                this.Height = 784;
                this.Width = 1366;

                IsMaximized = false;
            }
            else
            {
                this.WindowState = WindowState.Maximized;

                IsMaximized = true;
            }
        }

        private void btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }



        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Border_DoubleMouseLeftButtonClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Height = 784;
                    this.Width = 1366;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
            }
        }


        //--------------------------------------------------------------------------------TEST------------------------------------------------------------------------------

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Test test = new Test(this);
            Opacity = 0.8;
            test.ShowDialog();
            Opacity = 1;
        }
    }
}
