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
        List<TimerJob> jobs_done;

        public NotificationForm( List<TimerJob> jobs)
        {
            jobs_done = jobs;
            InitializeComponent();
            timer1.Interval = 500;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
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
            jobs_done.Clear();
            this.Close();
        }

        private void NotificationForm_Shown(object sender, EventArgs e)
        {
            UpdateList();
        }
    }
}
