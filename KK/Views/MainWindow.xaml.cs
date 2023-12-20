using KK.ViewModels;
using System.Windows;
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

        private void bt_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool IsMaximized = false;

        private void bt_Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }



        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
      

    }
}
