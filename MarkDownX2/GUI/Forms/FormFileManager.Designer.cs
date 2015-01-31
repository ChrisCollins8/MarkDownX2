namespace MarkDownX2.GUI.Forms
{
    partial class FormFileManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelWrapper = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.PanelPassword = new System.Windows.Forms.Panel();
            this.PanelUsername = new System.Windows.Forms.Panel();
            this.PanelUrl = new System.Windows.Forms.Panel();
            this.PanelName = new System.Windows.Forms.Panel();
            this.PanelType = new System.Windows.Forms.Panel();
            this.bevel = new Bevel.BevelControl();
            this.EditPassword = new DevNotepad.UI.Controls.LabelEdit();
            this.EditUsername = new DevNotepad.UI.Controls.LabelEdit();
            this.EditUrl = new DevNotepad.UI.Controls.LabelEdit();
            this.EditName = new DevNotepad.UI.Controls.LabelEdit();
            this.ComboType = new DevNotepad.UI.Controls.ComboEdit();
            this.gradientPanel1 = new MarkDownX2.GUI.UserControls.GradientPanel();
            this.LabelDescription = new System.Windows.Forms.Label();
            this.LabelTitle = new System.Windows.Forms.Label();
            this.PictureIcon = new System.Windows.Forms.PictureBox();
            this.panelWrapper.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.PanelPassword.SuspendLayout();
            this.PanelUsername.SuspendLayout();
            this.PanelUrl.SuspendLayout();
            this.PanelName.SuspendLayout();
            this.PanelType.SuspendLayout();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // panelWrapper
            // 
            this.panelWrapper.Controls.Add(this.panelButtons);
            this.panelWrapper.Controls.Add(this.bevel);
            this.panelWrapper.Controls.Add(this.PanelPassword);
            this.panelWrapper.Controls.Add(this.PanelUsername);
            this.panelWrapper.Controls.Add(this.PanelUrl);
            this.panelWrapper.Controls.Add(this.PanelName);
            this.panelWrapper.Controls.Add(this.PanelType);
            this.panelWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWrapper.Location = new System.Drawing.Point(0, 69);
            this.panelWrapper.Name = "panelWrapper";
            this.panelWrapper.Padding = new System.Windows.Forms.Padding(7);
            this.panelWrapper.Size = new System.Drawing.Size(549, 266);
            this.panelWrapper.TabIndex = 2;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.ButtonCancel);
            this.panelButtons.Controls.Add(this.ButtonOk);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(7, 209);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.panelButtons.Size = new System.Drawing.Size(535, 50);
            this.panelButtons.TabIndex = 5;
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ButtonCancel.Location = new System.Drawing.Point(5, 10);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 35);
            this.ButtonCancel.TabIndex = 2;
            this.ButtonCancel.Text = "&Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // ButtonOk
            // 
            this.ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.ButtonOk.Location = new System.Drawing.Point(455, 10);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 35);
            this.ButtonOk.TabIndex = 1;
            this.ButtonOk.Text = "&OK";
            this.ButtonOk.UseVisualStyleBackColor = true;
            // 
            // PanelPassword
            // 
            this.PanelPassword.Controls.Add(this.EditPassword);
            this.PanelPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelPassword.Location = new System.Drawing.Point(7, 167);
            this.PanelPassword.Name = "PanelPassword";
            this.PanelPassword.Padding = new System.Windows.Forms.Padding(8);
            this.PanelPassword.Size = new System.Drawing.Size(535, 40);
            this.PanelPassword.TabIndex = 3;
            // 
            // PanelUsername
            // 
            this.PanelUsername.Controls.Add(this.EditUsername);
            this.PanelUsername.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelUsername.Location = new System.Drawing.Point(7, 127);
            this.PanelUsername.Name = "PanelUsername";
            this.PanelUsername.Padding = new System.Windows.Forms.Padding(8);
            this.PanelUsername.Size = new System.Drawing.Size(535, 40);
            this.PanelUsername.TabIndex = 2;
            // 
            // PanelUrl
            // 
            this.PanelUrl.Controls.Add(this.EditUrl);
            this.PanelUrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelUrl.Location = new System.Drawing.Point(7, 87);
            this.PanelUrl.Name = "PanelUrl";
            this.PanelUrl.Padding = new System.Windows.Forms.Padding(8);
            this.PanelUrl.Size = new System.Drawing.Size(535, 40);
            this.PanelUrl.TabIndex = 1;
            // 
            // PanelName
            // 
            this.PanelName.Controls.Add(this.EditName);
            this.PanelName.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelName.Location = new System.Drawing.Point(7, 47);
            this.PanelName.Name = "PanelName";
            this.PanelName.Padding = new System.Windows.Forms.Padding(8);
            this.PanelName.Size = new System.Drawing.Size(535, 40);
            this.PanelName.TabIndex = 0;
            // 
            // PanelType
            // 
            this.PanelType.Controls.Add(this.ComboType);
            this.PanelType.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelType.Location = new System.Drawing.Point(7, 7);
            this.PanelType.Name = "PanelType";
            this.PanelType.Padding = new System.Windows.Forms.Padding(8);
            this.PanelType.Size = new System.Drawing.Size(535, 40);
            this.PanelType.TabIndex = 4;
            // 
            // bevel
            // 
            this.bevel.BevelStyle = Bevel.BevelStyle.Lowered;
            this.bevel.BevelType = Bevel.BevelType.TopLine;
            this.bevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.bevel.HighlightColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bevel.Location = new System.Drawing.Point(7, 207);
            this.bevel.Name = "bevel";
            this.bevel.Padding = new System.Windows.Forms.Padding(10);
            this.bevel.ShadowColor = System.Drawing.SystemColors.ButtonShadow;
            this.bevel.Size = new System.Drawing.Size(535, 2);
            this.bevel.TabIndex = 5;
            this.bevel.TabStop = false;
            this.bevel.Text = "bevelControl1";
            // 
            // EditPassword
            // 
            this.EditPassword.Caption = "&Password:";
            this.EditPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditPassword.Location = new System.Drawing.Point(8, 8);
            this.EditPassword.MinimumSize = new System.Drawing.Size(0, 25);
            this.EditPassword.Name = "EditPassword";
            this.EditPassword.PasswordChar = '\0';
            this.EditPassword.Size = new System.Drawing.Size(519, 25);
            this.EditPassword.TabIndex = 0;
            // 
            // EditUsername
            // 
            this.EditUsername.Caption = "&UserName:";
            this.EditUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditUsername.Location = new System.Drawing.Point(8, 8);
            this.EditUsername.MinimumSize = new System.Drawing.Size(0, 25);
            this.EditUsername.Name = "EditUsername";
            this.EditUsername.PasswordChar = '\0';
            this.EditUsername.Size = new System.Drawing.Size(519, 25);
            this.EditUsername.TabIndex = 0;
            // 
            // EditUrl
            // 
            this.EditUrl.Caption = "&Url:";
            this.EditUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditUrl.Location = new System.Drawing.Point(8, 8);
            this.EditUrl.MinimumSize = new System.Drawing.Size(0, 25);
            this.EditUrl.Name = "EditUrl";
            this.EditUrl.PasswordChar = '\0';
            this.EditUrl.Size = new System.Drawing.Size(519, 25);
            this.EditUrl.TabIndex = 0;
            // 
            // EditName
            // 
            this.EditName.Caption = "&Name:";
            this.EditName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditName.Location = new System.Drawing.Point(8, 8);
            this.EditName.MinimumSize = new System.Drawing.Size(0, 25);
            this.EditName.Name = "EditName";
            this.EditName.PasswordChar = '\0';
            this.EditName.Size = new System.Drawing.Size(519, 25);
            this.EditName.TabIndex = 2;
            // 
            // ComboType
            // 
            this.ComboType.Caption = "&File Manager Type:";
            this.ComboType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComboType.Location = new System.Drawing.Point(8, 8);
            this.ComboType.MinimumSize = new System.Drawing.Size(0, 20);
            this.ComboType.Name = "ComboType";
            this.ComboType.Size = new System.Drawing.Size(519, 24);
            this.ComboType.TabIndex = 0;
            this.ComboType.IndexChanged += new System.EventHandler(this.ComboType_IndexChanged);
            this.ComboType.Load += new System.EventHandler(this.ComboType_Load);
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.gradientPanel1.ColorStart = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(154)))), ((int)(((byte)(173)))));
            this.gradientPanel1.ColorStop = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(47)))));
            this.gradientPanel1.Controls.Add(this.LabelDescription);
            this.gradientPanel1.Controls.Add(this.LabelTitle);
            this.gradientPanel1.Controls.Add(this.PictureIcon);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel1.GardientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(549, 69);
            this.gradientPanel1.TabIndex = 3;
            // 
            // LabelDescription
            // 
            this.LabelDescription.AutoSize = true;
            this.LabelDescription.BackColor = System.Drawing.Color.Transparent;
            this.LabelDescription.ForeColor = System.Drawing.Color.White;
            this.LabelDescription.Location = new System.Drawing.Point(74, 31);
            this.LabelDescription.Name = "LabelDescription";
            this.LabelDescription.Size = new System.Drawing.Size(187, 13);
            this.LabelDescription.TabIndex = 2;
            this.LabelDescription.Text = "Create or update a filemanager config.";
            // 
            // LabelTitle
            // 
            this.LabelTitle.AutoSize = true;
            this.LabelTitle.BackColor = System.Drawing.Color.Transparent;
            this.LabelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTitle.ForeColor = System.Drawing.Color.White;
            this.LabelTitle.Location = new System.Drawing.Point(71, 9);
            this.LabelTitle.Name = "LabelTitle";
            this.LabelTitle.Size = new System.Drawing.Size(192, 20);
            this.LabelTitle.TabIndex = 1;
            this.LabelTitle.Text = "Manage a FileManager";
            // 
            // PictureIcon
            // 
            this.PictureIcon.BackColor = System.Drawing.Color.Transparent;
            this.PictureIcon.Image = global::MarkDownX2.Properties.Resources.file_manager1;
            this.PictureIcon.Location = new System.Drawing.Point(7, 2);
            this.PictureIcon.Name = "PictureIcon";
            this.PictureIcon.Size = new System.Drawing.Size(64, 64);
            this.PictureIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PictureIcon.TabIndex = 0;
            this.PictureIcon.TabStop = false;
            // 
            // FormFileManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 335);
            this.Controls.Add(this.panelWrapper);
            this.Controls.Add(this.gradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFileManager";
            this.Text = "FormFileManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFileManager_FormClosing);
            this.panelWrapper.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.PanelPassword.ResumeLayout(false);
            this.PanelUsername.ResumeLayout(false);
            this.PanelUrl.ResumeLayout(false);
            this.PanelName.ResumeLayout(false);
            this.PanelType.ResumeLayout(false);
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelWrapper;
        private Bevel.BevelControl bevel;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Panel PanelUsername;
        private DevNotepad.UI.Controls.LabelEdit EditUsername;
        private System.Windows.Forms.Panel PanelUrl;
        private DevNotepad.UI.Controls.LabelEdit EditUrl;
        private System.Windows.Forms.Panel PanelName;
        private DevNotepad.UI.Controls.LabelEdit EditName;
        private UserControls.GradientPanel gradientPanel1;
        private System.Windows.Forms.Label LabelDescription;
        private System.Windows.Forms.Label LabelTitle;
        private System.Windows.Forms.PictureBox PictureIcon;
        private System.Windows.Forms.Panel PanelPassword;
        private DevNotepad.UI.Controls.LabelEdit EditPassword;
        private System.Windows.Forms.Panel PanelType;
        private DevNotepad.UI.Controls.ComboEdit ComboType;
    }
}