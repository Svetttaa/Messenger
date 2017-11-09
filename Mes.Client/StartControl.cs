using System;
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
    public partial class StartControl : UserControl
    {
        public StartControl()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            (Parent as StartForm).ShowLoginControl();
            

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            (Parent as StartForm).ShowRegisterControl();
        }
    }
}
