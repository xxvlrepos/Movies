using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.View.User.ViewModels
{

    /// <summary>
    /// Модель вьюхи, которая содержит в себе поля главной поисковой страницы
    /// </summary>
    public class MainFilmsPageModel
    {
        public string FilmName { get; set; } // Название фильма
        public int FilmYear { get; set; } // Год фильма
        public string FilmCounty { get; set; } // Страна фильма
        public int IdGenre { get; set; } // Айди жанра
    }
}
