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

namespace Mes.Client
{
    public partial class RegisterControl : UserControl
    {
        string AvatarPath = String.Empty;

        public RegisterControl()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            txtLogin.BackColor = Color.White;
            txtPassword.BackColor = Color.White;
            txtPasswordAgain.BackColor = Color.White;
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                txtLogin.BackColor = Color.Red;
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.BackColor = Color.Red;
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPasswordAgain.Text))
            {
                txtPasswordAgain.BackColor = Color.Red;
                return;
            }
            if (txtPassword.Text != txtPasswordAgain.Text)
            {
                MessageBox.Show("Повторите пароль еще раз верно!");
                txtPasswordAgain.BackColor = Color.Red;
                txtPasswordAgain.Text = null;
                return;
            }

            User user = new User()
            {
                Name = txtLogin.Text,
                Password = txtPassword.Text
            };

            if (!String.IsNullOrWhiteSpace(AvatarPath))
                user.Ava = Client.FromFileToBytes(AvatarPath);

            var response = (User)Client.RegisterUser(user);

            if(response.Id != new Guid())
            {
                Properties.Settings.Default.CurrentUser = (User)response;
                ((StartForm)Parent).Close();
                MainForm MF = new MainForm();
                MF.Show();
            }
        }

        private void btnAva_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                AvatarPath = openFileDialog.FileName;
            }
        }

        
    }
}
