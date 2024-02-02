using System.Windows;

namespace Guard
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Manager.Frame = MainFrame;
            Reader.serialPort("COM3");
            MainFrame.Navigate(Manager.Monitoring = new());
        }
        private void MonitoringBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            MainFrame.Navigate(Manager.Monitoring = new());
        }
        private void EventsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Events());
        }
        private void AdminBtn_Click(object sender, RoutedEventArgs e)
        {
            Auntefication auntefication = new();
            if (auntefication.ShowDialog() == true)
            {
                if (auntefication.Password == "0000")
                {
                    KeyBtn.Visibility = Visibility.Visible;
                    OwnersBtn.Visibility = Visibility.Visible;
                    SettingsBtn.Visibility = Visibility.Visible;
                    AdminBtn.Visibility = Visibility.Hidden;
                    UserBtn.Visibility = Visibility.Visible;
                    WindowState = WindowState.Maximized;
                }
                else
                    MessageBox.Show("Неверный пароль");
            }
        }
        private void ReportsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Reports());
        }
        private void OwnersBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Owners());
        }
        private void KeyBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Keys());
        }
        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Settings());
        }
        private void UserBtn_Click(object sender, RoutedEventArgs e)
        {
            KeyBtn.Visibility = Visibility.Hidden;
            OwnersBtn.Visibility = Visibility.Hidden;
            SettingsBtn.Visibility = Visibility.Hidden;
            AdminBtn.Visibility = Visibility.Visible;
            UserBtn.Visibility = Visibility.Hidden;
            WindowState = WindowState.Maximized;
            MainFrame.Navigate(new Monitoring());
        }
    }
}
