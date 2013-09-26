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
        NotificationForm notification_form;
        Control[] control_set_absolute;
        Control[] control_set_related;
        System.Media.SoundPlayer sound_done;

        public MainForm()
        {
            InitializeComponent();
            jobs = new List<TimerJob>();
            jobs_done = new List<TimerJob>();
            timer1.Enabled = true;
            control_set_related = new Control[] { label2, label4, txtTimeOut };
            control_set_absolute = new Control[] { txtHour, txtMinute, txtSecond, label5, label6, label7 };
            sound_done = new System.Media.SoundPlayer("se_maoudamashii_chime13.wav");
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
                    notification_form = new NotificationForm(jobs_done);
                    notification_form.Show();
                }
                else
                {
                    notification_form.WindowState = FormWindowState.Normal;
                    notification_form.Show();
                }
            }
            if (chkSound.Checked && play_sound)
            {
                try
                {
                    sound_done.Play();
                }
                catch (System.IO.FileNotFoundException)
                {
                    chkSound.Checked = false;
                }
            }
            UpdateJobList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // check job name
            if (txtJobName.Text.Trim() == "")
            {
                MessageBox.Show("ジョブ名を入力してください");
                txtJobName.Clear();
                txtJobName.Focus();
                return;
            }
            if (radRelative.Checked)
            {
                double seconds;
                if (!double.TryParse(txtTimeOut.Text, out seconds) || seconds <= 0)
                {
                    MessageBox.Show("タイムアウト時間を正の数値で指定してください");
                    txtTimeOut.SelectAll();
                    txtTimeOut.Focus();
                    return;
                }
                jobs.Add(new TimerJob(txtJobName.Text, DateTime.Now.AddSeconds(seconds)));
            }
            else if (radAbsolute.Checked)
            {
                DateTime goal;
                if (!DateTime.TryParse(string.Format("{0}:{1}:{2}", txtHour.Text, txtMinute.Text, txtSecond.Text), out goal))
                {
                    MessageBox.Show("時刻を正しく指定してください");
                    txtHour.SelectAll();
                    txtHour.Focus();
                    return;
                }
                if (goal < DateTime.Now) goal = goal.AddDays(1.0);
                jobs.Add(new TimerJob(txtJobName.Text, goal));
            }
            txtJobName.Clear();
            txtJobName.Focus();
        }

        private void UpdateJobList()
        {
            if (jobs == null) return;
            DateTime now = DateTime.Now;
            if (jobs.Count != lvJobList.Items.Count)
            {
                lvJobList.Items.Clear();
                for (int i = 0; i < jobs.Count; ++i)
                {
                    string remaining = TimeSpanToString(jobs[i].timeout - now);
                    lvJobList.Items.Add(new ListViewItem(new string[] { i.ToString(), jobs[i].name, jobs[i].timeout.ToString(), remaining }));
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
            //System.Diagnostics.Debug.WriteLine(textbox.Name);
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
    }

    public class TimerJob
    {
        public string name { get; private set; }
        public DateTime timeout { get; private set; }

        public TimerJob(string name, DateTime timeout)
        {
            this.name = name;
            this.timeout = timeout;
        }
    }
}
