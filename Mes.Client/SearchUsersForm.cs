using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mes.Model;

namespace Mes.Client
{
    public partial class SearchUsersForm : Form
    {
        List<Chat> ChatsFromMainForm = new List<Chat>();
        List<Guid> AlreadyAddedUsers = new List<Guid>();
        public SearchUsersForm(IEnumerable<Chat> chats)
        {
            InitializeComponent();
            foreach (var c in chats.Where(x => x.Members.Count() == 2))
            {
                AlreadyAddedUsers.Add(c.Members.First(x => x.Id != Properties.Settings.Default.CurrentUser.Id).Id);
                ChatsFromMainForm.Add(c);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tblUsers.Controls.Clear();
            tblUsers.RowCount = 1;
            List<User> users = new List<User>();
            users.Clear();
            const int LabelSizeY = 50;
            if (!String.IsNullOrWhiteSpace(txtSearch.Text))
            {
                users = (List<User>)Client.SearchUsers(Properties.Settings.Default.CurrentUser.Id, txtSearch.Text);
                if (users.Any())
                {
                    //tblUsers.Visible = true;
                    tblUsers.RowStyles[0].Height = 20;
                    foreach (var u in users)
                    {
                        if (u.Id == Properties.Settings.Default.CurrentUser.Id)
                            continue;

                        var label = Client.CreateLabel(u.Name, LabelSizeY, tblUsers.Width);
                        label.Name = u.Id.ToString();

                        tblUsers.RowCount++;
                        tblUsers.RowStyles.Add(new RowStyle(SizeType.Absolute, LabelSizeY));
                        tblUsers.Controls.Add(label, 1, tblUsers.RowCount - 1);

                        PictureBox avatarPB = Client.CreatePictureBox(LabelSizeY, LabelSizeY);

                        if (u.Ava != null && u.Ava.Any())
                        {
                            avatarPB.BackgroundImage = Client.FromBytesToBitmap(u.Ava);
                        }
                        else
                        {
                            avatarPB.BackgroundImage = Properties.Resources.whaleIcon;
                        }
                        tblUsers.Controls.Add(avatarPB, 0, tblUsers.RowCount - 1);

                        PictureBox actionPB = Client.CreatePictureBox(LabelSizeY, LabelSizeY, (u.Id).ToString());

                        if (AlreadyAddedUsers.Contains(u.Id))
                        {
                            actionPB.BackgroundImage = Properties.Resources.chat;
                            actionPB.Cursor = Cursors.Hand;
                            actionPB.Click += new EventHandler(OpenChatOnClick);
                        }
                        else
                        {
                            actionPB.BackgroundImage = Properties.Resources.add_user;
                            actionPB.Cursor = Cursors.Hand;
                            actionPB.Click += new EventHandler(CreateChatOnClick);
                        }

                        tblUsers.Controls.Add(actionPB, 2, tblUsers.RowCount - 1);
                    }

                    tblUsers.RowCount++;
                }
                else
                {
                    MessageBox.Show("Попробуйте ввести другое имя в поисковую строку");
                }
            }
        }


        private void OpenChatOnClick(object sender, EventArgs e)
        {
            Guid idUser = Guid.Parse(((PictureBox)sender).Name);

            foreach (var c in ChatsFromMainForm)
            {
                if (c.Members.First(x => x.Id != Properties.Settings.Default.CurrentUser.Id).Id == idUser)
                {
                    Hide();
                    ChatForm CF = new ChatForm(c);
                    CF.Show();
                }
            }

        }
        private void CreateChatOnClick(object sender, EventArgs e)
        {
            Guid idUser = Guid.Parse(((PictureBox)sender).Name);
            Chat newChat = new Chat()
            {
                Name = Client.GetUser(idUser).Name+", "+ Properties.Settings.Default.CurrentUser.Name,
                Members = new List<User>() { Client.GetUser(Properties.Settings.Default.CurrentUser.Id), Client.GetUser(idUser) }
            };
            newChat = (Chat)Client.CreateChat(newChat);
            Hide();
            ChatForm CF = new ChatForm(newChat);
            CF.Show();
        }

        private void backToStart_Click(object sender, EventArgs e)
        {
            Hide();
            MainForm MF = new MainForm();
            MF.Show();
        }

        private void pbBackToMain_Click(object sender, EventArgs e)
        {
            Hide();
            MainForm MF = new MainForm();
            MF.Show();
        }
    }
}
