using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Mes.Model;

namespace Mes.Client
{
    public partial class MainForm : Form
    {
        const int LabelSizeY = 50;
        List<Chat> Chats = new List<Chat>();
        public MainForm()
        {
            InitializeComponent();

            lblName.Text = Properties.Settings.Default.CurrentUser.Name;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Avatars", (Properties.Settings.Default.CurrentUser.Id).ToString() + ".png");
            if (File.Exists(path))
                picBoxAva.BackgroundImage = (Bitmap)((new ImageConverter()).ConvertFrom(Properties.Settings.Default.CurrentUser.Ava));
            Chats = (List<Chat>)Client.GetUserChats(Properties.Settings.Default.CurrentUser.Id);
            if (Chats.Any())
            {
                lblChats.Text = "Чаты";
                ChatsTable.Visible = true;
                foreach (var chat in Chats)
                {
                    var label = Client.CreateLabel(chat.Name, LabelSizeY, ChatsTable.Width, label_Click);
                    label.Name = chat.Id.ToString();

                    ChatsTable.RowCount++;
                    ChatsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, LabelSizeY));
                    ChatsTable.Controls.Add(label, 1, ChatsTable.RowCount - 1);

                    PictureBox avatarPB = new PictureBox()
                    {
                        BackgroundImage = Properties.Resources.chat,
                        BackgroundImageLayout = ImageLayout.Zoom,
                        Size = new Size(LabelSizeY, LabelSizeY)
    
                    };

                    if (chat.Members.Count() == 2)
                    {
                        byte[] avatar = chat.Members.First(x => x.Id != Properties.Settings.Default.CurrentUser.Id).Ava;

                        if (avatar != null && avatar.Any())
                            avatarPB.BackgroundImage = Client.FromBytesToBitmap(avatar);
                    }
                    ChatsTable.Controls.Add(avatarPB, 0, ChatsTable.RowCount - 1);

                }

                ChatsTable.RowCount++;
            }

        }

        private void backToStart_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CurrentUser = null;
            Close();
            StartForm ST = new StartForm();
            ST.Show();
            backToStart.Visible = false;
        }

        private void searchUser_Click(object sender, EventArgs e)
        {
            Close();
            SearchUsersForm SUF = new SearchUsersForm(Chats);
            SUF.Show();
        }

        private void editInfo_Click(object sender, EventArgs e)
        {
            Close();
            EditUser EU = new EditUser();
            EU.Show();
        }

        private void label_Click(object sender, EventArgs e)
        {
            
        }
    }
}
