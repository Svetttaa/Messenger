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
        Message Sent(Guid userId, Guid chatId, string text, string attach, DateTime date);
        Message Change(Guid messageId);

    }
}
