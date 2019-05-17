using Movies.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.LogicApp
{

    /// <summary>
    /// Класс содержит общую логику работы пользователей
    /// </summary>

    class CommonLogic
    {
        // Метод авторизации в асинхронном режиме
        public async Task<Users> AuthorizationAsync(string login, string password)
        {
            try
            {
                if (login != string.Empty && password != string.Empty)
                {
                    //Создаем канал связи с бд
                    using (MyDB db = new MyDB())
                    {
                        // Возвращаем объект пользователя по логину и паролю
                        return await Task.Run(() =>
                        {
                            return db.Users.FirstOrDefault(i => i.Login == login && i.Pass == password);
                        });
                    }
                }
            }
            catch (Exception)
            { 
                // Обработать какую-нибудь ошибку (если она будет по ходу написания программы)
            }

            return null; // Возвращаем null, в случае, если пользователь не найден
        }

    }
}
