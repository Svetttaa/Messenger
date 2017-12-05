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
using System.Threading;
using System.IO;

namespace Mes.Client
{
    public partial class ChatForm : Form
    {
        const int LabelSizeY = 50;
        byte[] Attach = null;
        string NameOfFile="";
        Chat ChatOnForm;
        List<Model.Message> Messages = new List<Model.Message>();

        public ChatForm(Chat chat)
        {
            InitializeComponent();
            ChatOnForm = chat;
            lblName.Text = ChatOnForm.Name;
            if (((List<Model.Message>)Client.GetAmountOfMessages(ChatOnForm.Id, 0, 20)).Count != 0)
            {
                Messages = (List<Model.Message>)Client.GetAmountOfMessages(ChatOnForm.Id, 0, 20);
                Messages.Reverse();
                if (Messages.Any())
                {
                    foreach (var m in Messages)
                    {
                        AddMessageToForm(m);
                        NameOfFile = "";
                    }
                }
            }

            else lblChat.Visible = true;
            backgroundUpdater.RunWorkerAsync();
        }

        private void backToMain_Click(object sender, EventArgs e)
        {
            Close();
            MainForm MF = new MainForm();
            MF.Show();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMessage.Text) || Attach != null)
            {
                Model.Message message = new Model.Message()
                {
                    UserId = Properties.Settings.Default.CurrentUser.Id,
                    ChatId = ChatOnForm.Id,
                    Text = txtMessage.Text,
                    AttachFile = Attach,
                    FileName = NameOfFile
                };

                message = (Model.Message)Client.Send(message);
                AddMessageToForm(message);
                Messages.Add(message);
                NameOfFile = "";
            }
            txtMessage.Text = null;
            Client.RemoveRow(tableLayoutPanel1, 1);
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string AttachFile = openFileDialog1.FileName;
                if (!string.IsNullOrWhiteSpace(AttachFile))
                {
                    Attach = Client.FromFileToBytes(AttachFile);
                    NameOfFile = openFileDialog1.SafeFileName;
                    var labelFile = Client.CreateLabel(NameOfFile, LabelSizeY, chatTable.Width);
                    labelFile.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                    labelFile.Margin = new Padding(0, 0, 0, 20);
                    tableLayoutPanel1.Controls.Add(labelFile, 0, tableLayoutPanel1.RowCount - 1);
                }
            }
        }

        private void backgroundUpdater_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(3000);

            List<Model.Message> messages = (List<Model.Message>)Client.GetAmountOfMessages(ChatOnForm.Id, 0, 20);

            List<Model.Message> messagesFromAnotherUsers = new List<Model.Message>();
            if (Properties.Settings.Default.CurrentUser == null)
            {
                e.Result = "end process";
            }
            else
            {
                foreach (var m in messages.Where(x => x.UserId != Properties.Settings.Default.CurrentUser.Id))
                {
                    if (!Messages.Select(x => x.Id).Contains(m.Id))
                    {
                        messagesFromAnotherUsers.Add(m);
                        Messages.Add(m);
                    }

                }
                e.Result = messagesFromAnotherUsers;
            }

        }

        private void backgroundUpdater_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != "end process")
            {
                foreach (var m in (List<Model.Message>)e.Result)
                {
                    AddMessageToForm(m);
                    NameOfFile = "";
                }

                backgroundUpdater.RunWorkerAsync();
            }

        }
        private void AddMessageToForm(Model.Message message)
        {
            var labelText = Client.CreateLabel(message.Text, LabelSizeY, chatTable.Width);
            labelText.ContextMenuStrip = contextMenuStrip1;
            labelText.BackColor = Color.White;
            labelText.Margin = new Padding(0, 0, 0, 20);
            labelText.Name = message.Id.ToString();

            var labelDate = Client.CreateLabel(message.Date.ToString(), LabelSizeY, chatTable.Width);
            labelDate.BackColor = Color.White;
            labelDate.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            labelDate.Margin = new Padding(0, 0, 0, 20);

            var control = new NameUserControl(ChatOnForm.Members.First(x => x.Id == message.UserId));

            chatTable.RowCount++;
            chatTable.RowStyles.Add(new RowStyle(SizeType.AutoSize, LabelSizeY));
            chatTable.Controls.Add(control, 0, chatTable.RowCount - 1);
            chatTable.Controls.Add(labelText, 1, chatTable.RowCount - 1);
            if (message.FileName != "")
            {
                var controlAttach = new AttachFile(message.FileName);
                controlAttach.Name = message.Id.ToString();
                controlAttach.Cursor = Cursors.Hand;
                controlAttach.Click += new EventHandler(SaveAttachOnClick);
                chatTable.Controls.Add(controlAttach, 2, chatTable.RowCount - 1);
            }
            chatTable.Controls.Add(labelDate, 3, chatTable.RowCount - 1);
            chatTable.RowCount++;
        }

        private void SaveAttachOnClick(object sender, EventArgs e)
        {
            Guid id = Guid.Parse(((Control)sender).Name);
            Model.Message message = Messages.First(x => x.Id == id);
            //byte[] attach = .AttachFile;
            saveFileDialog1.FileName = message.FileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                File.WriteAllBytes(saveFileDialog1.FileName, message.AttachFile);
            }
        }

        private void pbEditChat_Click(object sender, EventArgs e)
        {
            Close();
            EditChat EC = new EditChat(ChatOnForm);
            EC.Show();
        }

        private void pbInfo_Click(object sender, EventArgs e)
        {

        }

        private void chatTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DeleteMessage_Click(object sender, EventArgs e)
        {
            ToolStripItem chatItem = sender as ToolStripItem;
            if (chatItem != null)
            {
                ContextMenuStrip owner = chatItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    Control sourceControl = owner.SourceControl;
                    Guid idMessage = Guid.Parse(sourceControl.Name);
                    int rowIndex = chatTable.GetRow(sourceControl);
                    Client.DeleteMessage(idMessage);
                    Client.RemoveRow(chatTable, rowIndex);
                }
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            Close();
            SearchMessages SM = new SearchMessages(ChatOnForm);
            SM.Show();
        }
    }
}
