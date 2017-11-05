using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Mes.Model;



namespace Mes.DataLayer.Sql
{
    public class ChatsRepository : IChatsRepository
    {
        private readonly string _connectionString;
        private readonly IUsersRepository _usersRepository;
        public ChatsRepository(string connectionString, UsersRepository usersRepository)
        {
            _connectionString = connectionString;
            _usersRepository = usersRepository;
        }
        public void AddMembers(IEnumerable<Guid> members, Guid idChat)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    foreach (var x in members)
                    {
                        command.CommandText = $"insert into ChatMembers (UserId, ChatId) values ('{x}','{idChat}')";
                    }
                    command.ExecuteNonQuery();
                    
                }
            }
        }

        public Chat Create(IEnumerable<Guid> members, string name)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var chat = new Chat
                    {
                        Name = name,
                        Id = Guid.NewGuid(),
                       // Members = members;
                    };

                    using (var command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandText = $"insert into chats (id, name) values ('{chat.Id}', N'{chat.Name}')";
                        command.ExecuteNonQuery();
                    }
                    foreach (var userId in members)
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandText = $"insert into ChatMembers (ChatId, UserId) values ('{chat.Id}', '{userId}')";
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                    chat.Members = members.Select(x => _usersRepository.Get(x));
                    return chat;
                }
            }  
        }

        public void Delete(Guid idChat,Guid idUser)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"select * from ChatMembers where ChatId='{idChat}'and UserId='{idUser}'";
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"Пользователя с id {idUser} нет в данном чате");
                    }
                    command.CommandText = $"delete from Chats where Id='{idChat}'";
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteMembers(IEnumerable<Guid> members, Guid idChat)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    foreach(var x in members )
                    {
                        command.CommandText = $"delete from ChatMembers where ChatId='{idChat}' and UserId='{x}'";
                    }
                    command.ExecuteNonQuery();
                    
                }
            }
        }

        public Chat GetChat(Guid idChat)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"select Name,Id from Chats where Id='{idChat}'";
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"Чат с id {idChat} не найден");
                        return new Chat
                        {
                            Id = reader.GetGuid(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };
                    }
                }
            }
        }
        public IEnumerable<User> GetChatMembers(Guid idChat)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"select UserId from chatMembers where ChatId='{idChat}'";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            yield return _usersRepository.Get(reader.GetGuid(reader.GetOrdinal("UserId")));
                    }
                }
            }
        }
        public IEnumerable<Chat> GetUserChats(Guid idUser)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"select ChatMembers.ChatId,Chats.Name from " +
                        $"ChatMembers " +
                        $" inner join Chats on ChatMembers.ChatId=Chats.Id where ChatMembers.UserId='{idUser}'";
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"Пользователь с id {idUser} не состоит ни в одном чате");
                       yield return new Chat
                        {
                            Id = reader.GetGuid(reader.GetOrdinal("ChatId")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Members= GetChatMembers(reader.GetGuid(reader.GetOrdinal("ChatId")))
                        };
                    }
                }
            }
        }        
    }
}
