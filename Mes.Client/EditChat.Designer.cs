namespace Mes.Client
{
    partial class EditChat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditChat));
            this.backToMain = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txbName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDeleteChat = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.chBoxAdd = new System.Windows.Forms.CheckedListBox();
            this.chBoxDelete = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.backToMain)).BeginInit();
            this.SuspendLayout();
            // 
            // backToMain
            // 
            this.backToMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.backToMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backToMain.Image = global::Mes.Client.Properties.Resources.back_left_arrow;
            this.backToMain.Location = new System.Drawing.Point(9, 8);
            this.backToMain.Name = "backToMain";
            this.backToMain.Size = new System.Drawing.Size(29, 29);
            this.backToMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.backToMain.TabIndex = 6;
            this.backToMain.TabStop = false;
            this.backToMain.Click += new System.EventHandler(this.backToMain_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblName.Location = new System.Drawing.Point(59, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(147, 28);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Редактировать";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(19, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "Новое имя чата";
            // 
            // txbName
            // 
            this.txbName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txbName.Location = new System.Drawing.Point(150, 78);
            this.txbName.Name = "txbName";
            this.txbName.Size = new System.Drawing.Size(180, 29);
            this.txbName.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(234, 395);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(115, 40);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDeleteChat
            // 
            this.btnDeleteChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnDeleteChat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteChat.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDeleteChat.FlatAppearance.BorderSize = 0;
            this.btnDeleteChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteChat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDeleteChat.Location = new System.Drawing.Point(97, 395);
            this.btnDeleteChat.Name = "btnDeleteChat";
            this.btnDeleteChat.Size = new System.Drawing.Size(115, 40);
            this.btnDeleteChat.TabIndex = 10;
            this.btnDeleteChat.Text = "Удалить чат";
            this.btnDeleteChat.UseVisualStyleBackColor = false;
            this.btnDeleteChat.Click += new System.EventHandler(this.btnDeleteChat_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 21);
            this.label2.TabIndex = 12;
            this.label2.Text = "Добавить:";
            // 
            // chBoxAdd
            // 
            this.chBoxAdd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chBoxAdd.FormattingEnabled = true;
            this.chBoxAdd.Location = new System.Drawing.Point(12, 190);
            this.chBoxAdd.Name = "chBoxAdd";
            this.chBoxAdd.Size = new System.Drawing.Size(130, 148);
            this.chBoxAdd.TabIndex = 13;
            // 
            // chBoxDelete
            // 
            this.chBoxDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chBoxDelete.FormattingEnabled = true;
            this.chBoxDelete.Location = new System.Drawing.Point(219, 190);
            this.chBoxDelete.Name = "chBoxDelete";
            this.chBoxDelete.Size = new System.Drawing.Size(130, 148);
            this.chBoxDelete.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(215, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 21);
            this.label3.TabIndex = 15;
            this.label3.Text = "Удалить:";
            // 
            // EditChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(234)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(382, 481);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chBoxDelete);
            this.Controls.Add(this.chBoxAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDeleteChat);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txbName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backToMain);
            this.Controls.Add(this.lblName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditChat";
            this.Text = "EditChat";
            ((System.ComponentModel.ISupportInitialize)(this.backToMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox backToMain;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDeleteChat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox chBoxAdd;
        private System.Windows.Forms.CheckedListBox chBoxDelete;
        private System.Windows.Forms.Label label3;
    }
}