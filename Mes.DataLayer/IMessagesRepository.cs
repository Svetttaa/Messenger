using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mes.Model;

namespace Mes.DataLayer
{
    public interface IMessagesRepository
    {
        void Delete(Guid messageId);
        Message Send(Guid userId, Guid chatId, string text, string attach);
        Message Change(Guid messageId);
        IEnumerable<Message> GetAmount(Guid chatId,int skip,int amount);
    }
}
