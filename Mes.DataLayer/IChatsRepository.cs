﻿using System;
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
        void Delete(Guid idChat);
        bool AddMembers(IEnumerable<Guid> members, Guid idChat);
        bool DeleteMembers(IEnumerable<Guid> members, Guid idChat);
        Chat GetChat(Guid idChat);
    }
}
