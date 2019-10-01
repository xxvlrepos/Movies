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

        List<Films> films; // Фильмы
        int filmcount = 0; // Количество фильмов
        int loadedfilm = 0; // Количество загруженных фильмов
        bool filmsloading = false; // Загружаются ли фильмы
        CommonLogic logic;
        Users user;

        public FilmsPage(Users user)
        {
                       
            InitializeComponent();

            logic = new CommonLogic();
            filmcount = logic.GetCountFilm();
            this.user = user;
            films = new List<Films>();

            load();
        }



        // Метод для загрузки данных с БД
        async void load()
        {
            // Загружаем фильмы из БД, если загружено меньше, чем максимальное количество
            if (loadedfilm <= filmcount && filmsloading == false)
            {
                filmsloading = true;

                var loadingfilms = await logic.GetFilmsAsync(1, loadedfilm, 5, true);

                foreach (var film in loadingfilms)
                    films.Add(film);

                list.ItemsSource = films;              
                list.Items.Refresh();

                filmsloading = false;
                loadedfilm += 5;
            }
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {

            var film = (Films)list.SelectedItem;
            NavigationService.Navigate(new AboutFilmPage(film.IdFilm, user));            
        }

        private void List_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            switch (GetScrollThumbPosition(e))
            {                
                case ScrollThumbPosition.ReachedTheTop: //Что-то делаем
                    break;
                case ScrollThumbPosition.UntilTheMid: //Что-то делаем                    
                    break;
                case ScrollThumbPosition.AfterTheMid: //Что-то делаем                    
                    // Логика прогрузки дополнительно
                    load();
                    break;
                case ScrollThumbPosition.ReachedTheBottom: //Что-то делаем
                    break;
                default:
                    break;
            }
        }

        #region Вспомогательные методы для скролла

        private ScrollThumbPosition GetScrollThumbPosition(ScrollChangedEventArgs e)
        {
            if (e.VerticalOffset == 0)
                return ScrollThumbPosition.ReachedTheTop;
            else if (e.ExtentHeight - e.ViewportHeight - e.VerticalOffset == 0)
                return ScrollThumbPosition.ReachedTheBottom;
            else if (e.VerticalOffset + e.ViewportHeight >= e.ExtentHeight / 2 + (e.ExtentHeight - ((ScrollViewer)e.OriginalSource).ScrollableHeight) / 2)
                return ScrollThumbPosition.AfterTheMid;
            else if (e.VerticalOffset + e.ViewportHeight <= e.ExtentHeight / 2 + (e.ExtentHeight - ((ScrollViewer)e.OriginalSource).ScrollableHeight) / 2)
                return ScrollThumbPosition.UntilTheMid;

            throw new Exception("Ошибка расчёта положения ползунка скрола");
        }

        private enum ScrollThumbPosition : byte
        {
            ReachedTheTop, UntilTheMid, AfterTheMid, ReachedTheBottom
        }

        #endregion

        private void List_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            MessageBox.Show("loaded");
        }
    }
}
