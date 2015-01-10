// Simple bevel control. Source from:
// http://www.codeproject.com/Articles/17342/Simple-Bevel-Control
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Bevel
{
    public enum BevelStyle
    {
        Lowered,
        Raised
    }

    public enum BevelType
    {
        Box,
        Frame,
        TopLine,
        BottomLine,
        LeftLine,
        RightLine,
        Spacer
    }

    public class BevelControl : Control
    {
        #region private members
        private BevelStyle _bevelStyle = BevelStyle.Lowered;
        private BevelType _bevelType = BevelType.Box;
        private Color _shadowColor = SystemColors.ButtonShadow;
        private Color _highlightColor = SystemColors.ButtonHighlight;
        #endregion

        #region protected methods (painting)
        protected virtual Pen GetPen(int iIndex)
        {
            Color color = Color.Black;

            if (iIndex.Equals(0))
                color = _bevelStyle.Equals(BevelStyle.Lowered) ? _shadowColor : _highlightColor;
            else
                color = _bevelStyle.Equals(BevelStyle.Lowered) ? _highlightColor : _shadowColor;

            return new Pen(color);
        }
        protected virtual void BevelRect(Graphics iGraphics, Rectangle iRect)
        {
            using (Pen pen = GetPen(0))
            {
                iGraphics.DrawLine(pen, iRect.Left, iRect.Bottom, iRect.Left, iRect.Top);
                iGraphics.DrawLine(pen, iRect.Left, iRect.Top, iRect.Right, iRect.Top);
            }
            using (Pen pen = GetPen(1))
            {
                iGraphics.DrawLine(pen, iRect.Right, iRect.Top, iRect.Right, iRect.Bottom);
                iGraphics.DrawLine(pen, iRect.Right, iRect.Bottom, iRect.Left, iRect.Bottom);
            }
        }
        protected virtual void FrameRect(Graphics iGraphics, Rectangle iRect)
        {
            using (Pen pen = GetPen(1))
                iGraphics.DrawRectangle(pen, iRect);

            iRect = new Rectangle(iRect.Left - 1, iRect.Top - 1, iRect.Width, iRect.Height);
            using (Pen pen = GetPen(0))
                iGraphics.DrawRectangle(pen, iRect);
        }
        protected virtual void BevelLine(Pen iPen, Graphics iGraphics, int iX1, int iY1, int iX2, int iY2)
        {
            iGraphics.DrawLine(iPen, iX1, iY1, iX2, iY2);
        }
        protected virtual void SpacerRect(Graphics iGraphics, Rectangle iRect)
        {
            using (Pen pen = new Pen(Color.Black))
            {
                pen.DashStyle = DashStyle.Dot;
                iGraphics.DrawRectangle(pen, iRect);
            }
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            // Calling the base class OnPaint
            base.OnPaint(pe);

            switch (_bevelType)
            {
                case BevelType.Box:
                    BevelRect(pe.Graphics, new Rectangle(0, 0, Width - 1, Height - 1));
                    break;
                case BevelType.Frame:
                    FrameRect(pe.Graphics, new Rectangle(1, 1, Width - 2, Height - 2));
                    break;
                case BevelType.TopLine:
                    using (Pen pen = GetPen(0))
                        BevelLine(pen, pe.Graphics, 0, 0, Width, 0);
                    using (Pen pen = GetPen(1))
                        BevelLine(pen, pe.Graphics, 0, 1, Width, 1);
                    break;
                case BevelType.BottomLine:
                    using (Pen pen = GetPen(0))
                        BevelLine(pen, pe.Graphics, 0, Height - 2, Width, Height - 2);
                    using (Pen pen = GetPen(1))
                        BevelLine(pen, pe.Graphics, 0, Height - 1, Width, Height - 1);
                    break;
                case BevelType.LeftLine:
                    using (Pen pen = GetPen(0))
                        BevelLine(pen, pe.Graphics, 0, 0, 0, Height);
                    using (Pen pen = GetPen(1))
                        BevelLine(pen, pe.Graphics, 1, 0, 1, Height);
                    break;
                case BevelType.RightLine:
                    using (Pen pen = GetPen(0))
                        BevelLine(pen, pe.Graphics, Width - 2, 0, Width - 2, Height);
                    using (Pen pen = GetPen(1))
                        BevelLine(pen, pe.Graphics, Width - 1, 0, Width - 1, Height);
                    break;
                case BevelType.Spacer:
                    if (DesignMode)
                        SpacerRect(pe.Graphics, new Rectangle(0, 0, Width - 1, Height - 1));
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region public properties
        [Category("Bevel"), Description("How to draw edge")]
        public BevelStyle BevelStyle
        {
            get { return _bevelStyle; }
            set { _bevelStyle = value; Refresh(); }
        }
        [Category("Bevel"), Description("Where to draw edge")]
        public BevelType BevelType
        {
            get { return _bevelType; }
            set { _bevelType = value; Refresh(); }
        }
        [Category("Bevel")]
        [BrowsableAttribute(true)]
        public Color HighlightColor
        {
            get { return _highlightColor; }
            set { _highlightColor = value; Refresh(); }
        }
        [Category("Bevel")]
        [BrowsableAttribute(true)]
        public Color ShadowColor
        {
            get { return _shadowColor; }
            set { _shadowColor = value; Refresh(); }
        }
        #endregion
    }
}