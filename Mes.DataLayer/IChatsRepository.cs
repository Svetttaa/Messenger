using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mes.Model;

namespace Mes.DataLayer
{
    public interface IChatsRepository
    {
        Chat Create(IEnumerable<Guid> members,string name);
        IEnumerable<User> GetUsersOfChat(Guid idChat);
        IEnumerable<Chat> GetUserChats(Guid idUser);
        void Delete(Guid idChat);

    }
}
