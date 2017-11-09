namespace Mes.Client
{
    partial class StartForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backToStart = new System.Windows.Forms.PictureBox();
            this.startControl = new Mes.Client.StartControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backToStart)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(104, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(168, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // backToStart
            // 
            this.backToStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.backToStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backToStart.Image = ((System.Drawing.Image)(resources.GetObject("backToStart.Image")));
            this.backToStart.Location = new System.Drawing.Point(12, 12);
            this.backToStart.Name = "backToStart";
            this.backToStart.Size = new System.Drawing.Size(29, 29);
            this.backToStart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.backToStart.TabIndex = 2;
            this.backToStart.TabStop = false;
            this.backToStart.Visible = false;
            this.backToStart.Click += new System.EventHandler(this.backToStart_Click);
            // 
            // startControl
            // 
            this.startControl.Location = new System.Drawing.Point(22, 200);
            this.startControl.Name = "startControl";
            this.startControl.Size = new System.Drawing.Size(333, 269);
            this.startControl.TabIndex = 0;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(234)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(377, 481);
            this.Controls.Add(this.backToStart);
            this.Controls.Add(this.startControl);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StartForm";
            this.Text = "Messenger";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backToStart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private StartControl startControl;
        private System.Windows.Forms.PictureBox backToStart;
    }
}

