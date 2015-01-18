using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkDownX2.GUI.UserControls
{

    public class GradientPanel : Panel
    {
        private Color _ColorStart = Color.White;
        public Color ColorStart {
            get
            {
                return _ColorStart;
            }
            set
            {
                _ColorStart = value;
            }
        }
        private Color _ColorStop = Color.White;
        public Color ColorStop
        {
            get
            {
                return _ColorStop;
            }
            set
            {
                _ColorStop = value;
            }
        }
        public GradientPanel()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        private LinearGradientMode _GradientMode = LinearGradientMode.Vertical;
        public LinearGradientMode GardientMode
        {
            get
            {
                return _GradientMode;
            }
            set
            {
                _GradientMode = value;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            using (LinearGradientBrush brush = new LinearGradientBrush(ClientRectangle, ColorStart, ColorStop, _GradientMode))
            {
                pevent.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }
    }  

}
