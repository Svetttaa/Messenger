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
    public partial class EditUser : Form
    {
        string AvatarPath = String.Empty;
        public EditUser()
        {
            InitializeComponent();
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {

        }

        private void backToMain_Click(object sender, EventArgs e)
        {
            Close();
            MainForm MF = new MainForm();
            MF.Show();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            User user = new User();
            user = Properties.Settings.Default.CurrentUser;


            if (!string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                if (txtLogin.Text == Properties.Settings.Default.CurrentUser.Name)
                    return;
                user.Name = txtLogin.Text;
                Client.ChangeName(user);
            }
            if (!string.IsNullOrWhiteSpace(txtNewPass.Text) && !string.IsNullOrWhiteSpace(txtNewPassAgain.Text) && !string.IsNullOrWhiteSpace(txtOldPass.Text))
            {
                if (txtOldPass.Text != Properties.Settings.Default.CurrentUser.Password)
                {
                    MessageBox.Show("вы ввели старый пароль не верно!");
                    return;
                }
                   
                if(txtNewPassAgain.Text == txtNewPass.Text)
                {
                    if (txtNewPass.Text == Properties.Settings.Default.CurrentUser.Password)
                        return;
                    user.Password = txtNewPass.Text;
                    Client.ChangePassword(user);
                }
                else
                {
                    MessageBox.Show("Проверьте новый пароль и повторите его!");
                    return;
                }
            }
            if (!String.IsNullOrWhiteSpace(AvatarPath))
            {
                user.Ava = FromFileToBytes(AvatarPath);
                Client.ChangeAvatar(user);
            }
                

            Properties.Settings.Default.CurrentUser = user;
            Close();
            MainForm MF = new MainForm();
            MF.Show();

        }

        private void btnChangeAvatar_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                AvatarPath = openFileDialog1.FileName;
            }
        }
        public static byte[] FromFileToBytes(string path)
        {
            Bitmap b = (Bitmap)Bitmap.FromFile(path);

            ImageConverter imageConverter = new ImageConverter();
            return (byte[])imageConverter.ConvertTo(b, typeof(byte[]));
        }

        private void btnDisabled_Click(object sender, EventArgs e)
        {
            var result=MessageBox.Show("Вы точно хотите отключить свой профиль?","Отключение профиля", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Client.DeleteUser(Properties.Settings.Default.CurrentUser.Id);
                Close();
                StartForm SF = new StartForm();
                SF.Show();
            }
        }
    }
}
