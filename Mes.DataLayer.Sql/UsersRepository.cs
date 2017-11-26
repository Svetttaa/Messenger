using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using Mes.DataLayer;
using Mes.Model;
using NLog;
using System.Data;
using System.Web;
using System.Web.Http;
using System.Net;
using System.Net.Http;

namespace Mes.DataLayer.Sql
{
    public class UsersRepository : IUsersRepository
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        private readonly string _connectionString;
        public UsersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User Login(string name, string password)
        {
            logger.Debug($"Попытка входа,имя: {name}, пароль: {password}");
            try
            {
                var userData = (DataTable)Helper.ExecuteQuery(_connectionString, $"select * from Users where Name=N'{name}' and Password=N'{password}'");
                if (userData.Rows.Count == 0)
                {
                    logger.Error($"Неверное имя {name} или пароль {password}");
                    throw new ArgumentException("Неверное имя или пароль");
                }
                User user = new User
                {
                    Id = (Guid)userData.Rows[0]["Id"],
                    Disabled = (bool)userData.Rows[0]["Disabled"],
                    Name = (string)userData.Rows[0]["Name"],
                    Password = (string)userData.Rows[0]["Password"]
                };

                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Avatars", (user.Id).ToString() + ".png");
                if (File.Exists(path))
                {
                    Bitmap b = (Bitmap)Bitmap.FromFile(path);

                    ImageConverter imageConverter = new ImageConverter();
                    user.Ava = (byte[])imageConverter.ConvertTo(b, typeof(byte[]));
                }

                logger.Debug($"Пользователь с id {user.Id} вошел в систему");
                return user;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"при попытке входа возникла ошибка при аргументах имя: {name}, пароль:{password}");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }

        }

        public void ChangeAvatar(Guid id, byte[] avatar)
        {
            logger.Debug($"Попытка изменения аватара, id пользователя: {id}");
            try
            {
                var userData = (DataTable)Helper.ExecuteQuery(_connectionString, $"select * from Users where Id='{id}'");
                if (userData.Rows.Count == 0)
                {
                    logger.Error($"Пользователя с id {id} нет в базе данных");
                    throw new ArgumentException($"Пользователь с id {id} не найден");
                }
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Avatars", (id).ToString() + ".png");
                if (File.Exists(path))
                    File.Delete(path);
                if (avatar != null && avatar.Any())
                {
                    Image avatarImage = (Bitmap)((new ImageConverter()).ConvertFrom(avatar));
                    avatarImage.Save(path);
                }
                logger.Debug($"Пользователь с id {id} успешно загрузил фото");
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"При попытке изменения аватара возникла ошибка при аргументах id пользователя {id}");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public void ChangeName(Guid id, string name)
        {
            logger.Debug($"Попытка изменения Имя у пользователя с id {id} на имя {name}");
            try
            {
                var userData = (int)Helper.ExecuteQuery(_connectionString, $"update Users set Name=N'{name}' where Id='{id}'");
                if (userData == 0)
                {
                    logger.Error($"Пользователя с id {id} нет в базе данных");
                    throw new ArgumentException($"Пользователь с id {id} не найден");
                }
                logger.Debug($"у пользователя с id {id} было изменено имя на {name}");
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"При попытке изменения имени возникла ошибка при аргументах id пользователя {id}");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public void ChangePassword(Guid id, string pass)
        {
            logger.Debug($"Попытка изменения пароля у пользователя с id {id}");
            try
            {
                var userData = (int)Helper.ExecuteQuery(_connectionString, $"update Users set Password=N'{pass}' where Id='{id}'");
                if (userData == 0)
                {
                    logger.Error($"Пользователя с id {id} нет в базе данных");
                    throw new ArgumentException($"Пользователь с id {id} не найден");
                }
                logger.Debug($"у пользователя с id {id} был изменен пароль на {pass}");
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"При попытке изменения пароля возникла ошибка при аргументах id пользователя {id}");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public User Create(User user)
        {
            logger.Debug($"Попытка создания пользователя с параметратми Имя {user.Name} Пароль {user.Password}");
            try
            {
                if ((int)Helper.ExecuteScalar(_connectionString, $"select count(*) from Users where Name=N'{user.Name}'") > 0)
                {
                    logger.Error($"Пользователь с именем {user.Name} уже существует");
                    throw new ArgumentException($"Пользователь с именем {user.Name} уже существует");
                }
                user.Id = Guid.NewGuid();
                Helper.ExecuteQuery(_connectionString, $"insert into Users (Id, Name, Password) values ('{user.Id}','{user.Name}', '{user.Password}')");

                if (user.Ava != null && user.Ava.Any())
                {
                    Image avatar = (Bitmap)((new ImageConverter()).ConvertFrom(user.Ava));
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Avatars", user.Id.ToString() + ".png");
                    avatar.Save(path);
                }

                logger.Debug($"Создан ользователь с параметрами id {user.Id}, имя {user.Name}, пароль {user.Password} ");
                return user;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"При попытке создания пользователя возникла ошибка при аргументах имя {user.Name}, пароль {user.Password} ");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public void Delete(Guid id)
        {
            logger.Debug($"Попытка удаления пользователя с id {id}");
            try
            {
                var userData = (int)Helper.ExecuteQuery(_connectionString, $"update Users set Disabled='1' where Id='{id}'");
                if (userData == 0)
                {
                    logger.Error($"Пользователя с id {id} нет в базе данных");
                    throw new ArgumentException($"Пользователь с id {id} не найден");
                }
                logger.Debug($"пользователь с id {id} удален (т.е. изменился статус его видимости Disabled с false на true)");
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"При попытке удаления пользователся с id {id} возникла ошибка");
               throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public User Get(Guid id)
        {
            logger.Debug($"Попытка получения данных пользователя с id {id}");
            try
            {

                var userData = (DataTable)Helper.ExecuteQuery(_connectionString, $"select * from Users where Id='{id}'");
                if (userData.Rows.Count == 0)
                {
                    logger.Error($"Пользователя с id {id} нет в базе данных");
                    throw new ArgumentException($"Пользователь с id {id} не найден");
                }
                User user = new User
                {
                    Id = (Guid)userData.Rows[0]["Id"],
                    Disabled = (bool)userData.Rows[0]["Disabled"],
                    Name = (string)userData.Rows[0]["Name"],
                    Password = (string)userData.Rows[0]["Password"]
                };
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Avatars", (user.Id).ToString() + ".png");
                if (File.Exists(path))
                {
                    Bitmap b = (Bitmap)Bitmap.FromFile(path);
                    ImageConverter imageConverter = new ImageConverter();
                    user.Ava = (byte[])imageConverter.ConvertTo(b, typeof(byte[]));
                }
                logger.Debug($"У Пользователя с id {id} получены данные");
                return user;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Ошибка при получении данных о пользователе с id {id}");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public IEnumerable<User> SearchUsers(Guid idUser, string name)
        {

            logger.Debug($"Попытка поиска пользователя с именем {name}");
            try
            {
                List<User> users = new List<User>();
                var userData = (DataTable)Helper.ExecuteQuery(_connectionString, $"select * from Users where Name like '%{name}%'");
                if (userData.Rows.Count == 0)
                {
                    logger.Error($"Пользователь с именем {name} не найден");
                    throw new ArgumentException($"Пользователь с именем {name} не найден");
                }
                foreach (DataRow item in userData.Rows)
                {
                    User user = new User()
                    {
                        Name = (string)item["Name"],
                        Id = (Guid)item["Id"]
                    };
                    users.Add(user);
                }
                logger.Debug($"Получен список пользователей чата с  с именем {name} ");
                return users;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Ошибка при попытке поиска пользователей с именем {name}");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }

        }

    }
}
