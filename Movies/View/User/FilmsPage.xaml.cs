using Movies.DataModel;
using Movies.LogicApp;
using Movies.View.User.ViewModels;
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
        MainFilmsPageModel model;
        CommonLogic logic;
        Users user;
        private delegate Task<List<Films>> method();
        method loading;

        public FilmsPage(Users user, MainFilmsPageModel model)
        {
                       
            InitializeComponent();

            InitializationData(user, model);
            load();
        }

        // Метод для инициализации данных
        private void InitializationData(Users user, MainFilmsPageModel model)
        {
            this.model = model;
            logic = new CommonLogic();
            filmcount = logic.GetCountFilm(model.IdGenre);
            this.user = user;
            films = new List<Films>();

            // Если ввели название фильма, то выведи в текст бокс
            if (!string.IsNullOrWhiteSpace(model.FilmName))
                resultbox.Text = model.FilmName;

            InitDelegat(); // Инициализируем ссылку делегата на метод
        }

        #region Методы для делегата

        private void InitDelegat()
        {
            if (!string.IsNullOrWhiteSpace(model.FilmName))
            {
                loading = OnlyFilmName;
                return;
            }

            if (model.IdGenre == 0)
            {
                loading = GetAllFilmsNotGenre;
            }
            else
                loading = GetAllFilmsGenre;
        }

        private Task<List<Films>> OnlyFilmName()
        {
            return logic.GetFilmsAsync(model.FilmName);
        }

        private Task<List<Films>> GetAllFilmsNotGenre()
        {
            return logic.GetFilmsAsync(loadedfilm, 5, true);
        }

        private Task<List<Films>> GetAllFilmsGenre()
        {
            return logic.GetFilmsAsync(model.IdGenre, loadedfilm, 5, true);
        }

        #endregion


        // Метод для загрузки данных с БД
        async void load()
        {
            // Загружаем фильмы из БД, если загружено меньше, чем максимальное количество
            if (loadedfilm <= filmcount && filmsloading == false)
            {
                filmsloading = true;

                List<Films> loadingfilms = await loading();

                foreach (var film in loadingfilms)
                    films.Add(film);

                list.ItemsSource = films;              
                list.Items.Refresh();

                filmsloading = false;
                loadedfilm += 5;
            }
        }

        #region WPF события

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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        #endregion

    }
}
