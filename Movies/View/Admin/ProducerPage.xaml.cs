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
    /// Логика взаимодействия для ProducerPage.xaml
    /// </summary>
    public partial class ProducerPage : Page
    {

        #region Свойства

        LogicApp.AdminLogic logic; // Админская логика
        Producers producer; // Продюссер

        #endregion

        public ProducerPage()
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
            ActorsGrid.ItemsSource = await logic.GetProducersAsync();
        }


        #region WPF События

        // Метод инициализирует данные актера из DataGrid в Edit-компоненты
        private void InitializationActorFromDG(Producers myproducer)
        {
            if (myproducer != null)
            {
                NameEditBox.Text = myproducer.Name;
                FamilyEditBox.Text = myproducer.Family;
                SurnameEditBox.Text = myproducer.Surname;
                GenderCB_Edit.SelectedItem = myproducer.Gender;

                producer = myproducer;
            }
            else
            {
                NameEditBox.Text = string.Empty;
                FamilyEditBox.Text = string.Empty;
                SurnameEditBox.Text = string.Empty;
                GenderCB_Edit.SelectedItem = null;

                producer = null; // сбрасываем актера
            }
        }

        // Редактирование актера
        private async void EditClick(object sender, RoutedEventArgs e)
        {
            // Если выбрали актера
            if (producer != null)
            {
                producer.Name = NameEditBox.Text;
                producer.Family = FamilyEditBox.Text;
                producer.Surname = SurnameEditBox.Text;
                producer.Gender = (string)GenderCB_Edit.SelectedItem;


                // Редактируем актера
                bool edit = await logic.EditProducerAsync(producer);

                // Если актер редактирован успешно, то загрузи по новой базу данных актеров
                if (edit == true)
                    Load();

                InitializationActorFromDG(null); // Сбрасываем Edit-компоненты                
            }
            else
                MessageBox.Show("Сперва выберите актера");
        }

        // Событие на выбор из дата грида актера
        private void ActorsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InitializationActorFromDG((Producers)ActorsGrid.SelectedItem);
        }

        // Кнопка добавления актера
        private async void AddClick(object sender, RoutedEventArgs e)
        {
            // Проверяем на заполненные строки
            if (!string.IsNullOrWhiteSpace(name.Text) && !string.IsNullOrWhiteSpace(family.Text) && !string.IsNullOrWhiteSpace(surname.Text) && !string.IsNullOrWhiteSpace((string)GenderCB_Added.SelectedItem))
            {
                // Добавляем актера
                bool ActorAdded = await logic.AddProducerAsync(new Producers() { Name = name.Text, Family = family.Text, Surname = surname.Text, Gender = (string)GenderCB_Added.SelectedItem });

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

        private void ActorsGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ActorsGrid.MaxHeight = this.WindowHeight - 80;
        }
    }
}
