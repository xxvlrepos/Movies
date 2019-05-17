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
using System.Windows.Shapes;

namespace Movies.View.Main
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }



        private async Task<Users> GetUserAsync(string login, string password)
        {
            try
            {
                using (MyDB db = new MyDB())
                {
                    return await Task.Run(() =>
                    {
                        return db.Users.FirstOrDefault(i => i.Login == login && i.Pass == password);
                    });
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //Событие на клик кнопки авторизация
        private async void Autorization_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text != string.Empty && Pass.Password != string.Empty)
            {
                // Получаем пользователя
                var user = await new CommonLogic().AuthorizationAsync(Login.Text, Pass.Password);

                // Если пользователь найден, то открой окно в зависимости от его статуса
                if (user != null)
                {
                    switch (user.IdStatus)
                    {
                        case (1):
                            Window window = new User.UserWindow();
                            window.Show();
                            this.Close();

                            break;
                        case (2):
                            Window window1 = new Admin.AdminWindow();
                            window1.Show();
                            this.Close();
                            break;
                        default:
                            break;
                    }
                }
                else
                    MessageBox.Show("Пользователь не найден");
            }
            else
                MessageBox.Show("Введите логин и пароль");            
            
        }

        //Событие на клик кнопки регистрация 
        private void Registrarion_Click(object sender, RoutedEventArgs e)
        {
            Window window = new RegistrationWindow();

            window.Show();
            this.Close();

        }
    }
}
