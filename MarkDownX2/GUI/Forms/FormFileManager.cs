using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileManagerConnector;
using MarkDownX2.Helpers;
using MarkDownX2.Models;

namespace MarkDownX2.GUI.Forms
{
    public partial class FormFileManager : Form
    {
        #region Properties

        public string SettingName
        {
            get
            {
                return EditName.Text;
            }
        }

        public string SettingUrl
        {
            get
            {
                return EditUrl.Text;
            }
        }

        public string SettingUsername
        {
            get
            {
                return EditUsername.Text;
            }
        }

        public string SettingPassword
        {
            get
            {
                return EditPassword.Text;
            }
        }

        public string SettingType
        {
            get
            {
                return ComboType.Text;
            }
        }


        #endregion

        public FormFileManager()
        {
            InitializeComponent();
            InitializeTypes();
        }

        private void FormFileManager_Load(object sender, EventArgs e)
        {

        }

        private void InitializeTypes()
        {
            foreach (IFileManagerInterface manager in FileManagerHelper.FileManagers)
            {
                ComboType.Items.Add(manager.Name);
            }
        }

        private void ComboType_Load(object sender, EventArgs e)
        {

        }

        private void ComboType_IndexChanged(object sender, EventArgs e)
        {
            try
            {
                IFileManagerInterface manager = FileManagerHelper.GetFileManager(ComboType.Text);
                if (manager != null)
                {
                    PanelPassword.Visible = manager.RequiresPassword;
                    PanelUsername.Visible = manager.RequiresUsername;                   
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.Process(ex);
            }
        }

        private void FormFileManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                e.Cancel = false;
                return;
            }
            if (String.IsNullOrEmpty(ComboType.Text))
            {
                MessageBox.Show("You must select a filemanager type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            if (String.IsNullOrEmpty(EditName.Text))
            {
                MessageBox.Show("You must enter a name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            if (String.IsNullOrEmpty(EditUrl.Text))
            {
                MessageBox.Show("You must enter a connection url", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            if (PanelUsername.Visible && String.IsNullOrEmpty(EditUsername.Text))
            {
                MessageBox.Show("You must provide a username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            if (PanelPassword.Visible && String.IsNullOrEmpty(EditPassword.Text))
            {
                MessageBox.Show("You must provide a password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            
        }
    }
}
