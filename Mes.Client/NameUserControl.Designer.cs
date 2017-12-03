namespace Mes.Client
{
    partial class NameUserControl
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbUserAva = new System.Windows.Forms.PictureBox();
            this.lblUserName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserAva)).BeginInit();
            this.SuspendLayout();
            // 
            // pbUserAva
            // 
            this.pbUserAva.Location = new System.Drawing.Point(3, 0);
            this.pbUserAva.Name = "pbUserAva";
            this.pbUserAva.Size = new System.Drawing.Size(38, 38);
            this.pbUserAva.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbUserAva.TabIndex = 0;
            this.pbUserAva.TabStop = false;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblUserName.Location = new System.Drawing.Point(0, 42);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(41, 17);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "label1";
            // 
            // NameUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.pbUserAva);
            this.Name = "NameUserControl";
            this.Size = new System.Drawing.Size(54, 59);
            ((System.ComponentModel.ISupportInitialize)(this.pbUserAva)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbUserAva;
        private System.Windows.Forms.Label lblUserName;
    }
}
