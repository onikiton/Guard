using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace Guard
{
    public partial class Owners : Page
    {
        public Owners()
        {
            InitializeComponent();
            SecurityDbContext db = new();
            ownersgrid.ItemsSource = db.Owners.Include(d => d.Department).Include(k => k.Key).ToList();
        }
        private void OwnerDelBtn_Click(object sender, RoutedEventArgs e)
        {
            using SecurityDbContext db = new();
            var ownerdel = ownersgrid.SelectedItem;
            if (ownerdel == null)
            {
                MessageBox.Show("Для удаления выдилите сотрудника", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (MessageBox.Show("Удалить сотрудника?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.Owners.Remove((Owner)ownerdel);
                        db.SaveChanges();
                        ownersgrid.ItemsSource = db.Owners.Include(d => d.Department).Include(k => k.Key).ToList();
                    }

                    catch
                    {
                        MessageBox.Show("Ошибка удаления", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
            private void OwnerEditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Owner)ownersgrid.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show("Для редактирования выдилите сотрудника", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                OwnersEdit OwnersEdit = new(selected);
                OwnersEdit.ShowDialog();
            }

        }
        private void OwnerAddBtn_Click(object sender, RoutedEventArgs e)
        {
            OwnersAdd OwnersAdd = new();
            OwnersAdd.ShowDialog();
        }

        private void DepartmentBtn_Click(object sender, RoutedEventArgs e)
        {
            Departments Departments = new();
            Departments.ShowDialog();
        }
    }
}
