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
            FilmsGrid.ItemsSource = new MyDB().Films.ToList(); // Загружаем в grid.itemssource названия фильмов
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var film = (DataModel.Films)(FilmsGrid.SelectedValue);
            using (MyDB db = new MyDB())
            {
                db.Films.Remove(db.Films.FirstOrDefault(s => s.IdFilm == film.IdFilm));
                db.SaveChanges();
                LoadDB();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var i = FilmsGrid.SelectedValue;
                if (i != null)
                {
                    using (MyDB db = new MyDB())
                    {
                        db.Entry(i).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();


                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Window wind = new AddFilmWindow();
            wind.Show();
        }
    }
}
