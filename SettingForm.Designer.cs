namespace notification_timer
{
    partial class SettingForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkSound = new System.Windows.Forms.CheckBox();
            this.lblSoundFile = new System.Windows.Forms.Label();
            this.btnSelectSound = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblListPath = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(376, 146);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "キャンセル(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(270, 146);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 30);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // chkSound
            // 
            this.chkSound.AutoSize = true;
            this.chkSound.Location = new System.Drawing.Point(12, 12);
            this.chkSound.Name = "chkSound";
            this.chkSound.Size = new System.Drawing.Size(145, 16);
            this.chkSound.TabIndex = 2;
            this.chkSound.Text = "ジョブ完了時に音を鳴らす";
            this.chkSound.UseVisualStyleBackColor = true;
            this.chkSound.CheckedChanged += new System.EventHandler(this.chkSound_CheckedChanged);
            // 
            // lblSoundFile
            // 
            this.lblSoundFile.AutoSize = true;
            this.lblSoundFile.Enabled = false;
            this.lblSoundFile.Location = new System.Drawing.Point(105, 41);
            this.lblSoundFile.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblSoundFile.Name = "lblSoundFile";
            this.lblSoundFile.Size = new System.Drawing.Size(35, 12);
            this.lblSoundFile.TabIndex = 3;
            this.lblSoundFile.Text = "label1";
            // 
            // btnSelectSound
            // 
            this.btnSelectSound.Enabled = false;
            this.btnSelectSound.Location = new System.Drawing.Point(24, 36);
            this.btnSelectSound.Margin = new System.Windows.Forms.Padding(15, 5, 3, 3);
            this.btnSelectSound.Name = "btnSelectSound";
            this.btnSelectSound.Size = new System.Drawing.Size(75, 23);
            this.btnSelectSound.TabIndex = 4;
            this.btnSelectSound.Text = "参照(&R)";
            this.btnSelectSound.UseVisualStyleBackColor = true;
            this.btnSelectSound.Click += new System.EventHandler(this.btnSelectSound_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 77);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "ジョブリストの保存ディレクトリ:";
            // 
            // lblListPath
            // 
            this.lblListPath.AutoSize = true;
            this.lblListPath.Location = new System.Drawing.Point(105, 104);
            this.lblListPath.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblListPath.Name = "lblListPath";
            this.lblListPath.Size = new System.Drawing.Size(35, 12);
            this.lblListPath.TabIndex = 6;
            this.lblListPath.Text = "label2";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 99);
            this.button1.Margin = new System.Windows.Forms.Padding(15, 10, 3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "参照(&S)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Wave ファイル(*.wav)|*.wav|すべてのファイル|*.*";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 188);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblListPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelectSound);
            this.Controls.Add(this.lblSoundFile);
            this.Controls.Add(this.chkSound);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkSound;
        private System.Windows.Forms.Label lblSoundFile;
        private System.Windows.Forms.Button btnSelectSound;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblListPath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}