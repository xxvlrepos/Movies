using Movies.DataModel;
using Movies.LogicApp;
using Movies.LogicApp.Helps;
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

namespace Movies.View.User
{
    /// <summary>
    /// Логика взаимодействия для AboutFilmPage.xaml
    /// </summary>
    public partial class AboutFilmPage : Page
    {
        #region Свойства

        Users user; // Юзер
        Films film; // Фильм
        CommonLogic logic; // логика
        Ratings myrating; // Мой рейтинг фильму
        List<Ratings> listrating; // Рейтинги фильма

        #endregion

        #region Вспомогательные методы

        private async void InitializationData(int idFilm)
        {
            logic = new CommonLogic();

            
            film = await logic.GetOneFilmAsync(idFilm);

            myrating = await logic.LogicRatings.GetMyRatingFilm(idFilm, user.IdUser); //  Мой рейтинг
            listrating = await logic.LogicRatings.GetRatingsOnFilm(idFilm); // Получаем списки рейтингов

            // Инициализация компонентов
            FilmName.Content = film.Name;
            producer.Content = film.Producers.GetProducerFIO;
            imgposter.Source = ImageLogic.ByteToImage(film.Poster); // Визуализация изображения
            aboutfilm.Text = film.AboutFilm;
            year.Content = film.DateFilm;
            country.Content = film.Country;
            genree.Content = film.Genres.GenreName;
            actorslist.ItemsSource = film.ActorsFilm;
            ratingslist.ItemsSource = listrating;
            rating.Content = listrating.Average(i => i.Rating);


        }
        
        #endregion

        public AboutFilmPage(int idFilm, Users user)
        {
            InitializeComponent();

            this.user = user;
            InitializationData(idFilm);
        }

        #region Свойства WPF

        // Событие на кнопку назад (Возвращает на главную страницу)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            base.NavigationService.GoBack();
        }

        // Событие на изменение слайдера
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                star.Content = e.NewValue;
            }
            catch (Exception)
            {

            }
        }

        // Отправить рейтинг
        private async void SendRating_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(CommentFilm.Text))
                {
                    // Если мой рейтинг пустой, то парси данные
                    if (myrating == null)
                    {
                        myrating = new Ratings()
                        {
                            Comment = CommentFilm.Text,
                            IdUser = user.IdUser,
                            IdFilm = film.IdFilm,
                            Rating = Convert.ToByte(star.Content)
                        };

                        // Добавляем рейтинг
                        bool added = await logic.LogicRatings.AddRatingsOnFilm(myrating);

                        // Если добавление успешно, то добавь в конец списка
                        if (added == true)
                        {
                            listrating.Add(myrating);
                            ratingslist.ItemsSource = listrating;
                            ratingslist.Items.Refresh();
                            rating.Content = listrating.Average(i => i.Rating);
                        }
                    }
                    // Иначе, если рейтинг есть, то обнови данные
                    else
                    {
                        myrating.Rating = Convert.ToByte(star.Content);
                        myrating.Comment = CommentFilm.Text;

                        bool updated = await logic.LogicRatings.UpdateRatingsOnFilm(myrating);

                        if (updated == true)
                        {
                            listrating.FirstOrDefault(i => i.IdRating == myrating.IdRating).Comment = myrating.Comment;
                            listrating.FirstOrDefault(i => i.IdRating == myrating.IdRating).Rating = myrating.Rating;

                            ratingslist.ItemsSource = listrating; // Обновляем список
                            ratingslist.Items.Refresh();
                            rating.Content = listrating.Average(i => i.Rating);
                        }

                    }
                }
                else
                    MessageBox.Show("Вы не написали комментарий!");
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Дождитесь загрузки данных!!!");
            }
        }

        #endregion

    }
}
