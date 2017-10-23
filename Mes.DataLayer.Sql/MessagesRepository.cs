using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Mes.Model;

namespace Mes.DataLayer.Sql
{
    class MessagesRepository : IMessagesRepository
    {
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"select * from Messages where ChatId='{chatId}' order by Date " +
                        $"offset '{skip}' rows" +
                        $"fetch next '{amount}' rows only";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                          yield return new Message
                        {
                              Id = reader.GetGuid(reader.GetOrdinal("Id")),
                              Text = reader.GetString(reader.GetOrdinal("Text")),
                              Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                              AttachPath = reader.GetString(reader.GetOrdinal("AttachPath")),
                              ChatId = reader.GetGuid(reader.GetOrdinal("ChatId")),
                              UserId = reader.GetGuid(reader.GetOrdinal("UserId"))
                          };
                    }
                }


            }
        }

        public Message Send(Guid userId, Guid chatId, string text, string attach)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var message = new Message
                {
                    Id = Guid.NewGuid(),
                    Text = text,
                    Date = DateTime.Now,
                    AttachPath = attach,
                    ChatId = chatId,
                    UserId = userId
                };
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"insert into Messages (Text,Date,AttachPath,ChatId,UserId,Id) " +
                        $"values ('{text}','{message.Date}','{attach}','{chatId}','{userId}','{message.Id}')";
                    command.ExecuteNonQuery();
                }
                return message;
            }
        }
    }
}
