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
    /// Логика взаимодействия для FilmsPage.xaml
    /// </summary>
    public partial class FilmsPage : Page
    {
        public FilmsPage()
        {
            InitializeComponent();
            LoadDB();
        }

        // Метод для загрузки данных из БД
        private void LoadDB()
        {
            // Создаем подключение к бд
            using (MyDB db = new MyDB())
                FilmsGrid.ItemsSource = new MyDB().Films.ToList(); // Загружаем в grid.itemssource названия фильмов
        }
        
    }
}
