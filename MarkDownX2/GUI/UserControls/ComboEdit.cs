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
    public partial class ComboEdit : UserControl
    {
        public event EventHandler IndexChanged;

        #region Properties

        public override string Text
        {
            get
            {
                return ComboBox.Text;
            }
            set
            {
                ComboBox.Text = value;
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

        /// <summary>
        /// Returns the combobox item collection.
        /// </summary>
        public System.Windows.Forms.ComboBox.ObjectCollection Items
        {
            get
            {
                return ComboBox.Items;
            }
        }

        

        #endregion

        public ComboEdit()
        {
            InitializeComponent();
        }

        public void HandleIndexChanged(object sender, EventArgs e)
        {
            this.OnIndexChanged(EventArgs.Empty);
        }

        public void SetFocus()
        {
            ComboBox.Focus();            
        }

        private void Label_Click(object sender, EventArgs e)
        {
            SetFocus();
        }

        protected virtual void OnIndexChanged(EventArgs e)
        {
            EventHandler handler = this.IndexChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleIndexChanged(sender, e);
        }
    }
}
