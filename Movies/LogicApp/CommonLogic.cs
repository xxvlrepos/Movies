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

        public CommonLogic()
        {
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
        /// Метод который загружает весь список фильмов
        /// </summary>
        /// <param name="LoadAllData">Загрузка всей инфы (Постеры, комменты, инфа о фильме)</param>
        /// <returns>Коллекцию фильмов</returns>
        public async Task<List<Films>> GetFilmsAsync(bool LoadAllData = false)
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
                            // !!! Эти преобразования и выгрузка делаются для того, чтобы не загружать много информации с изображениями !!!

                            var list = db.Films.Select(i => new { i.IdFilm, i.Name, i.Producers, i.Genres, i.Year, i.Country }).ToList();

                            List<Films> mylist = new List<Films>();

                            foreach (var item in list)
                            {
                                mylist.Add(new Films() { IdFilm = item.IdFilm, Producers = item.Producers, Country = item.Country, Name = item.Name, Year = item.Year, Genres = item.Genres });
                            }

                            return mylist;
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
        /// </summary>
        /// <param name="idStatusGenre">Айди жанра фильма</param>
        /// <param name="LoadAllData">Загрузка всей инфы (Постеры, комменты, инфа о фильме)</param>
        /// <returns>Коллекцию фильмов</returns>
        public async Task<List<Films>> GetFilmsAsync(byte idStatusGenre, bool LoadAllData = false)
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
                            // !!! Эти преобразования и выгрузка делаются для того, чтобы не загружать много информации с изображениями !!!

                            var list = db.Films.Where(i => i.IdGenre == idStatusGenre).Select(i => new { i.IdFilm, i.Name, i.Producers, i.Genres, i.Year, i.Country }).ToList();

                            List<Films> mylist = new List<Films>();

                            foreach (var item in list)
                            {
                                mylist.Add(new Films() { IdFilm = item.IdFilm, Producers = item.Producers, Country = item.Country, Name = item.Name, Year = item.Year, Genres = item.Genres });
                            }

                            return mylist;
                        }

                        // Если выбрали загрузку всей инфы, то вернуть всю инфу
                        return db.Films.Include(i => i.Genres).Include(i => i.Producers).Where(i => i.IdGenre == idStatusGenre).ToList();
                    }
                });
            }
            catch (Exception)
            {
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return null; // Возвращаем null, в случае, если пользователи не найдены или ошибка
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
                            return db.Users.FirstOrDefault(i => i.Login == login && i.Pass == password);
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

        public virtual void ShowWindow()
        {
            Window window = new View.User.UserWindow();
            window.Show();
        }

        #endregion

    }
}
