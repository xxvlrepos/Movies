using Movies.DataModel;
using Movies.LogicApp;
using Movies.View.User.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class MainFilmsPage : Page
    {
        Users user;
        readonly CommonLogic logic;
        MainFilmsPageModel model;
        

        public MainFilmsPage(Users user)
        {
            InitializeComponent();
            this.user = user;
            logic = new CommonLogic();
            model = new MainFilmsPageModel();

            LoadStartData(); // Загружаем первоначальные данные
        }

        #region Вспомогательные методы

        // Метод для загрузки и инициализации первоначальных данных
        private async void LoadStartData()
        {
            genrescb.ItemsSource = await logic.GetGenresAsync();
            datescb.ItemsSource = await logic.GetYears();

            countycb.ItemsSource = new List<string>() // Задаем страны
                {
                    "Россия",
                    "Украина",
                    "США",
                    "Канада",
                    "Англия",
                    "Германия"
                };
        }

        #endregion

        #region События WPF

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Если нажали энтер, то обработай входные данные и передай
            if (e.Key == Key.Enter)
            {
                NavigationService.Navigate(new FilmsPage(user, model));
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            model.FilmName = tbsearch.Text;
        }

        private void Genrescb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = (Genres)genrescb.SelectedItem;
                model.IdGenre = item.IdGenre;
            }
            catch (Exception)
            {
                model.IdGenre = 0;
            }
        }

        private void Datescb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = (int)datescb.SelectedItem;
                model.FilmYear = item;
            }
            catch (Exception)
            {
                model.FilmYear = 0;
            }
        }

        private void Countycb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.FilmCounty = (string)countycb.SelectedItem;
        }

        #endregion


        // Событие пкм на ComboBoxes для сброса значения
        private void Datescb_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            datescb.SelectedItem = null;
        }

        // Событие пкм на ComboBoxes для сброса значения
        private void Genrescb_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            genrescb.SelectedItem = null;
        }

        // Событие пкм на ComboBoxes для сброса значения
        private void Countycb_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            countycb.SelectedItem = null;
        }
    }
}
