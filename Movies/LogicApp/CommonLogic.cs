using Movies.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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
        public int GetCountFilm(int IdGenre = 0)
        {
            try
            {
                using (UserDB db = new UserDB())
                {
                    // Если не выбрали жанр то верни общее количество
                    if (IdGenre == 0)
                        return db.Films.Count();

                    // Если выбрали жанр
                    return db.Films.Where(i => i.IdGenre == IdGenre).Count();
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
        public async Task<List<Films>> GetFilmsAsync(int skip = 0, int take = 0, bool LoadAllData = false)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (UserDB db = new UserDB())
                    {
                        // Если не выбрали загрузку всей инфы, то загрузить определенные поля
                        if (LoadAllData == false)
                        {
                            if (skip != 0 || take != 0)
                            {
                                // !!! Эти преобразования и выгрузка делаются для того, чтобы не загружать много информации с изображениями !!!
                                var lists = db.Films.OrderBy(i => i.IdFilm).Skip(skip).Take(take).Select(i => new { i.IdFilm, i.Name, i.Producers, i.Genres, i.Year, i.Country }).ToList();

                                List<Films> mylists = new List<Films>();

                                foreach (var item in lists)
                                {
                                    mylists.Add(new Films() { IdFilm = item.IdFilm, Producers = item.Producers, Country = item.Country, Name = item.Name, Year = item.Year, Genres = item.Genres });
                                }

                                return mylists;
                            }


                            // !!! Эти преобразования и выгрузка делаются для того, чтобы не загружать много информации с изображениями !!!
                            var list = db.Films.Select(i => new { i.IdFilm, i.Name, i.Producers, i.Genres, i.Year, i.Country }).ToList();

                            List<Films> mylist = new List<Films>();

                            foreach (var item in list)
                            {
                                mylist.Add(new Films() { IdFilm = item.IdFilm, Producers = item.Producers, Country = item.Country, Name = item.Name, Year = item.Year, Genres = item.Genres });
                            }

                            return mylist;
                        }

                        // Если выбрали загрузку всей инфы

                        // Проверяет сколько пропустить в выборке, сколько взять
                        if (skip != 0 || take != 0)
                        {
                            var films = db.Films.Include(i => i.Genres).Include(i => i.Producers).OrderBy(i => i.IdFilm).Skip(skip).Take(take).ToList();

                            return films;
                        }
                            

                        // Если выбрали загрузку всей инфы, то вернуть всю инфу
                        return db.Films.Include(i => i.Genres).Include(i => i.Producers).ToList();

                    }
                });
            }
            catch (Exception)
            {
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return null; // Возвращаем null, в случае, если пользователи не найдены или ошибка
        }


        /// <summary>
        /// Метод который загружает весь список фильмов по жанрам с пропусками
        /// </summary>
        /// <param name="idStatusGenre">Айди жанра фильма</param>
        /// <param name="skip">Пропускает количество фильмов в выборке</param>
        /// <param name="take">Берет количество фильмов в выборке</param>
        /// <param name="LoadAllData">Загрузка всей инфы (Постеры, комменты, инфа о фильме)</param>
        /// <returns>Коллекцию фильмов</returns>
        public async Task<List<Films>> GetFilmsAsync(byte idStatusGenre, int skip = 0, int take = 0, bool LoadAllData = false)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (UserDB db = new UserDB())
                    {
                        // Если не выбрали загрузку всей инфы, то загрузить определенные поля
                        if (LoadAllData == false)
                        {
                            if (skip != 0 || take != 0)
                            {
                                // !!! Эти преобразования и выгрузка делаются для того, чтобы не загружать много информации с изображениями !!!
                                var lists = db.Films.Where(i => i.IdGenre == idStatusGenre).OrderBy(i => i.IdFilm).Skip(skip).Take(take).Select(i => new { i.IdFilm, i.Name, i.Producers, i.Genres, i.Year, i.Country }).ToList();

                                List<Films> mylists = new List<Films>();

                                foreach (var item in lists)
                                {
                                    mylists.Add(new Films() { IdFilm = item.IdFilm, Producers = item.Producers, Country = item.Country, Name = item.Name, Year = item.Year, Genres = item.Genres });
                                }

                                return mylists;
                            }


                            // !!! Эти преобразования и выгрузка делаются для того, чтобы не загружать много информации с изображениями !!!
                            var list = db.Films.Where(i => i.IdGenre == idStatusGenre).Select(i => new { i.IdFilm, i.Name, i.Producers, i.Genres, i.Year, i.Country }).ToList();

                            List<Films> mylist = new List<Films>();

                            foreach (var item in list)
                            {
                                mylist.Add(new Films() { IdFilm = item.IdFilm, Producers = item.Producers, Country = item.Country, Name = item.Name, Year = item.Year, Genres = item.Genres });
                            }

                            return mylist;
                        }

                        // Если выбрали загрузку всей инфы

                        // Проверяет сколько пропустить в выборке, сколько взять
                        if (skip != 0 || take != 0)
                        {
                            var films = db.Films.Where(i => i.IdGenre == idStatusGenre).Include(i => i.Genres).Include(i => i.Producers).OrderBy(i => i.IdFilm).Skip(skip).Take(take).ToList();

                            return films;
                        }


                        // Если выбрали загрузку всей инфы, то вернуть всю инфу
                        return db.Films.Where(i => i.IdGenre == idStatusGenre).Include(i => i.Genres).Include(i => i.Producers).ToList();

                    }
                });
            }
            catch (Exception)
            {
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return null; // Возвращаем null, в случае, если пользователи не найдены или ошибка
        }

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
            Window window = new View.User.UserWindow(user);
            window.Show();
        }

        #endregion

    }
}
