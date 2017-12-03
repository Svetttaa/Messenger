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
    public partial class ChatForm : Form
    {
        const int LabelSizeY = 50;
        byte[] File = null;
        string AttachFile;
        Chat ChatOnForm;
        List<Model.Message> Messages = new List<Model.Message>();

        public ChatForm(Chat chat)
        {
            InitializeComponent();
            ChatOnForm = chat;
            lblName.Text = ChatOnForm.Name;
            if(((List<Model.Message>)Client.GetAmountOfMessages(ChatOnForm.Id, 0, 20)).Count!=0)
            {
                Messages = (List<Model.Message>)Client.GetAmountOfMessages(ChatOnForm.Id, 0, 20);
                Messages.Reverse();
                if (Messages.Any())
                {
                    foreach (var m in Messages)
                    {

                        AddMessageToForm(m);
                    }
                }
            }
           
            else lblChat.Visible = true;
            
           // backgroundWorker1.RunWorkerAsync();
        }

        private void backToMain_Click(object sender, EventArgs e)
        {
            Close();
            MainForm MF = new MainForm();
            MF.Show();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // backgroundWorker1.RunWorkerAsync();
            if (!string.IsNullOrWhiteSpace(txtMessage.Text) || File != null)
            {
                Model.Message message=new Model.Message()
                {
                    UserId = Properties.Settings.Default.CurrentUser.Id,
                    ChatId = ChatOnForm.Id,
                    Text = txtMessage.Text,
                    AttachPath = AttachFile
                };
                if (File != null) message.Text += $" [прикреплен файл {AttachFile}]";
                message=(Model.Message)Client.Send(message);
                AddMessageToForm(message);
            }
            txtMessage.Text = null;
        }
//НАПИСАТЬ НОВЫЙ МЕТОД
        private void btnAddFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                AttachFile = openFileDialog1.FileName;
                if (!string.IsNullOrWhiteSpace(AttachFile))
                {
                    File = Client.FromFileToBytes(AttachFile);
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        private void AddMessageToForm(Model.Message message)
        {
            var labelText = Client.CreateLabel(message.Text, LabelSizeY, chatTable.Width);
            labelText.BackColor = Color.White;
            labelText.Margin = new Padding(0, 0, 0, 20);

            var labelDate = Client.CreateLabel(message.Date.ToString(), LabelSizeY, chatTable.Width);
            labelDate.BackColor = Color.White;
            labelDate.Margin = new Padding(0, 0, 0, 20);

            var control = new NameUserControl(ChatOnForm.Members.First(x => x.Id == message.UserId));

            chatTable.RowCount++;
            chatTable.RowStyles.Add(new RowStyle(SizeType.AutoSize, LabelSizeY));
            chatTable.Controls.Add(control, 0, chatTable.RowCount - 1);
            chatTable.Controls.Add(labelText, 1, chatTable.RowCount - 1);
            chatTable.Controls.Add(labelDate, 2, chatTable.RowCount - 1);
            chatTable.RowCount++;
        }

        private void pbEditChat_Click(object sender, EventArgs e)
        {

        }

        private void pbInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
