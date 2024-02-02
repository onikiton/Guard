using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Guard
{
    public partial class OwnersAdd : Window
    {
        public OwnersAdd()
        {
            InitializeComponent();
            SecurityDbContext db = new();
            AddDepartment.ItemsSource = db.Departments.ToList();
            AddIdentifier.ItemsSource = db.Keys.ToList();
        }
        private void BtnSaveOwner_Click(object sender, RoutedEventArgs e)
        {
            using (SecurityDbContext db = new())
            {
                try
                {
                    Owner addOwner = new()
                    {
                        FirstName = AddFirstName.Text,
                        LastName = AddLastName.Text,
                        Patronymic = AddPatronymic.Text,
                        Photo = File.ReadAllBytes(PhotoPath),
                        KeyId = KeyID,
                        DepartmentId = DepartmentID
                    };
                    db.Owners.Add(addOwner);
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Поля заполнены неверно");
                }
            }
            Close();
            Manager.Frame.Navigate(new Owners());
        }
        private void BtnAddImage_Click(object sender, RoutedEventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            PhotoPath = fileDialog.FileName;
            Photo.Source = new BitmapImage(new Uri(PhotoPath));
        }
        private void AddDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DepartmentID = ((sender as ComboBox).SelectedItem as Department).Id;

        }

        private void AddIdentifier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KeyID = ((sender as ComboBox).SelectedItem as Key).Id;
        }
        public int KeyID { get; set; }
        public int DepartmentID { get; set; }
        public string PhotoPath { get; set; }
    }
}
