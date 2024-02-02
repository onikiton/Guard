using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace Guard
{
    public partial class Keys : Page
    {
        public Keys()
        {
            InitializeComponent();
            SecurityDbContext db = new SecurityDbContext();
            ownersgrid.ItemsSource = db.Keys.Include(k => k.KeyType).ToList();
        }
        private void KeyDelBtn_Click(object sender, RoutedEventArgs e)
        {
            using SecurityDbContext db = new();
            Key keydel = (Key)ownersgrid.SelectedItem;
            if (keydel == null)
            {
                MessageBox.Show("Для удаления выдилите идентификатор", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (MessageBox.Show("Удалить идентификатор?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.Keys.Remove(keydel);
                        db.SaveChanges();
                        Reader.sp.Write("delKey" + keydel.Identifier);
                        ownersgrid.ItemsSource = db.Keys.ToList();
                    }

                    catch
                    {
                        MessageBox.Show("Ошибка удаления", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void KeyAddBtn_Click(object sender, RoutedEventArgs e)
        {
            isNewIdRead = true;
            Manager.KeyAdd = new();
            Manager.KeyAdd.ShowDialog();
        }
        public static bool isNewIdRead { get; set; } = false;
    }
}
