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
        public User  Login(string name,string password)
        {
            logger.Debug($"Попытка входа,имя: { name}, пароль: { password}"); 
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"select Name,Password,Id,Disabled from Users where Name=N'{name}' and Password=N'{password}'";
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                User user = new User
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    Disabled = reader.GetBoolean(reader.GetOrdinal("Disabled")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Password = reader.GetString(reader.GetOrdinal("Password"))
                                };
                                logger.Debug($"Пользователь с id {user.Id} вошел в систему");
                                return user;
                               
                            }
                            else
                            {
                                logger.Error($"Неверное имя {name} или пароль {password}");
                                throw new ArgumentException("Неверное имя или пароль");
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            { 
                logger.Error(ex,$"при попытке входа возникла ошибка при аргументах имя: {name}, пароль:{password}");
                throw ex;
            }
            
        }
        public void ChangeAvatar(Guid id, Image file)
        {
            logger.Debug($"Попытка изменения аватара, id пользователя: {id}");
            try
            {
                string path = @"D://Avatars//" + id.ToString();
                if (File.Exists(path))
                    File.Delete(path);
                file.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                logger.Debug($"Пользователь с id {id} успешно загрузил фото");
            }
            catch(Exception ex)
            {
                logger.Error(ex,$"При попытке изменения аватара возникла ошибка при аргументах id пользователя {id}");
                throw ex;
            }
        }

        public void ChangeName(Guid id, string name)
        {
            logger.Debug($"Попытка изменения Имя у пользователя с id {id} на имя {name}");
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"select Password from Users where Id='{id}'";
                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                logger.Error( $"Пользователя с id {id} нет в базе данных");
                                throw new ArgumentException($"Пользователь с id {id} не найден");
                            }
                        }
                        command.CommandText = $"update Users set Name=N'{name}' where Id='{id}'";
                        command.ExecuteNonQuery();
                        logger.Debug($"у пользователя с id {id} было изменено имя на {name}");
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"При попытке изменения имени возникла ошибка при аргументах id пользователя {id}");
                throw ex;
            }
        }

        public void ChangePassword(Guid id, string pass)
        {
            logger.Debug($"Попытка изменения пароля у пользователя с id {id}");
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"select Password from Users where Id='{id}'";
                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                logger.Error($"Пользователя с id {id} нет в базе данных");
                                throw new ArgumentException($"Пользователь с id {id} не найден");
                            }
                        }
                        command.CommandText = $"update Users set Password=N'{pass}' where Id='{id}'";
                        command.ExecuteNonQuery();
                        logger.Debug($"у пользователя с id {id} был изменен пароль на {pass}");

                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex,$"При попытке изменения пароля возникла ошибка при аргументах id пользователя {id}");
                throw ex;
            }
        }

        public User Create(User user)
        {
            logger.Debug($"Попытка создания пользователя с параметратми Имя {user.Name} Пароль {user.Password}");
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"select * from Users where Name=N'{user.Name}'";
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Close();
                                logger.Error($"Пользователь с именем {user.Name} уже существует");
                                throw new ArgumentException($"Пользователь с именем {user.Name} уже существует");
                            }
                            else
                            {
                                reader.Close();
                                user.Id = Guid.NewGuid();
                                command.CommandText = $"insert into Users (Id, Name, Password) values " +
                                    $"(@id,@name, @password)";
                                command.Parameters.AddWithValue("@id", user.Id);
                                command.Parameters.AddWithValue("@name", user.Name);
                                command.Parameters.AddWithValue("@password", user.Password);
                                command.ExecuteNonQuery();
                                user.Disabled = false;
                                logger.Debug($"Создан ользователь с параметрами id {user.Id}, имя {user.Name}, пароль {user.Password} ");
                                return user;
                            }

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"При попытке создания пользователя возникла ошибка при аргументах имя {user.Name}, пароль {user.Password} ");
                throw ex;
            }
        }

        public void Delete(Guid id)
        {
            logger.Debug($"Попытка удаления пользователя с id {id}");
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"update Users set Disabled='1' where Id='{id}'";
                        command.ExecuteNonQuery();
                        logger.Debug($"пользователь с id {id} удален (т.е. изменился статус его видимости Disabled с false на true)");
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex,$"При попытке удаления пользователся с id {id} возникла ошибка");
                throw ex;
            }
        }

        public User Get(Guid id)
        {
            logger.Debug($"Попытка получения данных пользователя с id {id}");
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"select Name,Password,Id,Disabled from Users where Id='{id}'";
                        command.ExecuteNonQuery();
                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                logger.Error($"Пользователь с id {id} не найден");
                                throw new ArgumentException($"Пользователь с id {id} не найден");
                            }

                            User user = new User
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Disabled = reader.GetBoolean(reader.GetOrdinal("Disabled")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Password = reader.GetString(reader.GetOrdinal("Password"))
                            };
                            logger.Debug($"У Пользователя с id {id} получены данные");
                            return user;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"Ошибка при получении данных о пользователе с id {id}");
                throw ex;
            }
        }
    }
}
