using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mes.Model
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public byte[] AttachFile { get; set; }
        public string FileName { get; set; }
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
    }
}
