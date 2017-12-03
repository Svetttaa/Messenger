using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Mes.Model;
using NLog;
using System.Data;
using System.Net;



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

        public void AddMembers(IEnumerable<Guid> members, Guid idChat, Guid idUser)
        {
            logger.Debug($"Попытка добавления списка пользователей в чат id {idChat} пользователем с id {idUser}");
            try
            {
                if (Helper.CheckUserInChat(_connectionString, idUser, idChat) == false)
                {
                    logger.Error($"Пользователя с id {idUser} нет в данном чате");
                    throw new ArgumentException($"Пользователя с id {idUser} нет в данном чате");
                }
                foreach (var x in members)
                {
                    Helper.ExecuteQuery(_connectionString, $"insert into ChatMembers (UserId, ChatId) values ('{x}','{idChat}')");
                }

                logger.Debug($"Список пользователей добавлен в чат id {idChat} пользователем с id {idUser}");

            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Ошибка при попытке добавления списка пользователей в чат id {idChat}");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
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
                        logger.Debug($"Чат с именем {name} создан");
                        return chat;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Ошибка при попытке создания чата с именем {name}");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public void Delete(Guid idChat, Guid idUser)
        {
            logger.Debug($"Попытка удаления чата с id {idChat} пользователем с id {idUser}");
            try
            {

                if (Helper.CheckUserInChat(_connectionString, idUser, idChat) == false)
                {
                    logger.Error($"Пользователя с id {idUser} нет в данном чате");
                    throw new ArgumentException($"Пользователя с id {idUser} нет в данном чате");
                }

                Helper.ExecuteQuery(_connectionString, $"delete from Chats where Id='{idChat}'");

                logger.Debug($"Удаление чата с id {idChat} пользователем с id {idUser}");

            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Ошибка при попытке создания  удаления чата с id {idChat} пользователем с id {idUser}");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public void DeleteMembers(IEnumerable<Guid> members, Guid idChat, Guid idUser)
        {
            logger.Debug($"Попытка удаления списка пользователей из чата с id {idChat} пользователем с id {idUser}");
            try
            {
                if (Helper.CheckUserInChat(_connectionString, idUser, idChat) == false)
                {
                    logger.Error($"Пользователя с id {idUser} нет в данном чате");
                    throw new ArgumentException($"Пользователя с id {idUser} нет в данном чате");
                }
                if ((int)Helper.ExecuteScalar(_connectionString, $"select count(*) from ChatMembers where ChatId='{idChat}'") == members.Count())
                {
                    Helper.ExecuteQuery(_connectionString, $"delete from ChatMembers where ChatId='{idChat}'");
                    logger.Debug($"Чат с id {idChat} удален, за неимением там нет юзеров");
                }
                else if ((int)Helper.ExecuteScalar(_connectionString, $"select count(*) from ChatMembers where ChatId='{idChat}'") > members.Count())
                {
                    foreach (var x in members)
                    {
                        Helper.ExecuteQuery(_connectionString, $"delete from ChatMembers where ChatId='{idChat}' and UserId='{x}'");
                    }

                    logger.Debug($"Пользователи удалены из чата с id {idChat} ");
                }
                else
                {
                    logger.Error($"Список пользователей, которых нужно удалить, превышает список пользователей в чате {idChat}");
                    throw new ArgumentException($"Список пользователей, которых нужно удалить, превышает список пользователей в чате {idChat}");
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Ошибка при попытке удаления списка пользователей из чата с id {idChat} пользователем с id {idUser}");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public Chat GetChat(Guid idChat)
        {
            logger.Debug($"Попытка получения данных о чате с id {idChat} ");
            try
            {
                var userData = (DataTable)Helper.ExecuteQuery(_connectionString, $"select Name,Id from Chats where Id='{idChat}'");
                if (userData.Rows.Count == 0)
                {
                    logger.Error($"Чат с id {idChat} не найден");
                    throw new ArgumentException($"Чат с id {idChat} не найден");
                }

                Chat chat = new Chat
                {
                    Id = (Guid)userData.Rows[0]["Id"],
                    Name = (string)userData.Rows[0]["Name"]
                };
                logger.Debug($"Получены данные о чате с id {idChat} ");
                return chat;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Ошибка при получении данных о чате с id {idChat} ");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public IEnumerable<User> GetChatMembers(Guid idChat)
        {
            logger.Debug($"Попытка получения списка пользователей чата с id {idChat} ");
            try
            {
                List<User> users = new List<User>();
                var userData = (DataTable)Helper.ExecuteQuery(_connectionString, $"select UserId from chatMembers where ChatId='{idChat}'");
                foreach (DataRow item in userData.Rows)
                {
                    users.Add(_usersRepository.Get((Guid)item["UserId"]));
                }
                logger.Debug($"Получен список пользователей чата с id {idChat} ");
                return users;

            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Ошибка при получении данных о пользователях чата с id {idChat} ");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public IEnumerable<Chat> GetUserChats(Guid idUser)
        {
            logger.Debug($"Попытка получения списка чатов у пользователя с id {idUser} ");
            try
            {
                var userData = (DataTable)Helper.ExecuteQuery(_connectionString, $"select ChatMembers.ChatId,Chats.Name from " +
                            $"ChatMembers " +
                            $" inner join Chats on ChatMembers.ChatId=Chats.Id where ChatMembers.UserId='{idUser}'");
                if (userData.Rows.Count == 0)
                {
                    logger.Error($"Пользователь с id {idUser} не состоит ни в одном чате");
                    throw new ArgumentException($"Пользователь с id {idUser} не состоит ни в одном чате");
                }
                List<Chat> chats = new List<Chat>();
                foreach (DataRow item in userData.Rows)
                {
                    Chat chat = new Chat
                    {
                        Id = (Guid)item["ChatId"],
                        Name = (string)item["Name"],
                        Members = GetChatMembers((Guid)item["ChatId"])
                    };
                    chats.Add(chat);
                }
                logger.Debug($"Получен список чатов у пользователя с id {idUser} ");
                return chats;

            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Ошибка при получении списка чатов у пользователя с id {idUser}  ");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }

        }

       
    }
}
