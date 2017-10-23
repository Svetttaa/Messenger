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




namespace Mes.DataLayer.Sql
{
    public class UsersRepository : IUsersRepository
    {
        private readonly string _connectionString;
        public UsersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool ChangeAvatar(Guid id, Image file)
        {
            try
            {
                string path = @"D://Avatars//" + id.ToString();
                if (File.Exists(path))
                    File.Delete(path);

                file.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                        
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangeName(Guid id, string name)
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
                            return false;
                            throw new ArgumentException($"Пользователь с id {id} не найден");
                        }
                    }
                    command.CommandText = $"update Users set Name='{name}' where Id='{id}'";
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool ChangePassword(Guid id, string pass)
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
                            return false;
                            throw new ArgumentException($"Пользователь с id {id} не найден");
                        }
                    }
                    command.CommandText = $"update Users set Password='{pass}' where Id='{id}'";
                    command.ExecuteNonQuery();
                    return true;
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
                    user.Id = Guid.NewGuid();
                    command.CommandText = $"insert into Users (Id, Name, Password) values " +
                        $"('{user.Id}','{user.Name}', '{user.Password}')";
                    command.ExecuteNonQuery();
                    user.Disabled = false;
                    return user;
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
