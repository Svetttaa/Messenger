﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mes.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Avatar { get; set; }
        public string Password { get; set; }
        public bool Disabled { get; set; } = false;
    }
}
