using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Mes.Model;
using NLog;

namespace Mes.DataLayer.Sql
{
   public class MessagesRepository : IMessagesRepository
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        private readonly string _connectionString;
        private readonly IUsersRepository _usersRepository;
        private readonly IChatsRepository _chatsRepository;
        public MessagesRepository(string connectionString, UsersRepository usersRepository, ChatsRepository chatsRepository)
        {
            _connectionString = connectionString;
            _usersRepository = usersRepository;
            _chatsRepository = chatsRepository;
        }
        public Message Change(Guid messageId)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid messageId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetAmount(Guid chatId, int skip, int amount)
        {
            logger.Debug($"Попытка подгрузки сообщений с параметрами skip {skip}, amount {amount} в чате id {chatId}");
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        List<Message> messages = new List<Message>();
                        command.CommandText = $"select * from Messages where ChatId='{chatId}' order by Date " +
                            $"offset {skip} rows" +
                            $" fetch next {amount} rows only";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Message message = new Message
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    Text = reader.GetString(reader.GetOrdinal("Text")),
                                    Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                    AttachPath = reader.GetString(reader.GetOrdinal("AttachPath")),
                                    ChatId = reader.GetGuid(reader.GetOrdinal("ChatId")),
                                    UserId = reader.GetGuid(reader.GetOrdinal("UserId"))
                                };
                                messages.Add(message);
                            }
                            return messages;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex,$"при попытке подгрузки сообщений в чате {chatId} возникла ошибка при аргументах skip: {skip}, amount:{amount}");
                throw ex;
            }
        }

        public Message Send(Guid userId, Guid chatId, string text, string attach)
        {
            logger.Debug($"Попытка отправки сообщения от пользователя {userId} в чате {chatId} с текстом {text} и файлом {attach}");
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"select * from ChatMembers where ChatId='{chatId}'and UserId='{userId}'";
                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                logger.Error($"Пользователя с id {userId} нет в данном чате");
                                throw new ArgumentException($"Пользователя с id {userId} нет в данном чате");
                            }
                                
                        }

                        var message = new Message
                        {
                            Id = Guid.NewGuid(),
                            Text = text,
                            Date = DateTime.Now,
                            AttachPath = attach,
                            ChatId = chatId,
                            UserId = userId
                        };

                        {
                            command.CommandText = $"insert into Messages (Text,Date,AttachPath,ChatId,UserId,Id) " +
                                $"values (N'{text}','{message.Date}',N'{attach}','{chatId}','{userId}','{message.Id}')";
                            command.ExecuteNonQuery();
                        }
                        logger.Debug($"Сообщение отправлено от пользователя {userId} в чате {chatId} с текстом {text} и файлом {attach}");
                        return message;
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex,$"ошибка в попытке отправки сообщения от пользователя {userId} в чате {chatId} с текстом {text} и файлом {attach}");
                throw ex;
            }
        }
    }
}
