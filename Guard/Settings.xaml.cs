using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;

namespace Guard
{
    public partial class Settings : Page
    {
        string port;
        public Settings()
        {
            InitializeComponent();
            COM.ItemsSource = SerialPort.GetPortNames();
        }
        private void SyncBtn_Click(object sender, RoutedEventArgs e)
        {
            Reader.sp.Write("sync");
        }
        private void spBtn_Click(object sender, RoutedEventArgs e)
        {           
            Reader.serialPort(port);
        }
        private void COM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            port = COM.SelectedItem as string;
        }
    }
}
