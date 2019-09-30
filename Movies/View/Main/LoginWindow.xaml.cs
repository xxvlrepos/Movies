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


        CommonLogic LogicApp; // Логика работы программы

        public LoginWindow()
        {
            InitializeComponent();

            LogicApp = new CommonLogic();
        }


        //Событие на клик кнопки авторизация
        private async void Autorization_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text != string.Empty && Pass.Password != string.Empty)
            {
                // Получаем пользователя
                var user = await LogicApp.AuthorizationAsync(Login.Text, Pass.Password);

                // Если пользователь найден, то открой окно в зависимости от его статуса
                if (user != null)
                {
                    if (user.IdStatus == 0) // Если админ, то реализация логики администратора
                        LogicApp = new AdminLogic();

                    LogicApp.ShowWindow(user);


                    this.Close(); // Закрываем текущее окно
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
