using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace notification_timer
{
    public partial class SettingForm : Form
    {
        private Settings settings_var;
        public Settings settings
        {
            get
            {
                return settings_var;
            }
            set
            {
                settings_var = value;
                lblListPath.Text = settings_var.joblist_dir;
                lblSoundFile.Text = settings_var.sound_file;
                chkSound.Checked = settings_var.sound;
            }
        }

        private bool Validation()
        {
            if (chkSound.Checked)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                player.SoundLocation = settings_var.sound_file;
                try
                {
                    player.Load();
                }
                catch
                {
                    MessageBox.Show("サウンドファイルが見つからないか、無効な形式です。");
                    return false;
                }
                finally
                {
                    player.Dispose();
                }
            }
            if (!Directory.Exists(settings_var.joblist_dir))
            {
                if (DialogResult.Yes == MessageBox.Show(string.Format("ディレクトリ {0} は存在しません。作成しますか？", settings_var.joblist_dir), "存在しないディレクトリ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    try
                    {
                        Directory.CreateDirectory(settings_var.joblist_dir);
                    }
                    catch
                    {
                        MessageBox.Show(string.Format("ディレクトリ {0} の作成に失敗しました。", settings_var.joblist_dir), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.Cancel) return;
            if (!Validation())
            {
                e.Cancel = true;
            }
        }

        private void chkSound_CheckedChanged(object sender, EventArgs e)
        {
            btnSelectSound.Enabled = chkSound.Checked;
            lblSoundFile.Enabled = chkSound.Checked;
            settings_var.sound = chkSound.Checked;
        }

        private void btnSelectSound_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(settings_var.sound_file);
            openFileDialog1.FileName = System.IO.Path.GetFileName(settings_var.sound_file);
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                settings_var.sound_file = openFileDialog1.FileName;
                lblSoundFile.Text = openFileDialog1.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.SelectedPath == "")
                folderBrowserDialog1.SelectedPath = settings_var.joblist_dir;
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                settings_var.joblist_dir = folderBrowserDialog1.SelectedPath;
                lblListPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }

    public struct Settings
    {
        public bool sound;
        public string sound_file;
        public string joblist_dir;

        public void Save(string filename)
        {
            using (var sw = new StreamWriter(filename))
            {
                sw.WriteLine(sound);
                sw.WriteLine(sound_file);
                sw.WriteLine(joblist_dir);
            }
        }

        public static Settings Load(string filename)
        {
            Settings ret;
            using (var sr = new StreamReader(filename))
            {
                ret.sound = bool.Parse(sr.ReadLine());
                ret.sound_file = sr.ReadLine();
                ret.joblist_dir = sr.ReadLine();
            }
            return ret;
        }
    }
}
