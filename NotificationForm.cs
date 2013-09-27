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
    public partial class NotificationForm : Form
    {
        List<TimerJob> jobs;
        List<TimerJob> jobs_done;

        public NotificationForm(List<TimerJob> jobs, List<TimerJob> jobs_done)
        {
            this.jobs = jobs;
            this.jobs_done = jobs_done;
            InitializeComponent();
            timer1.Interval = 500;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int now_ms = DateTime.Now.Millisecond;
            Color color_new = now_ms < 500 ? Color.Black : Color.Red;
            if (lblMessage.ForeColor.ToArgb() != color_new.ToArgb())
                lblMessage.ForeColor = color_new;
            UpdateList();
        }

        private void UpdateList()
        {
            if (lstDoneJobs.Items.Count != jobs_done.Count)
            {
                lstDoneJobs.Items.Clear();
                foreach (var each in jobs_done)
                {
                    lstDoneJobs.Items.Add(each.name);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NotificationForm_Shown(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void NotificationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var each in jobs_done)
            {
                if (each.repeat)
                {
                    jobs.Add(each.Again());
                }
            }
            jobs_done.Clear();
        }
    }
}
