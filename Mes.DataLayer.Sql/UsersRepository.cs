using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
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
        
        public bool ChangeAvatar(Guid id, byte[]ava)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"select Avatar from Users where Id='{id}'";
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new ArgumentException($"Пользователь с id {id} не найден");
                            return false;
                        }
                        command.CommandText = $"update Users set Avatar='{ava}' where Id='{id}'";
                        return true;
                    }
                }
            }
        }

        public bool ChangeName(Guid id,string name)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"select Name from Users where Id='{id}'";
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new ArgumentException($"Пользователь с id {id} не найден");
                            return false;
                        }
                        command.CommandText = $"update Users set Name='{name}' where Id='{id}'";
                        return true;
                    }
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
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new ArgumentException($"Пользователь с id {id} не найден");
                            return false;
                        }
                        command.CommandText = $"update Users set Password='{pass}' where Id='{id}'";
                        return true;
                    }
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
                    command.CommandText = $"insert into Users (Id, Name, Avatar, Password) values " +
                        $"('{user.Id}','{user.Name}', '{user.Avatar}', '{user.Password}')";
                    command.ExecuteNonQuery();
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
                    command.CommandText = $"update Users set Disabled='true' where Id='{id}'";
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
                    command.CommandText = $"select (Name,Avatar, Password) from Users where Id='{id}'";
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"Пользователь с id {id} не найден");
                        return new User
                        {
                            
                            Avatar = reader.GetSqlBinary(reader.GetOrdinal("Avatar")).Value,
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Password = reader.GetString(reader.GetOrdinal("Password"))
                        };
                    }
                }
            }
        }
    }
}
