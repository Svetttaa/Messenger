﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mes.Client
{
    public partial class AttachFile : UserControl
    {
        public AttachFile(string nameFile)
        {
            InitializeComponent();
            lblFileName.Text = nameFile;
        }

    }
}
