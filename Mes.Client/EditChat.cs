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
    public partial class EditChat : Form
    {
        Chat ChatOnForm;
        List<User> SelectedUsersForAdd = new List<User>();
        List<User> SelectedUsersForDelete = new List<User>();
        public EditChat(Chat chat)
        {
            InitializeComponent();

            ChatOnForm = chat;
            lblName.Text += " " + ChatOnForm.Name;
            List<User> thisChatUsers = ChatOnForm.Members.Where(x => x.Id != Properties.Settings.Default.CurrentUser.Id).ToList();

            foreach (var u in thisChatUsers)
            {
                chBoxDelete.Items.Add(u);
            }

            var privateChats = (Client.GetUserChats(Properties.Settings.Default.CurrentUser.Id) as List<Chat>)
                .Where(x => x.Members.Count() == 2);

            foreach (var c in privateChats)
            {
                var user = c.Members.First(x => x.Id != Properties.Settings.Default.CurrentUser.Id);

                if (!thisChatUsers.Select(x => x.Id).Contains(user.Id))
                    chBoxAdd.Items.Add(user);
            }
        }


        private void btnDeleteChat_Click(object sender, EventArgs e)
        {
            Client.DeleteChat(ChatOnForm.Id, Properties.Settings.Default.CurrentUser.Id);
            Close();
            MainForm MF = new MainForm();
            MF.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbName.Text))
            {
                ChatOnForm.Name = txbName.Text;
                Client.ChangeNameOfChat(ChatOnForm);
            }

            if (AddOrRemoveMembers(chBoxAdd) != null) Client.AddMembers(AddOrRemoveMembers(chBoxAdd));

            if (AddOrRemoveMembers(chBoxDelete) != null) Client.DeleteMembers(AddOrRemoveMembers(chBoxDelete));

            //for (int x = 0; x <= chBoxAdd.CheckedItems.Count - 1; x++)
            //{
            //    SelectedUsersForAdd.Add((User)chBoxAdd.CheckedItems[x]);
            //}
            //if (SelectedUsersForAdd.Count != 0)
            //{
            //    Chat chatForAdd = new Chat()
            //    {
            //        Id = ChatOnForm.Id,
            //        Members = SelectedUsersForAdd
            //    };
            //    Client.AddMembers(chatForAdd);
            //}

            //for (int x = 0; x <= chBoxDelete.CheckedItems.Count - 1; x++)
            //{
            //    SelectedUsersForDelete.Add((User)chBoxDelete.CheckedItems[x]);
            //}
            //if (SelectedUsersForDelete.Count != 0)
            //{
            //    Chat chatForDelete = new Chat()
            //    {
            //        Id = ChatOnForm.Id,
            //        Members = SelectedUsersForDelete
            //    };
            //    Client.DeleteMembers(chatForDelete);
            //}
            Close();
            MainForm MF = new MainForm();
            MF.Show();
        }

        private void backToMain_Click(object sender, EventArgs e)
        {
            Close();
            MainForm MF = new MainForm();
            MF.Show();
        }
        public Chat AddOrRemoveMembers(CheckedListBox chBox)
        {
            List<User> users = new List<User>();
            for (int x = 0; x <= chBox.CheckedItems.Count - 1; x++)
            {
                users.Add((User)chBox.CheckedItems[x]);
            }
            if (users.Count != 0)
            {
                Chat chat = new Chat()
                {
                    Id = ChatOnForm.Id,
                    Members = users
                };
                return chat;
            }
            else
                return new Chat();
        }
    }
}
