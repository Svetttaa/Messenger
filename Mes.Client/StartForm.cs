using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mes.Client
{
    public partial class StartForm : Form
    {
        Control CurrentControl;

        public StartForm()
        {
            InitializeComponent();
        }

        private void startControl_Load(object sender, EventArgs e)
        {
            
        }
        public void ShowLoginControl()
        {
            startControl.Hide();
            LoginControl loginControl = new LoginControl()
            {
                Location = startControl.Location
            };
            this.Controls.Add(loginControl);
            backToStart.Visible = true;
            CurrentControl = loginControl;
        }

        public void ShowRegisterControl()
        {
            startControl.Hide();
            RegisterControl registerControl = new RegisterControl()
            {
                Location = startControl.Location
            };
            this.Controls.Add(registerControl);
            backToStart.Visible = true;
            CurrentControl = registerControl;
        }

        private void backToStart_Click(object sender, EventArgs e)
        {
            CurrentControl.Hide();
            startControl.Show();
            backToStart.Visible = false;
        }


    }
}
