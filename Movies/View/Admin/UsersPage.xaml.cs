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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Movies.View.Admin
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
            LoadDB();
        }

        private void LoadDB()
        {
            using (MyDB db = new MyDB())
            {
                List<Users> users = new MyDB().Users.ToList();
                UsersGrid.ItemsSource = users;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window wind = new  AddUsersWindow();
            wind.Show();
            
        }

        private void Del<T>(ref T user)
            where T : class

        {
            using (MyDB db = new MyDB())
            {
                {
                    db.Entry(user).State = System.Data.Entity.EntityState.Deleted; // То удали из бд
                    db.SaveChanges();

                    UsersGrid.ItemsSource = db.Users.ToList();
                }
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {

       
            Users user = (Users)UsersGrid.SelectedValue;
            Del<Users>(ref user);


            //MyDB db = new MyDB();
            //Users user = (Users)UsersGrid.SelectedValue;
            //db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
            //db.SaveChanges();
            //UsersGrid.ItemsSource = db.Users.ToList();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //db.
        }
    }
}
