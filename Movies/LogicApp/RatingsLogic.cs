using Movies.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.LogicApp
{
    /// <summary>
    /// Логика рейтинга фильмов
    /// </summary>
    /// 

    public class RatingsLogic
    {

        /// <summary>
        /// Получает рейтинг пользователя по фильму
        /// </summary>
        /// <param name="IdFilm">Айди фильма</param>
        /// <param name="IdUser">Айди юзера</param>
        /// <returns>Возвращает рейтинг пользователя по фильму</returns>
        public async Task<Ratings> GetMyRatingFilm(int IdFilm, int IdUser)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (UserDB db = new UserDB())
                    {
                        return db.Ratings.FirstOrDefault(i => i.IdFilm == IdFilm && i.IdUser == IdUser);
                    }
                });
            }
            catch (Exception)
            {
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return null; // Возвращаем null, в случае, если не найдено ничего
        }

        /// <summary>
        /// Получает список рейтингов фильма
        /// </summary>
        /// <param name="IdFilm">Айди фильма</param>
        /// <returns>Возвращает список рейтингов фильма</returns>
        public async Task<List<Ratings>> GetRatingsOnFilm(int IdFilm)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (UserDB db = new UserDB())
                    {
                        return db.Ratings.Where(i => i.IdFilm == IdFilm).ToList();
                    }
                });
            }
            catch (Exception)
            {
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return null; // Возвращаем null, в случае, если не найдено ничего
        }

        /// <summary>
        /// Добавляет рейтинг в БД
        /// </summary>
        /// <param name="rating">Рейтинг</param>
        /// <returns>Возвращает true в случае успеха</returns>
        public async Task<bool> AddRatingsOnFilm(Ratings rating)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (UserDB db = new UserDB())
                    {
                        // Если пользователь не добавил рейтинг то добавь
                        if (GetMyRatingFilm(rating.IdFilm, rating.IdUser) != null)
                        {
                            db.Ratings.Add(rating);
                            db.SaveChanges();

                            return true;
                        }

                        return false;
                    }
                });
            }
            catch (Exception)
            {
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return false; ; // Возвращаем null, в случае, если не найдено ничего
        }

        public async Task<bool> UpdateRatingsOnFilm(Ratings rating)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (UserDB db = new UserDB())
                    {
                        // Если у пользователя есть рейтинг то обнови
                        if (GetMyRatingFilm(rating.IdFilm, rating.IdUser) != null)
                        {
                            db.Entry(rating).State = System.Data.Entity.EntityState.Modified;;
                            db.SaveChanges();

                            return true;
                        }

                        return false;
                    }
                });
            }
            catch (Exception)
            {
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return false; ; // Возвращаем null, в случае, если не найдено ничего
        }
    }

    
}
