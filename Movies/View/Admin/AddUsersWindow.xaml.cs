using Movies.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Movies.View.Admin
{
    /// <summary>
    /// Логика взаимодействия для AddUsersWindow.xaml
    /// </summary>
    public partial class AddUsersWindow : Window
    {
        public AddUsersWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool added = false;
            int i = 1;
            if (adm.IsChecked == true) { i = 2; }

            using (MyDB db = new MyDB())
            {
                Users user = new Users();
                user.Login = log.Text;
                user.Pass = pass.Text;
                user.IdStatus = i;

                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    
                    added = true;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (added == true)
                    MessageBox.Show($"Пользователь {log.Text} добавлен в БД!");
                else
                    MessageBox.Show($"Ошибка добавления!");
            }
            
            this.Close();
        }
    }
}
