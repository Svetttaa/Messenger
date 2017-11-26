using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mes.Model;
using Mes.DataLayer.Sql;

namespace Mes.Api.Controllers
{
    public class MessagesController : ApiController
    {
        private static string connectionString = @"Server=localhost\SQLExpress;Initial Catalog=DBMes;
        Trusted_Connection=True;";
        static UsersRepository usersRepository = new UsersRepository(connectionString);
        static ChatsRepository chatsRepository = new ChatsRepository(connectionString, usersRepository);
        MessagesRepository messagesRepository = new MessagesRepository(connectionString, usersRepository, chatsRepository);

        [HttpPost, Route("api/messages/send")]
        public object Send([FromBody]Message  message)
        {
            return messagesRepository.Send(message.UserId, message.ChatId, message.Text, message.AttachPath);
        }

        [HttpGet, Route("api/messages/getAmount/{chatId}/{skip}/{amount}")]
        public object GetAmount(Guid chatId,int skip,int amount)
        {
            return messagesRepository.GetAmountOfMessages(chatId,skip,amount);
        }
    }
}
