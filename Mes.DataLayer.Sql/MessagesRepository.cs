using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Mes.Model;
using NLog;
using System.Data;

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

        public IEnumerable<Message> GetAmountOfMessages(Guid chatId, int skip, int amount)
        {
            logger.Debug($"Попытка подгрузки сообщений с параметрами skip {skip}, amount {amount} в чате id {chatId}");
            try
            {
                List<Message> messages = new List<Message>();
                var userData = (DataTable)Helper.ExecuteQuery(_connectionString, $"select * from Messages where ChatId='{chatId}' order by Date desc " +
                            $"offset {skip} rows" +
                            $" fetch next {amount} rows only");

                foreach (DataRow item in userData.Rows)
                {
                    Message message = new Message
                    {
                        Id = (Guid)item["Id"],
                        Text = (string)item["Text"],
                        Date = (DateTime)item["Date"],
                        AttachPath = (string)item["AttachPath"],
                        ChatId = (Guid)item["ChatId"],
                        UserId = (Guid)item["UserId"]
                    };
                    messages.Add(message);
                }
                return messages;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"при попытке подгрузки сообщений в чате {chatId} возникла ошибка при аргументах skip: {skip}, amount:{amount}");
                throw ex;
            }
        }

        public Message Send(Guid userId, Guid chatId, string text, string attach)
        {
            logger.Debug($"Попытка отправки сообщения от пользователя {userId} в чате {chatId} с текстом {text} и файлом {attach}");
            try
            {
                if ((int)Helper.ExecuteScalar(_connectionString, $"select count(*) from ChatMembers where ChatId='{chatId}'and UserId='{userId}'") == 0)
                {
                    logger.Error($"Пользователя с id {userId} нет в данном чате");
                    throw new ArgumentException($"Пользователя с id {userId} нет в данном чате");
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
                Helper.ExecuteQuery(_connectionString, $"insert into Messages (Text,Date,AttachPath,ChatId,UserId,Id) " +
                        $"values (N'{text}','{message.Date}',N'{attach}','{chatId}','{userId}','{message.Id}')");

                logger.Debug($"Сообщение отправлено от пользователя {userId} в чате {chatId} с текстом {text} и файлом {attach}");
                return message;
            }

            catch (Exception ex)
            {
                logger.Error(ex, $"ошибка в попытке отправки сообщения от пользователя {userId} в чате {chatId} с текстом {text} и файлом {attach}");
                throw ex;
            }
        }
    }
}
