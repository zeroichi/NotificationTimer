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
        /// <summary>
        /// 実行中のジョブリスト
        /// </summary>
        List<TimerJob> jobs;
        /// <summary>
        /// 実行が完了し，確認待ちのジョブリスト
        /// </summary>
        List<TimerJob> jobs_done;
        /// <summary>
        /// ジョブのテンプレート
        /// </summary>
        List<TimerJob> jobs_template;
        NotificationForm notification_form;
        Control[] control_set_absolute;
        Control[] control_set_related;
        System.Media.SoundPlayer sound_done;
        const string settings_xml = "settings.xml";
        const string joblist_filename = "joblist.txt";

        private Settings _settings;
        /// <summary>
        /// アプリケーションの基本設定
        /// </summary>
        Settings settings
        {
            get
            {
                return _settings;
            }
            set
            {
                // 設定を sound_done, fileSystemWatcher1 に反映
                //System.Diagnostics.Debug.WriteLine("settings.set called");
                _settings = value;
                if (sound_done != null) sound_done.Dispose();
                if (_settings.sound)
                {
                    sound_done = new System.Media.SoundPlayer(_settings.sound_file);
                }
                fileSystemWatcher1.Path = _settings.joblist_dir;
                fileSystemWatcher1.Filter = joblist_filename;
            }
        }

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
            // ファイルから基本設定を読み込む
            try
            {
                this.settings = Settings.Load(settings_xml);
            }
            catch
            {
                MessageBox.Show("設定ファイルを読み込めませんでした．初期設定を行ってください．");
                this.settings = new Settings(false, "", "."); // デフォルト設定
                using (var f = new SettingForm())
                {
                    if (DialogResult.OK == f.ShowDialog())
                    {
                        this.settings = f.settings;
                    }
                }
            }
            // ファイルからジョブリストを読み込む
            try
            {
                LoadJobList();
            }
            catch { } // あらゆるエラーを無視
            UpdateJobList();
            UpdateTemplateList();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool play_sound = false;
            if (jobs == null) return;
            DateTime now = DateTime.Now;
            // タイムアウトしたかどうかを確認し，該当するものは jobs_done へ移す
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
            // jobs_done が空でなければ，通知ウィンドウを表示する
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
            // たったいま完了したジョブがあり，設定でサウンドが有効になっている場合は wav を再生する
            // for の外に出しているのは，複数のジョブが同時に完了した時に何度も再生するのを防止するため
            if (settings.sound && play_sound)
            {
                try
                {
                    sound_done.Play();
                }
                catch (System.IO.FileNotFoundException)
                {
                    // TODO: サウンドが再生できない場合のエラー処理
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
            if (!e.Cancel)
            {
                bool retry = false;
                do
                {
                    try
                    {
                        SaveJobList();
                        retry = false;
                    }
                    catch (Exception ex)
                    {
                        switch (MessageBox.Show(string.Format("ジョブリストを保存できませんでした:\r\n{0}", ex.ToString()), "ジョブリスト保存エラー", MessageBoxButtons.AbortRetryIgnore))
                        {
                            case System.Windows.Forms.DialogResult.Abort:
                                // アプリケーションの終了をキャンセル
                                retry = false;
                                e.Cancel = true;
                                break;
                            case System.Windows.Forms.DialogResult.Ignore:
                                // ジョブリストを保存せずに強制終了
                                retry = false;
                                e.Cancel = false;
                                break;
                            case System.Windows.Forms.DialogResult.Retry:
                                // ジョブリスト保存を再試行
                                retry = true;
                                break;
                        }
                    }
                } while (retry);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sound_done != null) sound_done.Dispose();
            if (notification_form != null) notification_form.Dispose();
        }

        private void LoadJobList()
        {
            string filepath = System.IO.Path.Combine(settings.joblist_dir, joblist_filename);
            using (var sr = new System.IO.StreamReader(filepath))
            {
                //txtTimeOut.Text = sr.ReadLine();
                //txtHour.Text = sr.ReadLine();
                //txtMinute.Text = sr.ReadLine();
                //txtSecond.Text = sr.ReadLine();
                //radRelative.Checked = "1" == sr.ReadLine();
                //radAbsolute.Checked = !radRelative.Checked;
                int c = int.Parse(sr.ReadLine());
                jobs.Clear();
                for (int i = 0; i < c; ++i)
                    jobs.Add(TimerJob.FromString(sr.ReadLine()));
                c = int.Parse(sr.ReadLine());
                jobs_template.Clear();
                for (int i = 0; i < c; ++i)
                    jobs_template.Add(TimerJob.FromString(sr.ReadLine()));
            }
        }

        private void SaveJobList()
        {
            string filepath = System.IO.Path.Combine(settings.joblist_dir, joblist_filename);
            using (var sw = new System.IO.StreamWriter(filepath))
            {
                //sw.WriteLine(txtTimeOut.Text);
                //sw.WriteLine(txtHour.Text);
                //sw.WriteLine(txtMinute.Text);
                //sw.WriteLine(txtSecond.Text);
                //sw.WriteLine(radRelative.Checked ? "1" : "0");
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
                time_form.TopMost = this.TopMost;
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
            time_form.TopMost = this.TopMost;
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
                // TODO: ジョブリスト保存ディレクトリが変更され，変更先に既にジョブリストが
                // 存在する場合，それを読み込むかどうか確認するメッセージを出す
            }
            f.Dispose();
            // 設定ファイルに保存
            while (true)
            {
                try
                {
                    this.settings.Save(settings_xml);
                    break;
                }
                catch (Exception ex)
                {
                    if (DialogResult.Cancel == MessageBox.Show(string.Format("設定をファイルに保存できませんでした:\r\n{0}", ex.ToString()), "設定保存エラー", MessageBoxButtons.RetryCancel))
                        break;
                }
            }
        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Filesystem changed: " + e.FullPath);
        }

        private void chkTopMost_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = chkTopMost.Checked;
        }
    }

    public class TimerJob
    {
        public enum Type
        {
            /// <summary>
            /// 無効な TimerJob．
            /// </summary>
            Null,
            /// <summary>
            /// 投入時からタイムアウトまでの時間の長さを相対的に指定するタイプ．
            /// </summary>
            Related,
            /// <summary>
            /// 絶対的なタイムアウト時刻を指定するタイプ．
            /// </summary>
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
}
