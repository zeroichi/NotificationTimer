﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace notification_timer
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    static class FlashWindow
    {
        [DllImport("user32.dll")]
        static extern Int32 FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            public UInt32 cbSize;    // FLASHWINFO構造体のサイズ
            public IntPtr hwnd;      // 点滅対象のウィンドウ・ハンドル
            public UInt32 dwFlags;   // 以下の「FLASHW_XXX」のいずれか
            public UInt32 uCount;    // 点滅する回数
            public UInt32 dwTimeout; // 点滅する間隔（ミリ秒単位）
        }

        // 点滅を止める
        public const UInt32 FLASHW_STOP = 0;
        // タイトルバーを点滅させる
        public const UInt32 FLASHW_CAPTION = 1;
        // タスクバー・ボタンを点滅させる
        public const UInt32 FLASHW_TRAY = 2;
        // タスクバー・ボタンとタイトルバーを点滅させる
        public const UInt32 FLASHW_ALL = 3;
        // FLASHW_STOPが指定されるまでずっと点滅させる
        public const UInt32 FLASHW_TIMER = 4;
        // ウィンドウが最前面に来るまでずっと点滅させる
        public const UInt32 FLASHW_TIMERNOFG = 12;

        public static void Flash(Form form, UInt32 flag)
        {
            FLASHWINFO info = new FLASHWINFO();
            info.cbSize = Convert.ToUInt32(Marshal.SizeOf(info));
            info.hwnd = form.Handle;
            info.dwFlags = flag;
            info.uCount = 5;
            info.dwTimeout = 0;
            FlashWindowEx(ref info);
        }
    }
}
