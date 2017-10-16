using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mes.Model;

namespace Mes.DataLayer.Sql
{
    public class ChatRepository : IChatsRepository
    {
        public Chat Create(IEnumerable<Guid> members, string name)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid idChat)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Chat> GetUserChats(Guid idUser)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsersOfChat(Guid idChat)
        {
            throw new NotImplementedException();
        }
    }
}
