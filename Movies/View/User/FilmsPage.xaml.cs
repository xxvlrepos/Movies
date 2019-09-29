using Movies.DataModel;
using Movies.LogicApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        CommonLogic logic;

        public FilmsPage()
        {
            InitializeComponent();

            logic = new CommonLogic();
            load();
        }

        // Метод для загрузки данных с БД
        async void load()
        {
            // Загружаем фильмы из БД
            list.ItemsSource = await logic.GetFilmsAsync();
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {

            var film = (Films)list.SelectedItem;


            MessageBox.Show($"Айди фильма {film.Name}");
        }

       
    }
}
