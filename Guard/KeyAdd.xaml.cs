using System.Windows;
using System.Windows.Controls;

namespace Guard
{
    public partial class KeyAdd : Window
    {
        public KeyAdd()
        {
            InitializeComponent();
            using SecurityDbContext db = new();
            typeKey.ItemsSource = db.KeyTypes.ToList();
        }
        private void BtnSaveId_Click(object sender, RoutedEventArgs e)
        {
            using SecurityDbContext db = new();
            try
            {
                Key addKey = new()
                {
                    Identifier =  int.Parse(ID.Text),
                    DateTime = DateTime.Now.ToString("G"),
                    KeyTypeId = typeId
                };
                db.Keys.Add(addKey);
                db.SaveChanges();
                Reader.sp.Write("newKey" + ID.Text);
                Manager.Frame.Navigate(new Keys());
                Close();
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения");
                Close();
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Keys.isNewIdRead = false;
        }
        private void typeKey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            typeId = ((sender as ComboBox).SelectedItem as KeyType).Id;
        }
        public int? typeId { get; set; }
    }
}
