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
        public string Password { get; set; }
        public bool Disabled { get; set; } = false;
        public byte[] Ava { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

}
