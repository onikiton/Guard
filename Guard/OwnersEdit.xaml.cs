using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Guard
{
    public partial class OwnersEdit : Window
    {
        public OwnersEdit(Owner selected)
        {
            InitializeComponent();
            SecurityDbContext db = new();
            EditDepartment.ItemsSource = db.Departments.ToList();
            EditIdentifier.ItemsSource = db.Keys.ToList();
            SelectedOwner = selected;
            if (SelectedOwner != null)
            {
                using (var stream = new MemoryStream(SelectedOwner.Photo))
                {
                    Photo.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                }
                EditFirstName.Text = SelectedOwner.FirstName;
                EditLastName.Text = SelectedOwner.LastName;
                EditPatronymic.Text = SelectedOwner.Patronymic;
                EditDepartment.SelectedValue = SelectedOwner.DepartmentId;
                EditIdentifier.SelectedValue = SelectedOwner.KeyId;
            }
        }
        private void BtnEditImage_Click(object sender, RoutedEventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "D:\\Documents\\";
            fileDialog.ShowDialog();
            PhotoPath = fileDialog.FileName;
            Photo.Source = new BitmapImage(new Uri(PhotoPath));
        }
        private void BtnSaveOwner_Click(object sender, RoutedEventArgs e)
        {
            using (SecurityDbContext db = new())
            {
                try
                {
                    if (PhotoPath != null) SelectedOwner.Photo = File.ReadAllBytes(PhotoPath);
                    SelectedOwner.FirstName = EditFirstName.Text;
                    SelectedOwner.LastName = EditLastName.Text;
                    SelectedOwner.Patronymic = EditPatronymic.Text;
                    SelectedOwner.DepartmentId = (int?)EditDepartment.SelectedValue;
                    SelectedOwner.KeyId = (int?)EditIdentifier.SelectedValue;
                    db.Entry(SelectedOwner).State = EntityState.Modified;
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
        private void BtnRemKey_Click(object sender, RoutedEventArgs e)
        {
            using SecurityDbContext db = new();
            try
            {
                SelectedOwner.KeyId = null;
                db.Entry(SelectedOwner).State = EntityState.Modified;
                db.SaveChanges();
                EditIdentifier.ItemsSource = db.Keys.ToList();
                Close();    
                Manager.Frame.Navigate(new Owners());
            }
            catch
            {
                MessageBox.Show("Поля заполнены неверно");
            }

        }
        public string? PhotoPath;
        public Owner SelectedOwner;
    }
}

