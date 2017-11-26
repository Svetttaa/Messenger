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
using Mes.Client;

namespace Mes.Client
{
    public partial class LoginControl : UserControl
    {
        public LoginControl()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            warningLogin.Hide();
            warningPassword.Hide();

            if(String.IsNullOrWhiteSpace(txtLogin.Text))
            {
                warningLogin.Show();
                return;
            }
           
            if (String.IsNullOrWhiteSpace(txtPassword.Text))
            {
                warningPassword.Show();
                return;
            }
            User user = new User()
            {
                Name = txtLogin.Text,
                Password = txtPassword.Text
            };
            var response = (User)Client.LoginUser(user);

            if (response.Id != new Guid())
            {
                Properties.Settings.Default.CurrentUser = (User)response;
                ((StartForm)Parent).Close();
                MainForm MF = new MainForm();
                MF.Show();
            }
        }
    }
}
