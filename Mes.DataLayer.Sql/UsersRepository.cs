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
                string path = @"D://Avatars//" + id.ToString();
                if (File.Exists(path))
                    File.Delete(path);
                file.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }

        public void ChangeName(Guid id, string name)
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
                            throw new ArgumentException($"Пользователь с id {id} не найден");
                        }
                    }
                    command.CommandText = $"update Users set Name=N'{name}' where Id='{id}'";
                    command.ExecuteNonQuery();
                    
                }
            }
        }

        public void ChangePassword(Guid id, string pass)
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
                            throw new ArgumentException($"Пользователь с id {id} не найден");
                        }
                    }
                    command.CommandText = $"update Users set Password=N'{pass}' where Id='{id}'";
                    command.ExecuteNonQuery();
                    
                }
            }
        }

        public User Create(User user)
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
                            return user;
                        }
                      
                    }
                }
            }
        }

        public void Delete(Guid id)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"update Users set Disabled='1' where Id='{id}'";
                    command.ExecuteNonQuery();
                }
            }
        }

        public User Get(Guid id)
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
                            throw new ArgumentException($"Пользователь с id {id} не найден");
                        return new User
                        {
                            Id = reader.GetGuid(reader.GetOrdinal("Id")),
                            Disabled = reader.GetBoolean(reader.GetOrdinal("Disabled")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Password = reader.GetString(reader.GetOrdinal("Password"))
                        };
                    }
                }
            }
        }
    }
}
