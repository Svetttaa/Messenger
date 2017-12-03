namespace Mes.Client
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblName = new System.Windows.Forms.Label();
            this.lblChats = new System.Windows.Forms.Label();
            this.ChatsTable = new System.Windows.Forms.TableLayoutPanel();
            this.searchUser = new System.Windows.Forms.PictureBox();
            this.editInfo = new System.Windows.Forms.PictureBox();
            this.backToStart = new System.Windows.Forms.PictureBox();
            this.picBoxAva = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.удалитьЧатToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.searchUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backToStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAva)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblName.Location = new System.Drawing.Point(116, 11);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(60, 28);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblChats
            // 
            this.lblChats.AutoSize = true;
            this.lblChats.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblChats.Location = new System.Drawing.Point(30, 114);
            this.lblChats.Margin = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.lblChats.Name = "lblChats";
            this.lblChats.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.lblChats.Size = new System.Drawing.Size(207, 21);
            this.lblChats.TabIndex = 6;
            this.lblChats.Text = "У Вас пока нет чатов...";
            // 
            // ChatsTable
            // 
            this.ChatsTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatsTable.AutoScroll = true;
            this.ChatsTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ChatsTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(234)))), ((int)(((byte)(249)))));
            this.ChatsTable.ColumnCount = 2;
            this.ChatsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ChatsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 312F));
            this.ChatsTable.Location = new System.Drawing.Point(9, 165);
            this.ChatsTable.Margin = new System.Windows.Forms.Padding(0);
            this.ChatsTable.Name = "ChatsTable";
            this.ChatsTable.RowCount = 1;
            this.ChatsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ChatsTable.Size = new System.Drawing.Size(372, 320);
            this.ChatsTable.TabIndex = 7;
            this.ChatsTable.Visible = false;
            // 
            // searchUser
            // 
            this.searchUser.BackgroundImage = global::Mes.Client.Properties.Resources.find_users;
            this.searchUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.searchUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.searchUser.Location = new System.Drawing.Point(279, 8);
            this.searchUser.Name = "searchUser";
            this.searchUser.Size = new System.Drawing.Size(33, 33);
            this.searchUser.TabIndex = 4;
            this.searchUser.TabStop = false;
            this.searchUser.Click += new System.EventHandler(this.searchUser_Click);
            // 
            // editInfo
            // 
            this.editInfo.BackgroundImage = global::Mes.Client.Properties.Resources.pencil;
            this.editInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.editInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.editInfo.Location = new System.Drawing.Point(226, 8);
            this.editInfo.Name = "editInfo";
            this.editInfo.Size = new System.Drawing.Size(33, 33);
            this.editInfo.TabIndex = 3;
            this.editInfo.TabStop = false;
            this.editInfo.Click += new System.EventHandler(this.editInfo_Click);
            // 
            // backToStart
            // 
            this.backToStart.BackgroundImage = global::Mes.Client.Properties.Resources.end;
            this.backToStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.backToStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backToStart.Location = new System.Drawing.Point(332, 8);
            this.backToStart.Name = "backToStart";
            this.backToStart.Size = new System.Drawing.Size(33, 33);
            this.backToStart.TabIndex = 2;
            this.backToStart.TabStop = false;
            this.backToStart.Click += new System.EventHandler(this.backToStart_Click);
            // 
            // picBoxAva
            // 
            this.picBoxAva.BackgroundImage = global::Mes.Client.Properties.Resources.whale;
            this.picBoxAva.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBoxAva.InitialImage = global::Mes.Client.Properties.Resources.whaleIcon;
            this.picBoxAva.Location = new System.Drawing.Point(13, 11);
            this.picBoxAva.Name = "picBoxAva";
            this.picBoxAva.Size = new System.Drawing.Size(80, 68);
            this.picBoxAva.TabIndex = 0;
            this.picBoxAva.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.удалитьЧатToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(140, 26);
            // 
            // удалитьЧатToolStripMenuItem
            // 
            this.удалитьЧатToolStripMenuItem.Name = "удалитьЧатToolStripMenuItem";
            this.удалитьЧатToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.удалитьЧатToolStripMenuItem.Text = "Удалить чат";
            this.удалитьЧатToolStripMenuItem.Click += new System.EventHandler(this.удалитьЧатToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(234)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(377, 481);
            this.Controls.Add(this.ChatsTable);
            this.Controls.Add(this.lblChats);
            this.Controls.Add(this.searchUser);
            this.Controls.Add(this.editInfo);
            this.Controls.Add(this.backToStart);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.picBoxAva);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.searchUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backToStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAva)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxAva;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.PictureBox backToStart;
        private System.Windows.Forms.PictureBox editInfo;
        private System.Windows.Forms.PictureBox searchUser;
        private System.Windows.Forms.Label lblChats;
        private System.Windows.Forms.TableLayoutPanel ChatsTable;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem удалитьЧатToolStripMenuItem;
    }
}