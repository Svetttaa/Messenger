using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Mes.Model;
using NLog;



namespace Mes.DataLayer.Sql
{
    public class ChatsRepository : IChatsRepository
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        private readonly string _connectionString;
        private readonly IUsersRepository _usersRepository;
        public ChatsRepository(string connectionString, UsersRepository usersRepository)
        {
            _connectionString = connectionString;
            _usersRepository = usersRepository;
        }
        public void AddMembers(IEnumerable<Guid> members, Guid idChat)
        {
            logger.Debug($"Попытка добавления списка пользователей в чат id {idChat}");
            try
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
                        logger.Debug($"Список пользователей добавлен в чат id {idChat}");
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex,$"Ошибка при попытке добавления списка пользователей в чат id {idChat}");
                throw ex;
            }
        }

        public Chat Create(IEnumerable<Guid> members, string name)
        {
            logger.Debug($"Попытка создания чата с именем {name}");
            try
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
                        logger.Debug( $"Чат с именем {name} создан");
                        return chat;
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"Ошибка при попытке создания чата с именем {name}");
                throw ex;
            }
        }

        public void Delete(Guid idChat,Guid idUser)
        {
            logger.Debug($"Попытка удаления чата с id {idChat} пользователем с id {idUser}");
            try
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
                            {
                                logger.Error( $"Пользователя с id {idUser} нет в данном чате");
                                throw new ArgumentException($"Пользователя с id {idUser} нет в данном чате");
                            }
                               
                        }
                        command.CommandText = $"delete from Chats where Id='{idChat}'";
                        command.ExecuteNonQuery();
                        logger.Debug($"Удаление чата с id {idChat} пользователем с id {idUser}");
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"Ошибка при попытке создания  удаления чата с id {idChat} пользователем с id {idUser}");
                throw ex;
            }
        }

        public void DeleteMembers(IEnumerable<Guid> members, Guid idChat)
        {
            logger.Debug($"Попытка удаления списка пользователей из чата с id {idChat} ");
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        foreach (var x in members)
                        {
                            command.CommandText = $"delete from ChatMembers where ChatId='{idChat}' and UserId='{x}'";
                        }
                        command.ExecuteNonQuery();
                        logger.Debug($"Пользователи удалены из чата с id {idChat} ");
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"Ошибка при попытке удаления списка пользователей из чата с id {idChat}");
                throw ex;
            }
        }

        public Chat GetChat(Guid idChat)
        {
            logger.Debug($"Попытка получения данных о чате с id {idChat} ");
            try
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
                            {
                                logger.Error($"Чат с id {idChat} не найден");
                                throw new ArgumentException($"Чат с id {idChat} не найден");
                            }
                              Chat chat=new Chat
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            };
                            logger.Debug($"Получены данные о чате с id {idChat} ");
                            return chat;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex,$"Ошибка при получении данных о чате с id {idChat} ");
                throw ex;
            }
        }
        public IEnumerable<User> GetChatMembers(Guid idChat)
        {
            logger.Debug($"Попытка получения списка пользователей чата с id {idChat} ");
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        List<User> users = new List<User>();
                        command.CommandText = $"select UserId from chatMembers where ChatId='{idChat}'";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(_usersRepository.Get(reader.GetGuid(reader.GetOrdinal("UserId"))));
                            }
                               
                        }
                        
                        logger.Debug($"Получен список пользователей чата с id {idChat} ");
                        return users;
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"Ошибка при получении данных о пользователях чата с id {idChat} ");
                throw ex;
            }
        }
        public IEnumerable<Chat> GetUserChats(Guid idUser)
        {
            logger.Debug($"Попытка получения списка чатов у пользователя с id {idUser} ");
            try
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
                        List<Chat> chats = new List<Chat>();
                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                logger.Error($"Пользователь с id {idUser} не состоит ни в одном чате");
                                throw new ArgumentException($"Пользователь с id {idUser} не состоит ни в одном чате");
                            }
                            while(reader.Read())
                            {
                                Chat chat = new Chat
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("ChatId")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Members = GetChatMembers(reader.GetGuid(reader.GetOrdinal("ChatId")))
                                };
                                chats.Add(chat);
                            }
                            logger.Debug($"Получен список чатов у пользователя с id {idUser} ");
                            return chats;

                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Ошибка при получении списка чатов у пользователя с id {idUser}  ");
                throw ex;
            }

        }        
    }
}
