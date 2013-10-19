using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace notification_timer
{
    // Double Buffer を使用し，ちらつきのない描画をする ListView
    public class ListViewDoubleBuffered : System.Windows.Forms.ListView
    {
        protected override bool DoubleBuffered
        {
            get
            {
                return true;
            }
            set { }
        }
    }
}
