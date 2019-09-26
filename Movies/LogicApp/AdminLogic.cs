using Movies.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Movies.LogicApp
{
    /// <summary>
    /// Класс содержит общую логику работы администраторов
    /// </summary>
    /// 
    class AdminLogic : CommonLogic
    {

        // Метод, который добавляет актера
        public async Task<bool> AddActor(Actors actor)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (MyDB db = new MyDB())
                    {
                        db.Actors.Add(actor);
                        db.SaveChanges();

                        return true;
                    }
                });

                return true; // т.к. успешно редактирован актер
            }
            catch (Exception)
            {
                // Тут можно обработать ошибку как-нибудь ..

                return false; // т.к. неудачно добавлен актер
            }
        }

        // Метод, который редактирует актера
        public async Task<bool> EditActor(Actors actor)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (MyDB db = new MyDB())
                    {
                        db.Entry(actor).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        return true;
                    }
                });

                return true; // т.к. успешно редактирован актер
            }
            catch (Exception)
            {
                // Тут можно обработать ошибку как-нибудь ..

                return false; // т.к. неудачно редактирован актер (Ошибка)
            }
        }

        // Метод, который получает список актеров.
        public async Task<List<Actors>> GetActorsAsync()
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (MyDB db = new MyDB())
                        return db.Actors.ToList();
                });
            }
            catch (Exception)
            {
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return null; // Возвращаем null, в случае, если пользователи не найдены или ошибка
        }

        // Метод, который добавляет фильм в БД. И возвращает true в случае успеха
        public bool AddFilm(Films film)
        {
            try
            {
                using (MyDB db = new MyDB())
                {
                    db.Films.Add(film);
                    db.SaveChanges();

                    return true; // Возвращаем true, т.к. фильм успешно добавлен
                }
            }
            catch (Exception)
            {

            }

            return false; // faslse, т.к. фильм не удалось добавить
        }

        // Метод, который получает список пользователей. Айди статуса необязательный параметр. Если он не указан, то выдаст всех пользователей
        public async Task<List<Users>> GetUsersAsync(byte idStatus = 0)
        {
            try
            {
                using (MyDB db = new MyDB())
                {
                    return await Task.Run(() =>
                    {
                        // Если не передали статус аккаунта, то верни всех пользователей
                        if (idStatus == 0)
                            return db.Users.ToList();
                        // Иначе, если статус аккаунта передали, то верни аккаунты по статусу
                        else
                            return db.Users.Where(i => i.IdStatus == idStatus).ToList();
                    });
                }
            }
            catch (Exception)
            {
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return null; // Возвращаем null, в случае, если пользователи не найдены или ошибка
        }


        #region Работа с окнами

        public override void ShowWindow()
        {
            Window window1 = new View.Admin.AdminWindow();
            window1.Show();
        }

        #endregion
    }
}
