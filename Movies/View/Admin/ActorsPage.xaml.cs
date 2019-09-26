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
    /// Логика взаимодействия для ActorsPage.xaml
    /// </summary>
    public partial class ActorsPage : Page
    {

        #region Свойства

        LogicApp.AdminLogic logic; // Админская логика
        Actors actor; // Актер

        #endregion

        public ActorsPage()
        {
            InitializeComponent();

            logic = new LogicApp.AdminLogic();

            // Инициализируем итемссурс для гендеров актера
            GenderCB_Added.ItemsSource = new List<string>()
            {
                "М", "Ж"
            };

            GenderCB_Edit.ItemsSource = GenderCB_Added.ItemsSource;

            Load();
        }

        public async void Load()
        {
            ActorsGrid.ItemsSource = await logic.GetActorsAsync();
        }


        #region WPF События

        // Метод инициализирует данные актера из DataGrid в Edit-компоненты
        private void InitializationActorFromDG(Actors myactor)
        {
            if (myactor != null)
            {
                NameEditBox.Text = myactor.Name;
                FamilyEditBox.Text = myactor.Family;
                SurnameEditBox.Text = myactor.Surname;
                GenderCB_Edit.SelectedItem = myactor.Gender;

                actor = myactor;
            }
            else
            {
                NameEditBox.Text = string.Empty;
                FamilyEditBox.Text = string.Empty;
                SurnameEditBox.Text = string.Empty;
                GenderCB_Edit.SelectedItem = null;

                actor = null; // сбрасываем актера
            }
        }

        // Редактирование актера
        private async void EditClick(object sender, RoutedEventArgs e)
        {
            // Если выбрали актера
            if (actor != null)
            {
                actor.Name = NameEditBox.Text;
                actor.Family = FamilyEditBox.Text;
                actor.Surname = SurnameEditBox.Text;
                actor.Gender = (string)GenderCB_Edit.SelectedItem;


                // Редактируем актера
                bool edit = await logic.EditActor(actor);

                // Если актер редактирован успешно, то загрузи по новой базу данных актеров
                if (edit == true)                    
                    ActorsGrid.ItemsSource = await logic.GetActorsAsync();

                InitializationActorFromDG(null); // Сбрасываем Edit-компоненты                
            }
            else
                MessageBox.Show("Сперва выберите актера");
        }

        // Событие на выбор из дата грида актера
        private void ActorsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InitializationActorFromDG((Actors)ActorsGrid.SelectedItem);
        }

        // Кнопка добавления актера
        private async void AddClick(object sender, RoutedEventArgs e)
        {
            // Проверяем на заполненные строки
            if (!string.IsNullOrWhiteSpace(name.Text) && !string.IsNullOrWhiteSpace(family.Text) && !string.IsNullOrWhiteSpace(surname.Text) && !string.IsNullOrWhiteSpace((string)GenderCB_Added.SelectedItem))
            {
                // Добавляем актера
                bool ActorAdded = await logic.AddActor(new Actors() { Name = name.Text, Family = family.Text, Surname = surname.Text, Gender = (string)GenderCB_Added.SelectedItem });

                // Если актер добавлен то загрузи в DataGrid актеров и обнули поля
                if (ActorAdded)
                {
                    // Загружаем
                    Load();

                    // Обнуляем поля
                    name.Text = string.Empty;
                    family.Text = string.Empty;
                    surname.Text = string.Empty;
                    GenderCB_Added.SelectedItem = null;
                }
            }
            else
                MessageBox.Show("Заполните строки");

            //bool ActorAdded = await logic.AddActor(new Actors());
        }

        #endregion
    }
}
