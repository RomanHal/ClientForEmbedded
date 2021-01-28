using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaserFeederHelper.Models
{
    class WindowFocuser
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public void FocusAndWrite(string processName,string message)
        {
            var importantMessage = "test123";
            var proceses = Process.GetProcessesByName("notepad++");
            var handler = proceses[0].MainWindowHandle;
            ShowWindow(handler, 5);
            SetForegroundWindow(handler);
            WriteAsKeyboard(importantMessage);

        }
        void WriteAsKeyboard(string s)
        {
            SendKeys.SendWait(s);
        }
    }
}
