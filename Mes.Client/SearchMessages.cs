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
using System.IO;

namespace Mes.Client
{
    public partial class SearchMessages : Form
    {
        int LabelSizeY = 50;
        Chat ChatOnForm;
        List<Model.Message> Messages = new List<Model.Message>();
        public SearchMessages(Chat chat)
        {
            InitializeComponent();
            ChatOnForm = chat;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            messagesTable.Controls.Clear();
            messagesTable.RowCount = 1;
            Messages.Clear();
            if (!String.IsNullOrWhiteSpace(txtSearch.Text))
            {
                Messages = (List<Model.Message>)Client.SearchMessages(new Model.Message() { Text = txtSearch.Text, ChatId = ChatOnForm.Id });
                if (Messages.Any())
                {
                    lblMessages.Visible = false;
                    messagesTable.RowStyles[0].Height = 20;
                    foreach (var m in Messages)
                    {

                        AddMessageToForm(m);
                    }
                }

            }
        }
        private void AddMessageToForm(Model.Message message)
        {
            var labelText = Client.CreateLabel(message.Text, LabelSizeY, messagesTable.Width);
            labelText.ContextMenuStrip = contextMenuStrip1;
            labelText.BackColor = Color.White;
            labelText.Margin = new Padding(0, 0, 0, 20);
            labelText.Name = message.Id.ToString();

            var labelDate = Client.CreateLabel(message.Date.ToString(), LabelSizeY, messagesTable.Width);
            labelDate.BackColor = Color.White;
            labelDate.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            labelDate.Margin = new Padding(0, 0, 0, 20);

            var control = new NameUserControl(ChatOnForm.Members.First(x => x.Id == message.UserId));

            messagesTable.RowCount++;
            messagesTable.RowStyles.Add(new RowStyle(SizeType.AutoSize, LabelSizeY));
            messagesTable.Controls.Add(control, 0, messagesTable.RowCount - 1);
            messagesTable.Controls.Add(labelText, 1, messagesTable.RowCount - 1);
            if (message.FileName != "")
            {
                var controlAttach = new AttachFile(message.FileName);
                controlAttach.Name = message.Id.ToString();
                controlAttach.Cursor = Cursors.Hand;
                controlAttach.Click += new EventHandler(SaveAttachOnClick);
                messagesTable.Controls.Add(controlAttach, 2, messagesTable.RowCount - 1);
            }
            messagesTable.Controls.Add(labelDate, 3, messagesTable.RowCount - 1);
            messagesTable.RowCount++;
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

        private void SaveAttach_Click(object sender, EventArgs e)
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

        private void backToChat_Click(object sender, EventArgs e)
        {
            Close();
            ChatForm CF= new ChatForm(ChatOnForm);
            CF.Show();

        }
    }
}
