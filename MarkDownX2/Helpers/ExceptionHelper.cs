using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkDownX2.Helpers
{
    public static class ExceptionHelper
    {

        /// <summary>
        /// Show exception details. Ideally this should be extended in the future
        /// to be more robust but ideally there also shouldn't be any hard exceptions
        /// </summary>
        /// <param name="ex"></param>
        public static void Process(Exception ex)
        {
            MessageBox.Show(ex.ToString(), "An Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
