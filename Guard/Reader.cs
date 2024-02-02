using Microsoft.EntityFrameworkCore;
using System.IO.Ports;
using System.Windows;

namespace Guard
{
    public class Reader
    {
        public static SerialPort sp = new();
        public static void serialPort(string com)
        {
            try
            {
                if (sp.IsOpen) sp.Close();
                sp.PortName = com;
                sp.BaudRate = 9600;
                sp.Open();
                sp.DataReceived += OnDataReceived;
                sp.Write("time" + DateTime.Now.Subtract(DateTime.UnixEpoch).TotalSeconds.ToString());
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к контроллеру");
            }  
        }
        private static void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serialDevice = (SerialPort)sender;
            string[] msg = serialDevice.ReadExisting().Split(',');
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (Keys.isNewIdRead)
                {
                    Manager.KeyAdd.ID.Text = msg[2];
                }
                else
                {  
                    Events.AddEvent(msg);
                    using SecurityDbContext db = new();
                    Manager.Monitoring.eventlist.ItemsSource = db.Events.Include(o => o.Owner).
                        Include(e => e.EventType).OrderByDescending(t => t.DateTime).
                        Take(4).ToList();
                }
            }));
        }
    }
}