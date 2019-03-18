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
            
        //Событие на клик кнопки авторизация
        private void Autorization_Click(object sender, RoutedEventArgs e)
        {

            //Создаем канал связи с бд
            using (MyDB db = new MyDB())
            {
                // Ищем пользователя по параметрам
                var user = db.Users.FirstOrDefault(i => i.Login == Login.Text && i.Pass == Pass.Password);

                // Если пользователь найден ( != null), то определи какого он статуса и откорй соответствующие окно
                if (user != null)
                {
                    MessageBox.Show($"Пользователь {user.Login} найден");

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
