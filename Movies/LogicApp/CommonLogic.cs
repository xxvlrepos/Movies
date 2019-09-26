using Movies.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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




        // Метод, который получает список режисеров.
        public async Task<List<Producers>> GetProducersAsync()
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (UserDB db = new UserDB())
                        return db.Producers.ToList();
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

        // Метод, который получает список фильмов. Айди статуса жанра необязательный параметр. Если он не указан, то выдаст все фильмы
        public async Task<List<Films>> GetFilmsAsync(byte idStatusGenre = 0)
        {
            try
            {
                return await Task.Run(() =>
                {
                    // Если не передали статус аккаунта, то верни всех пользователей
                    if (idStatusGenre == 0)
                        using (UserDB db = new UserDB())
                            return db.Films.ToList();
                    // Иначе, если статус аккаунта передали, то верни аккаунты по статусу
                    else
                        using (UserDB db = new UserDB())
                            return db.Films.Where(i => i.IdGenre == idStatusGenre).ToList();
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
            catch (Exception)
            { 
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return null; // Возвращаем null, в случае, если пользователь не найден
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
