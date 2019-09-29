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

        // Метод, который получает ВЕСЬ список фильмов.
        public async Task<List<Films>> GetFilmsAsync()
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (UserDB db = new UserDB())
                        return db.Films.Include(p => p.Genres).Include(p => p.Producers).ToList();
                });
            }
            catch (Exception)
            {
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return null; // Возвращаем null, в случае, если пользователи не найдены или ошибка
        }

        // Метод, который получает список фильмов по айди жанра.
        public async Task<List<Films>> GetFilmsAsync(byte idStatusGenre)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (UserDB db = new UserDB())
                        return db.Films.Include(p => p.Genres).Include(p => p.Producers).Where(i => i.IdGenre == idStatusGenre).ToList();
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
