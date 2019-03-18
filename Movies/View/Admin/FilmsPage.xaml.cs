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

        private void LoadDB()
        {

            using (MyDB db = new MyDB())
            {
                List<Films> films = new MyDB().Films.ToList();

                foreach (var item in films)
                {
                    item.Genre = db.Genres.FirstOrDefault(i => i.IdGenre == item.IdGenre).GenreName;

                    var producer = db.Producers.FirstOrDefault(i => i.idProducer == item.IdProducer);
                    item.producer_fio = $"{producer.Family} {producer.Name} {producer.Surname}";
                }

                FilmsGrid.ItemsSource = films;
            }
                

            
        }
        
    }
}
