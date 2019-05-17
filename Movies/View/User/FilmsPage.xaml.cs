using Movies.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Movies.View.User
{
    /// <summary>
    /// Логика взаимодействия для FilmsPage.xaml
    /// </summary>
    public partial class FilmsPage : Page
    {
        public FilmsPage()
        {
            InitializeComponent();

            //load();
        }

        // Метод для загрузки данных с БД
        void load()
        {
            // Загружаем фильмы из БД
            list.ItemsSource = new MyDB().Films.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //await Task.Run(() =>
            //{
            //    for (int i = 0; i <= 100; i++)
            //    {
            //        list.Items.Add("kek");
            //        list.Items.Refresh();
            //        //Thread.Sleep(500);
            //    }
            //});


            //for (int i = 0; i <= 100; i++)
            //{
            //    list.Items.Add("kek");
            //    list.Items.Refresh();
                
            //    Thread.Sleep(50);
            //}


            // load();

            //var a = (Films)list.SelectedItem;

            //MessageBox.Show(a.IdFilm.ToString());
        }
    }
}
