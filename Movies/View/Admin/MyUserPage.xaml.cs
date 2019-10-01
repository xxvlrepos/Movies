using Movies.DataModel;
using Movies.LogicApp;
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
    /// Логика взаимодействия для MyUserPage.xaml
    /// </summary>
    public partial class MyUserPage : Page
    {

        public static string login { get; set; }
        public static string passw { get; set; }

        AdminLogic logic = new AdminLogic();

        public MyUserPage()
        {
            InitializeComponent();
            LoadDB();

            int a = 5;
        }

        // Метод загрузки данных пользователей из БД
        public async void LoadDB()
        {
            UsersGrid.ItemsSource = await logic.GetUsersAsync();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранного пользователя с SelectedValue
            Users user = (Users)UsersGrid.SelectedValue;

            // Если выбрали пользователя, то удали его
            if (user != null)
            {
                try
                {
                    // Создаем подключение к БД
                    using (MyDB db = new MyDB())
                    {
                        db.Users.Remove(db.Users.FirstOrDefault(i => i.IdUser == user.IdUser)); // Удаляем БД
                        db.SaveChanges(); // Сохраняем БД

                        UsersGrid.ItemsSource = db.Users.ToList(); // Прогружаем список пользователей
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        // Кнопка редактирования
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //db.
            try
            {
                var i = (Users)(UsersGrid.SelectedValue);
                if (i != null)
                {
                    using (MyDB db = new MyDB())
                    {
                        db.Entry(i).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        LoadDB();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Кнопка добавления 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window wind = new AddUsersWindow();
            login = log.Text;
            passw = pass.Password;
            wind.Show();
        }
    }
}
