using LaserFeederHelper.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaserFeederHelper.Models.Connection.Interpreters
{
    class ShootHandler : DecodingHandler
    {
        private readonly Action _errorHandler;

        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow); //ShowWindow needs an IntPtr
        public ShootHandler(Func<byte> getData, Action<byte[]> send,Action errorHandler) : base(getData, send)
        {
            _errorHandler = errorHandler;
        }

        public override void Handle(byte data)
        {
            if (data == SerialCodeCommand.LaserShoot)
            {
                ShootHandling();
                Send(SerialCodeRsponse.LaserShootOk);
            }
        }

        private void ShootHandling()
        {
            var proceses = Process.GetProcessesByName("notepad++");
            var handler = proceses[0].MainWindowHandle;
            ShowWindow(handler, 5);
            SetForegroundWindow(handler);
            SendKeys.SendWait("NIeWIemCOroBie");
            throw new NotImplementedException("TODO and Test");
        }
    }
}
