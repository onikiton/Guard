using System.Windows;

namespace Guard
{
    public partial class Auntefication : Window
    {
        public Auntefication()
        {
            InitializeComponent();
        }
        private void BtnAuntOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        public string Password
        {
            get { return Pass.Password; }
        }
    }
}
