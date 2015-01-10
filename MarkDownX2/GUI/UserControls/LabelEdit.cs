using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevNotepad.UI.Controls
{

    /// <summary>
    /// Simple labeledit control. Combines a label and a textbox to simplify
    /// adding both to a form.
    /// </summary>
    public partial class LabelEdit : UserControl
    {
        #region Properties

        public override string Text
        {
            get
            {
                return TextBox.Text;
            }
            set
            {
                TextBox.Text = value;
            }
        }

        public string Caption
        {
            get
            {
                return Label.Text;
            }
            set
            {
                Label.Text = value;
            }
        }

        #endregion

        public LabelEdit()
        {
            InitializeComponent();
        }

        public void SetFocus()
        {
            TextBox.Focus();
            
        }

        private void Label_Click(object sender, EventArgs e)
        {
            SetFocus();
        }
    }
}
