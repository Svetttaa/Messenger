namespace Mes.Client
{
    partial class SearchUsersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchUsersForm));
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.tblUsers = new System.Windows.Forms.TableLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.backToStart = new System.Windows.Forms.PictureBox();
            this.pbBackToMain = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.backToStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackToMain)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSearch.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblSearch.Location = new System.Drawing.Point(61, 16);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(201, 25);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Поиск пользователей";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSearch.Location = new System.Drawing.Point(17, 78);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(261, 29);
            this.txtSearch.TabIndex = 1;
            // 
            // tblUsers
            // 
            this.tblUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblUsers.AutoScroll = true;
            this.tblUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(234)))), ((int)(((byte)(249)))));
            this.tblUsers.ColumnCount = 3;
            this.tblUsers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.94444F));
            this.tblUsers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.98338F));
            this.tblUsers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.13297F));
            this.tblUsers.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tblUsers.Location = new System.Drawing.Point(12, 113);
            this.tblUsers.Name = "tblUsers";
            this.tblUsers.RowCount = 1;
            this.tblUsers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 368F));
            this.tblUsers.Size = new System.Drawing.Size(366, 368);
            this.tblUsers.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.White;
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(275, 78);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(44, 29);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // backToStart
            // 
            this.backToStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.backToStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backToStart.Image = global::Mes.Client.Properties.Resources.back_left_arrow;
            this.backToStart.Location = new System.Drawing.Point(17, 12);
            this.backToStart.Name = "backToStart";
            this.backToStart.Size = new System.Drawing.Size(29, 29);
            this.backToStart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.backToStart.TabIndex = 3;
            this.backToStart.TabStop = false;
            this.backToStart.Click += new System.EventHandler(this.backToStart_Click);
            // 
            // pbBackToMain
            // 
            this.pbBackToMain.BackgroundImage = global::Mes.Client.Properties.Resources.end;
            this.pbBackToMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbBackToMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbBackToMain.Location = new System.Drawing.Point(332, 12);
            this.pbBackToMain.Name = "pbBackToMain";
            this.pbBackToMain.Size = new System.Drawing.Size(33, 33);
            this.pbBackToMain.TabIndex = 4;
            this.pbBackToMain.TabStop = false;
            this.pbBackToMain.Click += new System.EventHandler(this.pbBackToMain_Click);
            // 
            // SearchUsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(234)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(377, 481);
            this.Controls.Add(this.pbBackToMain);
            this.Controls.Add(this.backToStart);
            this.Controls.Add(this.tblUsers);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchUsersForm";
            this.Text = "SearchUsersForm";
            ((System.ComponentModel.ISupportInitialize)(this.backToStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackToMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TableLayoutPanel tblUsers;
        private System.Windows.Forms.PictureBox backToStart;
        private System.Windows.Forms.PictureBox pbBackToMain;
    }
}