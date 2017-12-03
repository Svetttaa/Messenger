﻿namespace Mes.Client
{
    partial class ChatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.lblName = new System.Windows.Forms.Label();
            this.chatTable = new System.Windows.Forms.TableLayoutPanel();
            this.lblChat = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.pbEditChat = new System.Windows.Forms.PictureBox();
            this.backToMain = new System.Windows.Forms.PictureBox();
            this.pbInfo = new System.Windows.Forms.PictureBox();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pbEditChat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backToMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblName.Location = new System.Drawing.Point(62, 19);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(60, 28);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chatTable
            // 
            this.chatTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatTable.AutoScroll = true;
            this.chatTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chatTable.BackColor = System.Drawing.Color.White;
            this.chatTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.chatTable.ColumnCount = 3;
            this.chatTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.chatTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.chatTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.chatTable.Location = new System.Drawing.Point(-5, 89);
            this.chatTable.Name = "chatTable";
            this.chatTable.RowCount = 1;
            this.chatTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.chatTable.Size = new System.Drawing.Size(388, 322);
            this.chatTable.TabIndex = 8;
            // 
            // lblChat
            // 
            this.lblChat.AutoSize = true;
            this.lblChat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblChat.Location = new System.Drawing.Point(12, 65);
            this.lblChat.Name = "lblChat";
            this.lblChat.Size = new System.Drawing.Size(247, 21);
            this.lblChat.TabIndex = 9;
            this.lblChat.Text = "У Вас пока нет сообщений в чате";
            this.lblChat.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.AutoCompleteCustomSource.AddRange(new string[] {
            "Введите сообщение"});
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtMessage.Location = new System.Drawing.Point(3, 3);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtMessage.Size = new System.Drawing.Size(302, 22);
            this.txtMessage.TabIndex = 9;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.BackColor = System.Drawing.Color.White;
            this.btnSend.BackgroundImage = global::Mes.Client.Properties.Resources.send;
            this.btnSend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Location = new System.Drawing.Point(311, 3);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(34, 25);
            this.btnSend.TabIndex = 10;
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // pbEditChat
            // 
            this.pbEditChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbEditChat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbEditChat.Image = global::Mes.Client.Properties.Resources.pencil;
            this.pbEditChat.Location = new System.Drawing.Point(267, 12);
            this.pbEditChat.Name = "pbEditChat";
            this.pbEditChat.Size = new System.Drawing.Size(33, 33);
            this.pbEditChat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbEditChat.TabIndex = 5;
            this.pbEditChat.TabStop = false;
            this.pbEditChat.Click += new System.EventHandler(this.pbEditChat_Click);
            // 
            // backToMain
            // 
            this.backToMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.backToMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backToMain.Image = global::Mes.Client.Properties.Resources.back_left_arrow;
            this.backToMain.Location = new System.Drawing.Point(12, 18);
            this.backToMain.Name = "backToMain";
            this.backToMain.Size = new System.Drawing.Size(29, 29);
            this.backToMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.backToMain.TabIndex = 4;
            this.backToMain.TabStop = false;
            this.backToMain.Click += new System.EventHandler(this.backToMain_Click);
            // 
            // pbInfo
            // 
            this.pbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbInfo.Image = global::Mes.Client.Properties.Resources.info_icon;
            this.pbInfo.Location = new System.Drawing.Point(318, 12);
            this.pbInfo.Name = "pbInfo";
            this.pbInfo.Size = new System.Drawing.Size(38, 33);
            this.pbInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbInfo.TabIndex = 3;
            this.pbInfo.TabStop = false;
            this.pbInfo.Click += new System.EventHandler(this.pbInfo_Click);
            // 
            // btnAddFile
            // 
            this.btnAddFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFile.BackColor = System.Drawing.Color.White;
            this.btnAddFile.BackgroundImage = global::Mes.Client.Properties.Resources.add_new_file;
            this.btnAddFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFile.Location = new System.Drawing.Point(351, 3);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnAddFile.Size = new System.Drawing.Size(34, 25);
            this.btnAddFile.TabIndex = 11;
            this.btnAddFile.UseVisualStyleBackColor = false;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Controls.Add(this.btnAddFile, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtMessage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSend, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(-5, 417);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(388, 62);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(234)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(382, 481);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lblChat);
            this.Controls.Add(this.chatTable);
            this.Controls.Add(this.pbEditChat);
            this.Controls.Add(this.backToMain);
            this.Controls.Add(this.pbInfo);
            this.Controls.Add(this.lblName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChatForm";
            this.Text = "ChatForm";
            ((System.ComponentModel.ISupportInitialize)(this.pbEditChat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backToMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.PictureBox pbInfo;
        private System.Windows.Forms.PictureBox backToMain;
        private System.Windows.Forms.PictureBox pbEditChat;
        private System.Windows.Forms.TableLayoutPanel chatTable;
        private System.Windows.Forms.Label lblChat;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnAddFile;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}