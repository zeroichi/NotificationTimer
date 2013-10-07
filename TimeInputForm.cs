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
    /* 
     * TimeInputForm
     * 時間を設定するためのフォーム
     * 設定された時間は Time プロパティを通して取得できる
     * 時間の最大値は 99:59:59 = 359999 seconds
     */
    public partial class TimeInputForm : Form
    {
        private Label[] labels;
        private int[] dividers;

        public TimeInputForm()
        {
            InitializeComponent();
            labels = new Label[] { lblHour1, lblHour2, lblMinute1, lblMinute2, lblSecond1, lblSecond2 };
            dividers = new int[] { 36000, 3600, 600, 60, 10, 1 };
        }

        private int time;
        public int Time
        {
            get
            {
                return time;
            }
            set
            {
                if (value < 0 || value > 359999) throw new InvalidOperationException("範囲外の秒数が指定されました");
                time = value;
                UpdateSelector();
            }
        }

        /// <summary>
        /// 総秒数から時間・分・秒の各位の数字を求める
        /// </summary>
        /// <param name="t">総秒数</param>
        /// <returns>各位の数字を格納した配列</returns>
        private int[] TimeToHMS(int t)
        {
            int[] ret = new int[dividers.Length];
            for (int i = 0; i < dividers.Length; ++i)
            {
                ret[i] = t / dividers[i];
                t -= ret[i] * dividers[i];
            }
            System.Diagnostics.Debug.Assert(t == 0);
            return ret;
        }

        private int HMSToTime(int[] hms)
        {
            if (hms.Length != dividers.Length) throw new InvalidOperationException();
            int ret = 0;
            for (int i = 0; i < dividers.Length; ++i)
                ret += hms[i] * dividers[i];
            return ret;
        }

        private void UpdateSelector()
        {
            var hms = TimeToHMS(this.time);
            for (int i = 0; i < labels.Length; ++i)
                labels[i].Text = hms[i].ToString();
        }

        private void UpdateTime()
        {
            int[] hms = labels.Select((l) => int.Parse(l.Text.ToString())).ToArray();
            this.time = HMSToTime(hms);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            Label l = sender as Label;
            if (!labels.Contains(l)) return;
            int max_value = 9;
            if (l.Equals(lblMinute1) || l.Equals(lblSecond1)) max_value = 5;
            if (e.Button.HasFlag(MouseButtons.Left) && !e.Button.HasFlag(MouseButtons.Right))
            {
                // left click -> increment
                int old_value;
                int.TryParse(l.Text, out old_value);
                // note: old_value is 0 when failed to parse
                old_value++;
                if (old_value >= (max_value + 1)) old_value = 0;
                l.Text = old_value.ToString();
                UpdateTime();
            }
            else if (!e.Button.HasFlag(MouseButtons.Left) && e.Button.HasFlag(MouseButtons.Right))
            {
                // right click -> decrement
                int old_value;
                int.TryParse(l.Text, out old_value);
                // note: old_value is 0 when failed to parse
                old_value--;
                if (old_value < 0) old_value = max_value;
                l.Text = old_value.ToString();
                UpdateTime();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Time = 0;
        }
    }
}
