using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Mes.Model;
using NLog;
using System.Data;
using System.Net;

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


        public void Delete(Guid idMessage)
        {
            logger.Debug($"Попытка удаления сообщения {idMessage}");
            try
            {

                Helper.ExecuteQuery(_connectionString, $"delete from Messages where Id='{idMessage}'");

                logger.Debug($"Удаление сообщения {idMessage}");

            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Ошибка при попытке  удаления сообщения {idMessage}");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }
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
                        AttachFile = (byte[])item["AttachFile"],
                        FileName = (string)item["FileName"],
                        ChatId = (Guid)item["ChatId"],
                        UserId = (Guid)item["UserId"]
                    };
                    messages.Add(message);
                }
                logger.Debug($"Сообщения с параметрами skip {skip}, amount {amount} в чате id {chatId} подгружены");
                return messages;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"при попытке подгрузки сообщений в чате {chatId} возникла ошибка при аргументах skip: {skip}, amount:{amount}");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public IEnumerable<Message> SearchMessage(string text, Guid chatId)
        {
            logger.Debug($"Попытка поиска сообщений с текстом {text} в чате id={chatId}");
            try
            {
                List<Message> messages = new List<Message>();
                var messageData = (DataTable)Helper.ExecuteQuery(_connectionString, $"select * from Messages where ChatId='{chatId}' and Text like '%{text}%'");
                if (messageData.Rows.Count == 0)
                {
                    logger.Error($"сообщений с текстом {text} в чате id={chatId} нет");
                    throw new ArgumentException($"сообщений с текстом {text} в чате id={chatId} нет");
                }
                foreach (DataRow item in messageData.Rows)
                {
                    Message message = new Message
                    {
                        Id = (Guid)item["Id"],
                        Text = (string)item["Text"],
                        Date = (DateTime)item["Date"],
                        AttachFile = (byte[])item["AttachFile"],
                        FileName = (string)item["FileName"],
                        ChatId = (Guid)item["ChatId"],
                        UserId = (Guid)item["UserId"]
                    };
                    messages.Add(message);
                }
                logger.Debug($"Сообщения с текстом {text} в чате id={chatId} найдены");
                return messages;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"при попытке поиска сообщений с текстом {text} в чате id={chatId} возникла ошибка");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public Message Send(Guid userId, Guid chatId, string text, byte[] attach, string fileName)
        {
            logger.Debug($"Попытка отправки сообщения от пользователя {userId} в чате {chatId} с текстом {text} и файлом {fileName}");
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
                    AttachFile = attach,
                    FileName = fileName,
                    ChatId = chatId,
                    UserId = userId
                };

                string query = $"insert into Messages (Text,Date,AttachFile,FileName,ChatId,UserId,Id) " +
                        $"values (N'{text}','{message.Date}',@Content,'{fileName}','{chatId}','{userId}','{message.Id}')";

                using (SqlConnection _con = new SqlConnection(_connectionString))
                using (SqlCommand _cmd = new SqlCommand(query, _con))
                {
                    SqlParameter param = _cmd.Parameters.Add("@Content", SqlDbType.VarBinary);
                    param.Value = attach ?? (new byte[0]);

                    _con.Open();
                    _cmd.ExecuteNonQuery();
                    _con.Close();
                }

                logger.Debug($"Сообщение отправлено от пользователя {userId} в чате {chatId} с текстом {text} и файлом {fileName}");
                return message;
            }

            catch (Exception ex)
            {
                logger.Error(ex, $"ошибка в попытке отправки сообщения от пользователя {userId} в чате {chatId} с текстом {text} и файлом {fileName}");
                throw Helper.GetHttpException(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
