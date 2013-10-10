namespace notification_timer
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtJobName = new System.Windows.Forms.TextBox();
            this.txtTimeOut = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnQuit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.radRelative = new System.Windows.Forms.RadioButton();
            this.radAbsolute = new System.Windows.Forms.RadioButton();
            this.txtHour = new System.Windows.Forms.TextBox();
            this.txtMinute = new System.Windows.Forms.TextBox();
            this.txtSecond = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkRepeat = new System.Windows.Forms.CheckBox();
            this.chkSound = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtSoundFile = new System.Windows.Forms.TextBox();
            this.btnSelectSound = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lstTemplate = new System.Windows.Forms.ListBox();
            this.btnTemplate = new System.Windows.Forms.Button();
            this.btnTimeSet = new System.Windows.Forms.Button();
            this.lvJobList = new notification_timer.ListViewDB();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ジョブ名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "タイムアウト:";
            // 
            // txtJobName
            // 
            this.txtJobName.Location = new System.Drawing.Point(80, 16);
            this.txtJobName.Name = "txtJobName";
            this.txtJobName.Size = new System.Drawing.Size(315, 19);
            this.txtJobName.TabIndex = 1;
            this.txtJobName.Enter += new System.EventHandler(this.txtJobName_Enter);
            // 
            // txtTimeOut
            // 
            this.txtTimeOut.Location = new System.Drawing.Point(191, 50);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Size = new System.Drawing.Size(100, 19);
            this.txtTimeOut.TabIndex = 4;
            this.txtTimeOut.Text = "600";
            this.txtTimeOut.Enter += new System.EventHandler(this.txtJobName_Enter);
            this.txtTimeOut.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTimeOut_KeyDown);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(17, 116);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "追加(&A)";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "ジョブリスト:";
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.Location = new System.Drawing.Point(558, 493);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(100, 30);
            this.btnQuit.TabIndex = 16;
            this.btnQuit.Text = "終了";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(297, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "秒";
            // 
            // radRelative
            // 
            this.radRelative.AutoSize = true;
            this.radRelative.Checked = true;
            this.radRelative.Location = new System.Drawing.Point(16, 51);
            this.radRelative.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.radRelative.Name = "radRelative";
            this.radRelative.Size = new System.Drawing.Size(101, 16);
            this.radRelative.TabIndex = 2;
            this.radRelative.TabStop = true;
            this.radRelative.Text = "残り時間で指定";
            this.radRelative.UseVisualStyleBackColor = true;
            this.radRelative.CheckedChanged += new System.EventHandler(this.radRelative_CheckedChanged);
            // 
            // radAbsolute
            // 
            this.radAbsolute.AutoSize = true;
            this.radAbsolute.Location = new System.Drawing.Point(16, 87);
            this.radAbsolute.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.radAbsolute.Name = "radAbsolute";
            this.radAbsolute.Size = new System.Drawing.Size(80, 16);
            this.radAbsolute.TabIndex = 6;
            this.radAbsolute.Text = "時刻を指定";
            this.radAbsolute.UseVisualStyleBackColor = true;
            this.radAbsolute.CheckedChanged += new System.EventHandler(this.radRelative_CheckedChanged);
            // 
            // txtHour
            // 
            this.txtHour.Enabled = false;
            this.txtHour.Location = new System.Drawing.Point(127, 86);
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(40, 19);
            this.txtHour.TabIndex = 7;
            this.txtHour.Enter += new System.EventHandler(this.txtJobName_Enter);
            this.txtHour.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTimeOut_KeyDown);
            // 
            // txtMinute
            // 
            this.txtMinute.Enabled = false;
            this.txtMinute.Location = new System.Drawing.Point(196, 86);
            this.txtMinute.Name = "txtMinute";
            this.txtMinute.Size = new System.Drawing.Size(40, 19);
            this.txtMinute.TabIndex = 9;
            this.txtMinute.Enter += new System.EventHandler(this.txtJobName_Enter);
            this.txtMinute.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTimeOut_KeyDown);
            // 
            // txtSecond
            // 
            this.txtSecond.Enabled = false;
            this.txtSecond.Location = new System.Drawing.Point(265, 86);
            this.txtSecond.Name = "txtSecond";
            this.txtSecond.Size = new System.Drawing.Size(40, 19);
            this.txtSecond.TabIndex = 11;
            this.txtSecond.Enter += new System.EventHandler(this.txtJobName_Enter);
            this.txtSecond.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTimeOut_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(173, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "時";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Enabled = false;
            this.label6.Location = new System.Drawing.Point(242, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "分";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(311, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "秒";
            // 
            // chkRepeat
            // 
            this.chkRepeat.AutoSize = true;
            this.chkRepeat.Location = new System.Drawing.Point(127, 124);
            this.chkRepeat.Name = "chkRepeat";
            this.chkRepeat.Size = new System.Drawing.Size(155, 16);
            this.chkRepeat.TabIndex = 17;
            this.chkRepeat.Text = "完了時にジョブを再投入(&R)";
            this.chkRepeat.UseVisualStyleBackColor = true;
            // 
            // chkSound
            // 
            this.chkSound.AutoSize = true;
            this.chkSound.Location = new System.Drawing.Point(14, 168);
            this.chkSound.Name = "chkSound";
            this.chkSound.Size = new System.Drawing.Size(120, 16);
            this.chkSound.TabIndex = 18;
            this.chkSound.Text = "完了時に音を鳴らす";
            this.chkSound.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtSoundFile
            // 
            this.txtSoundFile.Location = new System.Drawing.Point(140, 166);
            this.txtSoundFile.Name = "txtSoundFile";
            this.txtSoundFile.Size = new System.Drawing.Size(377, 19);
            this.txtSoundFile.TabIndex = 19;
            // 
            // btnSelectSound
            // 
            this.btnSelectSound.Location = new System.Drawing.Point(523, 164);
            this.btnSelectSound.Name = "btnSelectSound";
            this.btnSelectSound.Size = new System.Drawing.Size(66, 23);
            this.btnSelectSound.TabIndex = 20;
            this.btnSelectSound.Text = "参照...";
            this.btnSelectSound.UseVisualStyleBackColor = true;
            this.btnSelectSound.Click += new System.EventHandler(this.btnSelectSound_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Location = new System.Drawing.Point(14, 493);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(99, 32);
            this.btnRemove.TabIndex = 21;
            this.btnRemove.Text = "削除";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lstTemplate
            // 
            this.lstTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstTemplate.FormattingEnabled = true;
            this.lstTemplate.ItemHeight = 12;
            this.lstTemplate.Location = new System.Drawing.Point(509, 12);
            this.lstTemplate.Name = "lstTemplate";
            this.lstTemplate.Size = new System.Drawing.Size(149, 136);
            this.lstTemplate.TabIndex = 22;
            this.lstTemplate.DoubleClick += new System.EventHandler(this.lstTemplate_DoubleClick);
            this.lstTemplate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstTemplate_KeyDown);
            // 
            // btnTemplate
            // 
            this.btnTemplate.Location = new System.Drawing.Point(403, 12);
            this.btnTemplate.Name = "btnTemplate";
            this.btnTemplate.Size = new System.Drawing.Size(100, 30);
            this.btnTemplate.TabIndex = 23;
            this.btnTemplate.Text = "テンプレ化 >>";
            this.btnTemplate.UseVisualStyleBackColor = true;
            this.btnTemplate.Click += new System.EventHandler(this.btnTemplate_Click);
            // 
            // btnTimeSet
            // 
            this.btnTimeSet.Location = new System.Drawing.Point(320, 48);
            this.btnTimeSet.Name = "btnTimeSet";
            this.btnTimeSet.Size = new System.Drawing.Size(26, 23);
            this.btnTimeSet.TabIndex = 24;
            this.btnTimeSet.Text = "...";
            this.btnTimeSet.UseVisualStyleBackColor = true;
            this.btnTimeSet.Click += new System.EventHandler(this.btnTimeSet_Click);
            // 
            // lvJobList
            // 
            this.lvJobList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvJobList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lvJobList.FullRowSelect = true;
            this.lvJobList.HideSelection = false;
            this.lvJobList.Location = new System.Drawing.Point(12, 217);
            this.lvJobList.Name = "lvJobList";
            this.lvJobList.Size = new System.Drawing.Size(646, 270);
            this.lvJobList.TabIndex = 25;
            this.lvJobList.UseCompatibleStateImageBehavior = false;
            this.lvJobList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 30;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ジョブ名";
            this.columnHeader2.Width = 250;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "期限";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "残り";
            this.columnHeader4.Width = 150;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "再投入";
            this.columnHeader5.Width = 50;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 535);
            this.Controls.Add(this.lvJobList);
            this.Controls.Add(this.btnTimeSet);
            this.Controls.Add(this.btnTemplate);
            this.Controls.Add(this.lstTemplate);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnSelectSound);
            this.Controls.Add(this.txtSoundFile);
            this.Controls.Add(this.chkSound);
            this.Controls.Add(this.chkRepeat);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSecond);
            this.Controls.Add(this.txtMinute);
            this.Controls.Add(this.txtHour);
            this.Controls.Add(this.radAbsolute);
            this.Controls.Add(this.radRelative);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtTimeOut);
            this.Controls.Add(this.txtJobName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "通知タイマー";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtJobName;
        private System.Windows.Forms.TextBox txtTimeOut;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radRelative;
        private System.Windows.Forms.RadioButton radAbsolute;
        private System.Windows.Forms.TextBox txtHour;
        private System.Windows.Forms.TextBox txtMinute;
        private System.Windows.Forms.TextBox txtSecond;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkRepeat;
        private System.Windows.Forms.CheckBox chkSound;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtSoundFile;
        private System.Windows.Forms.Button btnSelectSound;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ListBox lstTemplate;
        private System.Windows.Forms.Button btnTemplate;
        private System.Windows.Forms.Button btnTimeSet;
        private ListViewDB lvJobList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}

