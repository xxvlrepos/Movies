using Movies.DataModel;
using Movies.View.User;
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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        Users user;

        public AdminWindow(Users user)
        {
            InitializeComponent();

            this.user = user;
            frame.Content = new MainFilmsPage(user);
        }

        private void Films_click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new FilmsPage());
        }

        private void Users_click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new MyUserPage());
        }

        private void Actors_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new ActorsPage());
        }

        private void Producers_Click(object sender, RoutedEventArgs e)
        {            
            frame.Navigate(new ProducerPage());
        }

        private void MainPage_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new MainFilmsPage(user));
        }
    }
}
