﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mes.Model
{
    class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool SelfDestroy { get; set; }
        public string AttachPath { get; set; }
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
    }
}
