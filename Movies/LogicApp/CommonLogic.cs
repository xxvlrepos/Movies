using Movies.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Movies.LogicApp
{

    /// <summary>
    /// Класс содержит общую логику работы пользователей
    /// </summary>

    class CommonLogic
    {

        public RatingsLogic LogicRatings { get; private set; } // Логика рейтингов

        public CommonLogic()
        {
            LogicRatings = new RatingsLogic();
        }


        #region Секция методов для работы с БД
        public async Task<List<Films>> GetFilmsAsync(int skip = 0, int take = 0, string filmname = null, string country = null, int FilmYear = 0, int IdGenre = 0, bool loadposters = true)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (UserDB db = new UserDB())
                    {
                        bool CheckName = !(string.IsNullOrEmpty(filmname));
                        bool CheckCountry = !(string.IsNullOrEmpty(country));
                        bool CheckYear = FilmYear > 0;
                        bool CheckGenre = IdGenre > 0;

                        List<Films> films = (from vm in
                                                 (from item in db.Films
                                                  where
                                                      (CheckName ? item.Name.Contains(filmname) : !string.IsNullOrEmpty(item.Name)) &&
                                                      (CheckCountry ? item.Country.Contains(country) : !string.IsNullOrEmpty(item.Country)) &&
                                                      (CheckGenre ? item.IdGenre == IdGenre : item.IdGenre > 0) &&
                                                      (CheckYear ? item.Year.Value.Year == FilmYear : item.Year.Value.Year > 0)
                                                  select new
                                                  {
                                                      Poster = (loadposters == true) ? item.Poster : null,
                                                      IdFilm = item.IdFilm,
                                                      Name = item.Name,
                                                      IdProducer = item.IdProducer,
                                                      Year = item.Year,
                                                      IdGenre = item.IdGenre,
                                                      Country = item.Country,
                                                      Genres = item.Genres,
                                                      Producers = item.Producers

                                                  }).ToList().Skip(skip).Take(take)
                                             select new Films
                                             {
                                                 Poster = vm.Poster,
                                                 IdFilm = vm.IdFilm,
                                                 Name = vm.Name,
                                                 IdProducer = vm.IdProducer,
                                                 Year = vm.Year,
                                                 IdGenre = vm.IdGenre,
                                                 Country = vm.Country,
                                                 Genres = vm.Genres,
                                                 Producers = vm.Producers
                                             }).ToList();

                        return films;                     
                    }
                });
            }
            catch (Exception)
            {
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return null; // Возвращаем null, в случае, если пользователи не найдены или ошибка
        }

        // Метод, который получает список годов фильмов, которые были добавлены в бд (для вывода)
        public async Task<List<int>> GetYears()
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (UserDB db = new UserDB())
                    {
                        var dates = from p in db.Films
                                    group p by p.Year.Value.Year into g
                                    orderby g.Key descending
                                    select new { Date = g.Key };

                        List<int> list = new List<int>();

                        foreach (var item in dates)
                        {
                            list.Add(item.Date);
                        }
                        //return dates.GroupBy(s => dates).ToList();
                        return list;
                    }
                });
            }
            catch (Exception)
            {
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return null; // Возвращаем null, в случае, если пользователи не найдены или ошибка
        }

        // Метод, который получает список жанров.
        public async Task<List<Genres>> GetGenresAsync()
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (UserDB db = new UserDB())
                        return db.Genres.ToList();
                });
            }
            catch (Exception)
            {
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return null; // Возвращаем null, в случае, если пользователи не найдены или ошибка
        }

        /// <summary>
        /// Метод, который получает количество фильмов
        /// </summary>
        /// <param name="IdGenre">Айди жанра</param>
        /// <returns>Возвращает количество фильмов</returns>
        public int GetCountFilm(string filmname = null, string country = null, int FilmYear = 0, int IdGenre = 0, bool maxcount = false)
        {
            try
            {
                using (UserDB db = new UserDB())
                {
                    // Если выбрали максимальное количество фильмов
                    if (maxcount == true)
                        return db.Films.Count();

                    // Иначе выведи максимальное количество фильмов, которые можно вывести по выборке
                    bool CheckName = !(string.IsNullOrEmpty(filmname));
                    bool CheckCountry = !(string.IsNullOrEmpty(country));
                    bool CheckYear = FilmYear > 0;
                    bool CheckGenre = IdGenre > 0;

                    var count = from item in db.Films
                                where
                                    (CheckName ? item.Name.Contains(filmname) : !string.IsNullOrEmpty(item.Name)) &&
                                    (CheckCountry ? item.Country.Contains(country) : !string.IsNullOrEmpty(item.Country)) &&
                                    (CheckGenre ? item.IdGenre == IdGenre : item.IdGenre > 0) &&
                                    (CheckYear ? item.Year.Value.Year == FilmYear : item.Year.Value.Year > 0)
                                select new
                                {
                                    Year = item.Year,
                                };

                    return count.Count();
                }
            }
            catch (Exception)
            {

            }

            return 0; // в случае, если неудачно
        }

        /// <summary>
        /// Метод который загружает фильмы без выборки по жанрам
        /// </summary>
        /// <param name="skip">Пропускает количество фильмов в выборке</param>
        /// <param name="take">Берет количество фильмов в выборке</param>
        /// <param name="LoadAllData">Загрузка всей инфы (Постеры, комменты, инфа о фильме)</param>
        /// <returns>Коллекцию фильмов</returns>
        //public async Task<List<Films>> GetFilmsAsync(double test, int skip = 0, int take = 0, bool LoadAllData = false)
        //{
        //    try
        //    {
        //        return await Task.Run(() =>
        //        {
        //            using (UserDB db = new UserDB())
        //            {
        //                // Если не выбрали загрузку всей инфы, то загрузить определенные поля
        //                if (LoadAllData == false)
        //                {
        //                    if (skip != 0 || take != 0)
        //                    {
        //                        // !!! Эти преобразования и выгрузка делаются для того, чтобы не загружать много информации с изображениями !!!
        //                        var lists = db.Films.OrderBy(i => i.IdFilm).Skip(skip).Take(take).Select(i => new { i.IdFilm, i.Name, i.Producers, i.Genres, i.Year, i.Country }).ToList();

        //                        List<Films> mylists = new List<Films>();

        //                        foreach (var item in lists)
        //                        {
        //                            mylists.Add(new Films() { IdFilm = item.IdFilm, Producers = item.Producers, Country = item.Country, Name = item.Name, Year = item.Year, Genres = item.Genres });
        //                        }

        //                        return mylists;
        //                    }


        //                    // !!! Эти преобразования и выгрузка делаются для того, чтобы не загружать много информации с изображениями !!!
        //                    var list = db.Films.Select(i => new { i.IdFilm, i.Name, i.Producers, i.Genres, i.Year, i.Country }).ToList();

        //                    List<Films> mylist = new List<Films>();

        //                    foreach (var item in list)
        //                    {
        //                        mylist.Add(new Films() { IdFilm = item.IdFilm, Producers = item.Producers, Country = item.Country, Name = item.Name, Year = item.Year, Genres = item.Genres });
        //                    }

        //                    return mylist;
        //                }

        //                // Если выбрали загрузку всей инфы

        //                // Проверяет сколько пропустить в выборке, сколько взять
        //                if (skip != 0 || take != 0)
        //                {
        //                    var films = db.Films.Include(i => i.Genres).Include(i => i.Producers).OrderBy(i => i.IdFilm).Skip(skip).Take(take).ToList();

        //                    return films;
        //                }
                            

        //                // Если выбрали загрузку всей инфы, то вернуть всю инфу
        //                return db.Films.Include(i => i.Genres).Include(i => i.Producers).ToList();

        //            }
        //        });
        //    }
        //    catch (Exception)
        //    {
        //        // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
        //    }

        //    return null; // Возвращаем null, в случае, если пользователи не найдены или ошибка
        //}

        ///// <summary>
        ///// Метод который загружает весь список фильмов по жанрам с пропусками
        ///// </summary>
        ///// <param name="idStatusGenre">Айди жанра фильма</param>
        ///// <param name="skip">Пропускает количество фильмов в выборке</param>
        ///// <param name="take">Берет количество фильмов в выборке</param>
        ///// <param name="LoadAllData">Загрузка всей инфы (Постеры, комменты, инфа о фильме)</param>
        ///// <returns>Коллекцию фильмов</returns>
        //public async Task<List<Films>> GetFilmsAsync(int idStatusGenre, int skip = 0, int take = 0, bool LoadAllData = false)
        //{
        //    try
        //    {
        //        return await Task.Run(() =>
        //        {
        //            using (UserDB db = new UserDB())
        //            {
        //                // Если не выбрали загрузку всей инфы, то загрузить определенные поля
        //                if (LoadAllData == false)
        //                {
        //                    if (skip != 0 || take != 0)
        //                    {
        //                        // !!! Эти преобразования и выгрузка делаются для того, чтобы не загружать много информации с изображениями !!!
        //                        var lists = db.Films.Where(i => i.IdGenre == idStatusGenre).OrderBy(i => i.IdFilm).Skip(skip).Take(take).Select(i => new { i.IdFilm, i.Name, i.Producers, i.Genres, i.Year, i.Country }).ToList();

        //                        List<Films> mylists = new List<Films>();

        //                        foreach (var item in lists)
        //                        {
        //                            mylists.Add(new Films() { IdFilm = item.IdFilm, Producers = item.Producers, Country = item.Country, Name = item.Name, Year = item.Year, Genres = item.Genres });
        //                        }

        //                        return mylists;
        //                    }


        //                    // !!! Эти преобразования и выгрузка делаются для того, чтобы не загружать много информации с изображениями !!!
        //                    var list = db.Films.Where(i => i.IdGenre == idStatusGenre).Select(i => new { i.IdFilm, i.Name, i.Producers, i.Genres, i.Year, i.Country }).ToList();

        //                    List<Films> mylist = new List<Films>();

        //                    foreach (var item in list)
        //                    {
        //                        mylist.Add(new Films() { IdFilm = item.IdFilm, Producers = item.Producers, Country = item.Country, Name = item.Name, Year = item.Year, Genres = item.Genres });
        //                    }

        //                    return mylist;
        //                }

        //                // Если выбрали загрузку всей инфы

        //                // Проверяет сколько пропустить в выборке, сколько взять
        //                if (skip != 0 || take != 0)
        //                {
        //                    var films = db.Films.Where(i => i.IdGenre == idStatusGenre).Include(i => i.Genres).Include(i => i.Producers).OrderBy(i => i.IdFilm).Skip(skip).Take(take).ToList();

        //                    return films;
        //                }


        //                // Если выбрали загрузку всей инфы, то вернуть всю инфу
        //                return db.Films.Where(i => i.IdGenre == idStatusGenre).Include(i => i.Genres).Include(i => i.Producers).ToList();

        //            }
        //        });
        //    }
        //    catch (Exception)
        //    {
        //        // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
        //    }

        //    return null; // Возвращаем null, в случае, если пользователи не найдены или ошибка
        //}

        //public async Task<List<Films>> GetFilmsAsync(string name)
        //{
        //    try
        //    {
        //        return await Task.Run(() =>
        //        {
        //            using (UserDB db = new UserDB())
        //            {
        //                //var films = from f in db.Films
        //                //            where f.Name.Contains(name);

        //                return db.Films.Where(p => p.Name.Contains(name)).Include(i => i.Genres).Include(i => i.Producers).ToList();

        //            }
        //        });
        //    }
        //    catch (Exception)
        //    {
        //        // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
        //    }

        //    return null; // Возвращаем null, в случае, если пользователи не найдены или ошибка
        //}

        /// <summary>
        /// Метод, который загружает один фильм
        /// </summary>
        /// <param name="IdFilm">Айди фильма</param>
        /// <returns>Фильм</returns>
        public async Task<Films> GetOneFilmAsync(int IdFilm)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (UserDB db = new UserDB())
                    {
                        return db.Films.Include(i => i.Genres).Include(i => i.Producers).Include(i => i.ActorsFilm).Include("ActorsFilm.Actors").Include(i => i.Ratings).FirstOrDefault(i => i.IdFilm == IdFilm);
                    }
                });
            }
            catch (Exception)
            {
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return null; // Возвращаем null, в случае, если не найдено ничего
        }

        // Метод авторизации в асинхронном режиме
        public async Task<Users> AuthorizationAsync(string login, string password)
        {
            try
            {
                if (login != string.Empty && password != string.Empty)
                {
                    // Возвращаем объект пользователя по логину и паролю
                    return await Task.Run(() =>
                    {
                        using (UserDB db = new UserDB())
                            return db.Users.Include(i => i.Ratings).FirstOrDefault(i => i.Login == login && i.Pass == password);
                    });
                }
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Сервер не отвечает");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return null;
        }

        #endregion

        #region Методы работы с окнами

        public virtual void ShowWindow(Users user)
        {
            Window window = new Movies.View.User.UserWindow(user);
            window.Show();
        }

        #endregion

    }
}
