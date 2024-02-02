using System.Windows;

namespace Guard
{
    public partial class Departments : Window
    {
        public Departments()
        {
            InitializeComponent();
            SecurityDbContext db = new SecurityDbContext();
            departmentlist.ItemsSource = db.Departments.ToList();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddBtn.Visibility = Visibility.Hidden;
            DelBtn.Visibility = Visibility.Hidden;
            newpanel.Visibility = Visibility.Visible;
        }
        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            using SecurityDbContext db = new();
            var departmentdel = departmentlist.SelectedItem;
            if (departmentdel == null)
            {
                MessageBox.Show("Для удаления выдилите отдел", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (MessageBox.Show("Удалить отдел?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.Departments.Remove((Department)departmentdel);
                        db.SaveChanges();
                        departmentlist.ItemsSource = db.Departments.ToList();
                    }

                    catch
                    {
                        MessageBox.Show("Ошибка удаления", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            departmentlist.ItemsSource = db.Departments.ToList();
        }
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            using (SecurityDbContext db = new())
            {
                try
                {
                    Department addDepartment = new Department
                    {
                        Name = newdepartment.Text
                    };
                    db.Departments.Add(addDepartment);
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Поля заполнены неверно");
                }
                departmentlist.ItemsSource = db.Departments.ToList();
            }
            AddBtn.Visibility = Visibility.Visible;
            DelBtn.Visibility = Visibility.Visible;
            newpanel.Visibility = Visibility.Hidden;
        }
        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            AddBtn.Visibility = Visibility.Visible;
            DelBtn.Visibility = Visibility.Visible;
            newpanel.Visibility = Visibility.Hidden;
        }
    }
}
