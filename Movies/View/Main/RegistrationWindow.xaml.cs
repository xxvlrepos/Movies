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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }


        private void GoBack()
        {
            Window window = new LoginWindow();

            window.Show();
            this.Close();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void Registrarion_click(object sender, RoutedEventArgs e)
        {
            // Проверяем входные данные 
            if (Login.Text != string.Empty && Pass.Password != string.Empty)
            {
                using (MyDB db = new MyDB())
                {

                    try
                    {
                        db.Users.Add(new Users(Login.Text, Pass.Password));
                        db.SaveChanges();

                        MessageBox.Show($"Пользователь успешно зарегистрирован");
                        GoBack();

                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show($"Аккаунт {Login.Text} уже существует");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
            else
                MessageBox.Show("Данные не заполнены!!!");
        }
    }
}
