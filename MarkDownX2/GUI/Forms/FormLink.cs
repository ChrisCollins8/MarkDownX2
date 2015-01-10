using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkDownX2.GUI.Forms
{
    public partial class FormLink : Form
    {
        #region Properties

        /// <summary>
        /// Returns the Url
        /// </summary>
        public string Url
        {
            get
            {
                return EditUrl.Text;
            }
        }

        /// <summary>
        /// Returns the title text
        /// </summary>
        public string Title
        {
            get
            {
                return EditTitle.Text;
            }
        }

        /// <summary>
        /// Returns the anchor text
        /// </summary>
        public string Anchor
        {
            get
            {
                return EditAnchor.Text;
            }
        }

        #endregion
        public FormLink(string baseText)
        {
            InitializeComponent();
            if (!String.IsNullOrEmpty(baseText))
            {
                EditTitle.Text = baseText;
                EditAnchor.Text = baseText;
            }
        }

        private void FormLink_Load(object sender, EventArgs e)
        {
            EditUrl.Text = "http://";
            ActiveControl = EditUrl;
        }

        private void bevel_Click(object sender, EventArgs e)
        {

        }
    }
}
