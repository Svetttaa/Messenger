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
        IEnumerable<User> GetChatMembers(Guid idChat);
        IEnumerable<Chat> GetUserChats(Guid idUser);
        void Delete(Guid idChat,Guid idUser);
        void AddMembers(IEnumerable<Guid> members, Guid idChat, Guid idUser);
        void DeleteMembers(IEnumerable<Guid> members, Guid idChat, Guid idUser);
        Chat GetChat(Guid idChat);
    }
}
