using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace notification_timer
{
    public partial class MainForm : Form
    {
        List<TimerJob> jobs;
        List<TimerJob> jobs_done;
        List<TimerJob> jobs_template;
        NotificationForm notification_form;
        Control[] control_set_absolute;
        Control[] control_set_related;
        System.Media.SoundPlayer sound_done;
        const string settings_xml = "settings.xml";
        const string joblist_filename = "joblist.txt";
        Settings settings;

        public MainForm()
        {
            InitializeComponent();
            jobs = new List<TimerJob>();
            jobs_done = new List<TimerJob>();
            jobs_template = new List<TimerJob>();
            timer1.Enabled = true;
            control_set_related = new Control[] { label2, label4, txtTimeOut };
            control_set_absolute = new Control[] { txtHour, txtMinute, txtSecond, label5, label6, label7 };
            openFileDialog1.Filter = "Wave サウンド (*.wav)|*.wav|すべてのファイル (*.*)|*.*";
            try
            {
                LoadSettings();
            }
            // Ignore all exceptions
            catch { }
            ReloadSound();
            UpdateJobList();
            UpdateTemplateList();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool play_sound = false;
            if (jobs == null) return;
            DateTime now = DateTime.Now;
            foreach (var each in jobs)
            {
                if (each.timeout < now)
                {
                    // a job timed out
                    jobs_done.Add(each);
                    play_sound = true;
                }
            }
            jobs.RemoveAll((x) => jobs_done.Contains(x));
            if (jobs_done.Count > 0)
            {
                if (notification_form == null || notification_form.IsDisposed)
                {
                    notification_form = new NotificationForm(jobs, jobs_done);
                    notification_form.Show();
                    FlashWindow.Flash(notification_form, FlashWindow.FLASHW_ALL | FlashWindow.FLASHW_TIMERNOFG);
                }
                else
                {
                    notification_form.WindowState = FormWindowState.Normal;
                    notification_form.Show();
                }
            }
            if (settings.sound && play_sound)
            {
                try
                {
                    sound_done.Play();
                }
                catch (System.IO.FileNotFoundException)
                {
                    // TODO: エラー処理
                }
            }
            UpdateJobList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!CheckForm()) return;
            if (radRelative.Checked)
            {
                double seconds = double.Parse(txtTimeOut.Text);
                jobs.Add(new TimerJob(txtJobName.Text, seconds, chkRepeat.Checked));
            }
            else if (radAbsolute.Checked)
            {
                DateTime goal = DateTime.Parse(string.Format("{0}:{1}:{2}", txtHour.Text, txtMinute.Text, txtSecond.Text));
                jobs.Add(new TimerJob(txtJobName.Text, goal, chkRepeat.Checked));
            }
            UpdateJobList();
            txtJobName.Clear();
            txtJobName.Focus();
        }

        private bool CheckForm()
        {
            // check job name
            if (txtJobName.Text.Trim() == "")
            {
                MessageBox.Show("ジョブ名を入力してください");
                txtJobName.Clear();
                txtJobName.Focus();
                return false;
            }
            if (radRelative.Checked)
            {
                double seconds;
                if (!double.TryParse(txtTimeOut.Text, out seconds) || seconds < 0)
                {
                    MessageBox.Show("タイムアウト時間を正の数値で指定してください");
                    txtTimeOut.SelectAll();
                    txtTimeOut.Focus();
                    return false;
                }
            }
            else if (radAbsolute.Checked)
            {
                DateTime goal;
                if (!DateTime.TryParse(string.Format("{0}:{1}:{2}", txtHour.Text, txtMinute.Text, txtSecond.Text), out goal))
                {
                    MessageBox.Show("時刻を正しく指定してください");
                    txtHour.SelectAll();
                    txtHour.Focus();
                    return false;
                }
            }
            return true;
        }

        private void UpdateJobList()
        {
            if (jobs == null) return;
            lvJobList.BeginUpdate();
            DateTime now = DateTime.Now;
            if (jobs.Count != lvJobList.Items.Count)
            {
                lvJobList.Items.Clear();
                for (int i = 0; i < jobs.Count; ++i)
                {
                    string remaining = TimeSpanToString(jobs[i].timeout - now);
                    lvJobList.Items.Add(new ListViewItem(new string[] { i.ToString(), jobs[i].name, jobs[i].timeout.ToString(), remaining, jobs[i].repeat ? "する" : "しない" }));
                }
            }
            else
            {
                for (int i = 0; i < jobs.Count; ++i)
                {
                    string remaining = TimeSpanToString(jobs[i].timeout - now);
                    lvJobList.Items[i].SubItems[3].Text = remaining;
                }
            }
            lvJobList.EndUpdate();
        }

        private string TimeSpanToString(TimeSpan ts)
        {
            StringBuilder sb = new StringBuilder();
            int hours = (int)Math.Floor(ts.TotalHours);
            if (hours != 0) sb.AppendFormat("{0} 時間 ", hours);
            int minutes = ts.Minutes;
            if (hours != 0 || minutes != 0) sb.AppendFormat("{0} 分 ", minutes);
            int seconds = ts.Seconds;
            sb.AppendFormat("{0} 秒", seconds);
            return sb.ToString();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetControlSetEnabled(Control[] control_set, bool enabled)
        {
            foreach (var each in control_set) each.Enabled = enabled;
        }

        private void radRelative_CheckedChanged(object sender, EventArgs e)
        {
            SetControlSetEnabled(control_set_related, radRelative.Checked);
            SetControlSetEnabled(control_set_absolute, radAbsolute.Checked);
        }

        private void txtJobName_Enter(object sender, EventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox == null) return;
            textbox.SelectAll();
        }

        private void txtTimeOut_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox == null) return;
            string text = textbox.Text;
            int val;
            if (!int.TryParse(text, out val)) val = -1;
            if (e.KeyCode == Keys.Down)
            {
                val--;
                if (val < 0) val = 0;
                textbox.Text = val.ToString();
            }
            else if (e.KeyCode == Keys.Up)
            {
                val++;
                if (val < 0) val = 0;
                textbox.Text = val.ToString();
            }
            else
            {
                return;
            }
            e.Handled = true;
            return;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (DialogResult.No == MessageBox.Show("終了してもよろしいですか？", "確認", MessageBoxButtons.YesNo))
                    e.Cancel = true;
            }
        }

        private void ReloadSound()
        {
            if (sound_done != null) sound_done.Dispose();
            sound_done = new System.Media.SoundPlayer(settings.sound_file);
            sound_done.Load();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sound_done != null) sound_done.Dispose();
            if (notification_form != null) notification_form.Dispose();
            while (true)
            {
                try
                {
                    SaveSettings();
                    break;
                }
                catch (Exception ex)
                {
                    if (DialogResult.Cancel == MessageBox.Show(string.Format("設定を保存できませんでした:\r\n{0}", ex.ToString()), "設定保存エラー", MessageBoxButtons.RetryCancel))
                        break;
                }
            }
        }

        private void LoadSettings()
        {
            using (var xml = System.Xml.XmlReader.Create("test.xml"))
            {
                var doc = new System.Xml.XmlDocument();
                doc.Load(xml);
                foreach (var child in doc.ChildNodes)
                {
                    var node_settings = child as System.Xml.XmlElement;
                    if (node_settings == null || node_settings.Name != "settings")
                        continue;
                    var node1 = node_settings.GetElementsByTagName("joblistdir");
                    settings.joblist_dir = node1.Count > 0 ? node1[0].InnerText : "";
                    var node2 = node_settings.GetElementsByTagName("sound");
                    bool.TryParse(node2[0].InnerText, out settings.sound);
                    var node3 = node_settings.GetElementsByTagName("soundfile");
                    settings.sound_file = node3.Count > 0 ? node3[0].InnerText : "";
                }
            }
            string filepath = System.IO.Path.Combine(settings.joblist_dir, joblist_filename);
            using (var sr = new System.IO.StreamReader(filepath))
            {
                try
                {
                    txtTimeOut.Text = sr.ReadLine();
                    txtHour.Text = sr.ReadLine();
                    txtMinute.Text = sr.ReadLine();
                    txtSecond.Text = sr.ReadLine();
                    radRelative.Checked = "1" == sr.ReadLine();
                    radAbsolute.Checked = !radRelative.Checked;
                    int c = int.Parse(sr.ReadLine());
                    jobs.Clear();
                    for (int i = 0; i < c; ++i)
                        jobs.Add(TimerJob.FromString(sr.ReadLine()));
                    c = int.Parse(sr.ReadLine());
                    jobs_template.Clear();
                    for (int i = 0; i < c; ++i)
                        jobs_template.Add(TimerJob.FromString(sr.ReadLine()));
                }
                catch
                {

                }
            }
        }

        private void SaveSettings()
        {
            var xmlsettings = new System.Xml.XmlWriterSettings();
            xmlsettings.Indent = true;
            using (var xml = System.Xml.XmlWriter.Create("test.xml", xmlsettings))
            {
                xml.WriteStartElement("settings");
                xml.WriteElementString("joblistdir", settings.joblist_dir);
                xml.WriteElementString("sound", settings.sound.ToString());
                xml.WriteElementString("soundfile", settings.sound_file);
                xml.WriteEndElement(); // settings
            }
            string filepath = System.IO.Path.Combine(settings.joblist_dir, joblist_filename);
            using (var sw = new System.IO.StreamWriter(filepath))
            {
                sw.WriteLine(txtTimeOut.Text);
                sw.WriteLine(txtHour.Text);
                sw.WriteLine(txtMinute.Text);
                sw.WriteLine(txtSecond.Text);
                sw.WriteLine(radRelative.Checked ? "1" : "0");
                sw.WriteLine(jobs.Count);
                foreach (var each in jobs)
                    sw.WriteLine(each.Serialize());
                sw.WriteLine(jobs_template.Count);
                foreach (var each in jobs_template)
                    sw.WriteLine(each.Serialize());
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            if (lvJobList.SelectedItems.Count == 0) return;
            if (DialogResult.No == MessageBox.Show("選択されたジョブを削除します．よろしいですか？", "確認", MessageBoxButtons.YesNo)) return;
            List<TimerJob> remove_list = new List<TimerJob>();
            foreach (ListViewItem each in lvJobList.SelectedItems)
            {
                remove_list.Add(jobs[int.Parse(each.Text)]);
            }
            jobs.RemoveAll((x) => remove_list.Contains(x));
            UpdateJobList();
        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {
            if (!CheckForm()) return;
            if (radRelative.Checked)
            {
                double seconds = double.Parse(txtTimeOut.Text);
                jobs_template.Add(new TimerJob(txtJobName.Text, seconds, chkRepeat.Checked));
            }
            else if (radAbsolute.Checked)
            {
                DateTime goal = DateTime.Parse(string.Format("{0}:{1}:{2}", txtHour.Text, txtMinute.Text, txtSecond.Text));
                jobs_template.Add(new TimerJob(txtJobName.Text, goal, chkRepeat.Checked));
            }
            UpdateTemplateList();
            txtJobName.Clear();
            txtJobName.Focus();
        }

        private void UpdateTemplateList()
        {
            lstTemplate.Items.Clear();
            foreach (var each in jobs_template)
            {
                lstTemplate.Items.Add(each);
            }
        }

        private void lstTemplate_DoubleClick(object sender, EventArgs e)
        {
            if (lstTemplate.SelectedItems.Count <= 0) return;
            TimerJob job = lstTemplate.SelectedItems[0] as TimerJob;
            if (job == null) return;
            if (job.type == TimerJob.Type.Related && job.add_seconds == 0)
            {
                // 時間が 0 の場合は都度時間指定する
                TimeInputForm time_form = new TimeInputForm();
                time_form.Time = 0;
                if (time_form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    jobs.Add(new TimerJob(job.name, (double)time_form.Time, job.repeat));
                }
                time_form.Dispose();
            }
            else
            {
                jobs.Add(job.Again());
            }
            UpdateJobList();
            lstTemplate.SelectedItems.Clear();
        }

        private void btnTimeSet_Click(object sender, EventArgs e)
        {
            TimeInputForm time_form = new TimeInputForm();
            int initial_value;
            int.TryParse(txtTimeOut.Text, out initial_value);
            if (initial_value < 0) initial_value = 0;
            if (initial_value > 359999) initial_value = 359999;
            time_form.Time = initial_value;
            if (time_form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtTimeOut.Text = time_form.Time.ToString();
            }
            time_form.Dispose();
        }

        private void lstTemplate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lstTemplate.SelectedItems.Count > 0)
            {
                List<object> remove_target = new List<object>();
                foreach (var each in lstTemplate.SelectedItems)
                    jobs_template.Remove(each as TimerJob);
                UpdateTemplateList();
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            SettingForm f = new SettingForm();
            f.settings = this.settings;
            if (DialogResult.OK == f.ShowDialog())
            {
                this.settings = f.settings;
            }
            f.Dispose();
        }
    }

    public class TimerJob
    {
        public enum Type
        {
            Null,
            Related,
            Absolute,
        }

        public string name { get; private set; }
        public DateTime timeout { get; private set; }
        public bool repeat { get; private set; }
        public TimerJob.Type type { get; private set; }
        public double add_seconds { get; private set; }

        private TimerJob() { }

        public TimerJob(string name, DateTime timeout, bool again)
        {
            this.name = name;
            DateTime now = DateTime.Now;
            while (timeout <= now) timeout = timeout.AddDays(1.0);
            this.timeout = timeout;
            this.repeat = again;
            this.type = Type.Absolute;
            this.add_seconds = 0;
        }

        public TimerJob(string name, double seconds, bool again)
        {
            this.name = name;
            this.timeout = DateTime.Now.AddSeconds(seconds);
            this.repeat = again;
            this.type = Type.Related;
            this.add_seconds = seconds;
        }

        public TimerJob Again()
        {
            if (type == Type.Related)
            {
                return new TimerJob(name, add_seconds, repeat);
            }
            else if (type == Type.Absolute)
            {
                return new TimerJob(name, timeout, repeat);
            }
            else { throw new InvalidOperationException(); }
        }

        public string Serialize()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}", name, timeout.Ticks, repeat, (int)type, add_seconds);
        }

        public static TimerJob FromString(string s)
        {
            TimerJob ret = new TimerJob();
            string[] a = s.Split('\t');
            ret.name = a[0];
            ret.timeout = new DateTime(long.Parse(a[1]));
            ret.repeat = bool.Parse(a[2]);
            ret.type = (TimerJob.Type)int.Parse(a[3]);
            ret.add_seconds = double.Parse(a[4]);
            return ret;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.name);
            if (this.type == Type.Absolute)
            {
                sb.AppendFormat("({0,2}:{1,2})", timeout.Hour, timeout.Minute);
            }
            else if (this.type == Type.Related && this.add_seconds != 0)
            {
                sb.Append('(');
                int total_seconds = (int)this.add_seconds;
                int hours = total_seconds / 3600;
                total_seconds -= 3600 * hours;
                int minutes = total_seconds / 60;
                total_seconds -= 60 * minutes;
                if (hours > 0) sb.AppendFormat(" {0}時間", hours);
                if (minutes > 0) sb.AppendFormat(" {0}分", minutes);
                if (total_seconds > 0 || hours == 0 && minutes == 0) sb.AppendFormat(" {0}秒", total_seconds);
                sb.Append(')');
            }
            return sb.ToString();
            //return base.ToString();
        }
    }
    /* 再投入の種類
     * 1. 確認してから +x (相対)
     * 2. 翌日の xx:xx (絶対)
     */


}
