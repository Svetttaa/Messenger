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
        public SearchUsersForm(IEnumerable<Chat> chats)
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<User> users = new List<User>();
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
                        var label = Client.CreateLabel(u.Name, LabelSizeY, tblUsers.Width, label_Click);
                        label.Name = u.Id.ToString();

                        tblUsers.RowCount++;
                        tblUsers.RowStyles.Add(new RowStyle(SizeType.Absolute, LabelSizeY));
                        tblUsers.Controls.Add(label, 1, tblUsers.RowCount - 1);

                        PictureBox avatarPB = new PictureBox()
                        {
                            BackgroundImage = Properties.Resources.add_user,
                            BackgroundImageLayout = ImageLayout.Zoom,
                            Size = new Size(LabelSizeY, LabelSizeY)

                        };
                        if (u.Ava != null && u.Ava.Any())
                            avatarPB.BackgroundImage = Client.FromBytesToBitmap(u.Ava);
                        tblUsers.Controls.Add(avatarPB, 0, tblUsers.RowCount - 1);
                    }

                    tblUsers.RowCount++;
                }
                else
                {
                    MessageBox.Show("Попробуйте ввести другое имя в поисковую строку");
                }

            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
