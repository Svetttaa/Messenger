namespace Mes.Client
{
    partial class AttachFile
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
            this.lblFileName = new System.Windows.Forms.Label();
            this.pbAttach = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbAttach)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFileName.Location = new System.Drawing.Point(0, 36);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(27, 17);
            this.lblFileName.TabIndex = 3;
            this.lblFileName.Text = "File";
            // 
            // pbAttach
            // 
            this.pbAttach.Image = global::Mes.Client.Properties.Resources.attach;
            this.pbAttach.Location = new System.Drawing.Point(3, 3);
            this.pbAttach.Name = "pbAttach";
            this.pbAttach.Size = new System.Drawing.Size(24, 30);
            this.pbAttach.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAttach.TabIndex = 2;
            this.pbAttach.TabStop = false;
            // 
            // AttachFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.pbAttach);
            this.Name = "AttachFile";
            this.Size = new System.Drawing.Size(47, 59);
            ((System.ComponentModel.ISupportInitialize)(this.pbAttach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.PictureBox pbAttach;
    }
}
