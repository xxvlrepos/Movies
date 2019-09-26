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

        LogicApp.AdminLogic logic;

        public ActorsPage()
        {
            InitializeComponent();

            logic = new LogicApp.AdminLogic();

            Load();
        }

        public async void Load()
        {
            ActorsGrid.ItemsSource = await logic.GetActorsAsync();
        }


    }
}
