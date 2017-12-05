using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mes.Model;
using System.IO;

namespace Mes.Client
{
    public partial class NameUserControl : UserControl
    {
       // const int LabelSizeY = 50;
        public NameUserControl(User user)
        {
            InitializeComponent();

            if (user.Ava != null && user.Ava.Any())
            {
                pbUserAva.Image = Client.FromBytesToBitmap(user.Ava);
            }
            else
            {
                pbUserAva.Image = Properties.Resources.whaleIcon;
           }
            lblUserName.Text = user.Name;
        }
    }
}
