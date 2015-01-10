using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkDownX2.Helpers
{
    public static class SuspendUpdate
    {
        [DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);
        
        public static void Suspend(Control control)
        {
            LockWindowUpdate(control.Handle);
        }

        public static void Resume()
        {
            LockWindowUpdate(IntPtr.Zero);
        }
    }
}
