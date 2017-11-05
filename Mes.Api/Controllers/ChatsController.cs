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
    public class ChatsController : ApiController
    {
        private static string connectionString = @"Server=localhost\SQLExpress;Initial Catalog=DBMes;
        Trusted_Connection=True;";
        static UsersRepository usersRepository = new UsersRepository(connectionString);
        ChatsRepository chatsRepository = new ChatsRepository(connectionString,usersRepository);

        [HttpPost, Route("api/chats/create")]
        public object Create([FromBody]Chat chat)
        {
            return chatsRepository.Create(chat.Members.Select(u=>u.Id).ToArray(), chat.Name);
        }

        [HttpGet,Route("api/chats/{id}")]
        public object GetChat(Guid id)
        {
            return chatsRepository.GetChat(id);
        }

        [HttpDelete,Route("api/chats/{idChat}/{idUser}")]
        public void DeleteChat(Guid idChat, Guid idUser)
        {
             chatsRepository.Delete(idChat,idUser);
        }

        [HttpGet,Route("api/chats/getChatMembers/{id}")]
        public object GetChatMembers(Guid id)
        {
            return chatsRepository.GetChatMembers(id);
        }

        [HttpGet, Route("api/chats/getUserChats/{id}")]
        public object GetUserChats(Guid id)
        {
            return chatsRepository.GetUserChats(id);
        }

        [HttpPost, Route("api/chats/addMembers")]
        public void AddMembers([FromBody]Chat chat)
        {
            chatsRepository.AddMembers(chat.Members.Select(u => u.Id).ToArray(), chat.Id);
        }

        [HttpPost, Route("api/chats/deleteMembers")]
        public void DeleteMembers([FromBody]Chat chat)
        {
            chatsRepository.DeleteMembers(chat.Members.Select(u => u.Id).ToArray(), chat.Id);
        }
    }
}
