namespace notification_timer
{
    partial class NotificationForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.lstDoneJobs = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "次のジョブがタイムアウトになりました:";
            // 
            // lstDoneJobs
            // 
            this.lstDoneJobs.FormattingEnabled = true;
            this.lstDoneJobs.ItemHeight = 12;
            this.lstDoneJobs.Location = new System.Drawing.Point(12, 46);
            this.lstDoneJobs.Name = "lstDoneJobs";
            this.lstDoneJobs.Size = new System.Drawing.Size(469, 328);
            this.lstDoneJobs.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(329, 394);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(119, 32);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "確認";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // NotificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 438);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lstDoneJobs);
            this.Controls.Add(this.label1);
            this.Name = "NotificationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NotificationForm";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.NotificationForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstDoneJobs;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Timer timer1;
    }
}