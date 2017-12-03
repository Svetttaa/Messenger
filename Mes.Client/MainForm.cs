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

            var avatar = Properties.Settings.Default.CurrentUser.Ava;

            if (avatar != null && avatar.Any())
                picBoxAva.BackgroundImage = Client.FromBytesToBitmap(avatar);
            Chats = (List<Chat>)Client.GetUserChats(Properties.Settings.Default.CurrentUser.Id);
            if (Chats.Any())
            {
                lblChats.Text = "Чаты";
                ChatsTable.Visible = true;
                foreach (var chat in Chats)
                {
                    var label = Client.CreateLabel(chat.Name, LabelSizeY, ChatsTable.Width, label_Click);
                    label.Name = chat.Id.ToString();
                    label.Margin = new Padding(0, 15, 0, 5);
                    label.ContextMenuStrip = contextMenuStrip1;
                    ChatsTable.RowCount++;
                    ChatsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, LabelSizeY));
                    ChatsTable.Controls.Add(label, 1, ChatsTable.RowCount - 1);

                    PictureBox avatarPB = new PictureBox()
                    {
                        BackgroundImage = Properties.Resources.chat,
                        BackgroundImageLayout = ImageLayout.Zoom,
                        Size = new Size(LabelSizeY, LabelSizeY),
                        Name = chat.Id.ToString()
                    };

                    if (chat.Members.Count() == 2)
                    {
                        byte[] chatAvatar = chat.Members.First(x => x.Id != Properties.Settings.Default.CurrentUser.Id).Ava;

                        if (chatAvatar != null && chatAvatar.Any())
                            avatarPB.BackgroundImage = Client.FromBytesToBitmap(chatAvatar);
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
            Guid chatId = Guid.Parse(((Label)sender).Name);
            Close();
            Chat chat = (Chat)Client.GetChat(chatId);
            chat.Members = (List<User>)Client.GetChatMembers(chat.Id);
            ChatForm CF = new ChatForm(chat);
            CF.Show();

        }



        private void удалитьЧатToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Guid chatId = Guid.Parse(((Label)sender).Name);
            //Client.DeleteChat(chatId, Properties.Settings.Default.CurrentUser.Id);
            //((Label)sender).Height = 0;
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    Guid chatId = Guid.Parse(sourceControl.Name);
                    int rowIndex = ChatsTable.GetRow(sourceControl);
                    Client.DeleteChat(chatId, Properties.Settings.Default.CurrentUser.Id);
                    RemoveRow(ChatsTable, rowIndex);
                    //foreach (var c in ChatsTable.Controls.Find(chatId.ToString(), true))
                    //    c.Height = 0;
                    
                    //ChatsTable.SetRow(new Label() { Height = 0, AutoSize = false }, rowIndex);

                }
            }
        }

        private void RemoveRow(TableLayoutPanel panel, int index)
        {
            if (index >= panel.RowCount)
            {
                return;
            }

            // delete all controls of row that we want to delete
            for (int i = 0; i < panel.ColumnCount; i++)
            {
                var control = panel.GetControlFromPosition(i, index);
                panel.Controls.Remove(control);
            }

            // move up row controls that comes after row we want to remove
            for (int i = index + 1; i < panel.RowCount; i++)
            {
                for (int j = 0; j < panel.ColumnCount; j++)
                {
                    var control = panel.GetControlFromPosition(j, i);
                    if (control != null)
                    {
                        panel.SetRow(control, i - 1);
                    }
                }
            }

            // remove last row
            //panel.RowStyles.RemoveAt(panel.RowCount - 1);
            //panel.RowCount--;
        }
    }
}
