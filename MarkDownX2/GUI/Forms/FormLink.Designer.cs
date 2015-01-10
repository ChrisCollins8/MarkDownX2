namespace MarkDownX2.GUI.Forms
{
    partial class FormLink
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
            this.bevel = new Bevel.BevelControl();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.panelAnchor = new System.Windows.Forms.Panel();
            this.EditAnchor = new DevNotepad.UI.Controls.LabelEdit();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.EditTitle = new DevNotepad.UI.Controls.LabelEdit();
            this.panelUrl = new System.Windows.Forms.Panel();
            this.EditUrl = new DevNotepad.UI.Controls.LabelEdit();
            this.panelWrapper.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelAnchor.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.panelUrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelWrapper
            // 
            this.panelWrapper.Controls.Add(this.bevel);
            this.panelWrapper.Controls.Add(this.panelButtons);
            this.panelWrapper.Controls.Add(this.panelAnchor);
            this.panelWrapper.Controls.Add(this.panelTitle);
            this.panelWrapper.Controls.Add(this.panelUrl);
            this.panelWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWrapper.Location = new System.Drawing.Point(0, 0);
            this.panelWrapper.Name = "panelWrapper";
            this.panelWrapper.Padding = new System.Windows.Forms.Padding(7);
            this.panelWrapper.Size = new System.Drawing.Size(471, 181);
            this.panelWrapper.TabIndex = 0;
            // 
            // bevel
            // 
            this.bevel.BevelStyle = Bevel.BevelStyle.Lowered;
            this.bevel.BevelType = Bevel.BevelType.TopLine;
            this.bevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.bevel.HighlightColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bevel.Location = new System.Drawing.Point(7, 127);
            this.bevel.Name = "bevel";
            this.bevel.Padding = new System.Windows.Forms.Padding(10);
            this.bevel.ShadowColor = System.Drawing.SystemColors.ButtonShadow;
            this.bevel.Size = new System.Drawing.Size(457, 2);
            this.bevel.TabIndex = 5;
            this.bevel.TabStop = false;
            this.bevel.Text = "bevelControl1";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.ButtonCancel);
            this.panelButtons.Controls.Add(this.ButtonOk);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(7, 127);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.panelButtons.Size = new System.Drawing.Size(457, 47);
            this.panelButtons.TabIndex = 8;
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ButtonCancel.Location = new System.Drawing.Point(5, 10);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 32);
            this.ButtonCancel.TabIndex = 2;
            this.ButtonCancel.Text = "&Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // ButtonOk
            // 
            this.ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.ButtonOk.Location = new System.Drawing.Point(377, 10);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 32);
            this.ButtonOk.TabIndex = 1;
            this.ButtonOk.Text = "&OK";
            this.ButtonOk.UseVisualStyleBackColor = true;
            // 
            // panelAnchor
            // 
            this.panelAnchor.Controls.Add(this.EditAnchor);
            this.panelAnchor.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAnchor.Location = new System.Drawing.Point(7, 87);
            this.panelAnchor.Name = "panelAnchor";
            this.panelAnchor.Padding = new System.Windows.Forms.Padding(8);
            this.panelAnchor.Size = new System.Drawing.Size(457, 40);
            this.panelAnchor.TabIndex = 7;
            // 
            // EditAnchor
            // 
            this.EditAnchor.Caption = "&Anchor Text:";
            this.EditAnchor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditAnchor.Location = new System.Drawing.Point(8, 8);
            this.EditAnchor.MinimumSize = new System.Drawing.Size(0, 25);
            this.EditAnchor.Name = "EditAnchor";
            this.EditAnchor.Size = new System.Drawing.Size(441, 25);
            this.EditAnchor.TabIndex = 0;
            // 
            // panelTitle
            // 
            this.panelTitle.Controls.Add(this.EditTitle);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(7, 47);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Padding = new System.Windows.Forms.Padding(8);
            this.panelTitle.Size = new System.Drawing.Size(457, 40);
            this.panelTitle.TabIndex = 6;
            // 
            // EditTitle
            // 
            this.EditTitle.Caption = "&Title (Optional):";
            this.EditTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditTitle.Location = new System.Drawing.Point(8, 8);
            this.EditTitle.MinimumSize = new System.Drawing.Size(0, 25);
            this.EditTitle.Name = "EditTitle";
            this.EditTitle.Size = new System.Drawing.Size(441, 25);
            this.EditTitle.TabIndex = 0;
            // 
            // panelUrl
            // 
            this.panelUrl.Controls.Add(this.EditUrl);
            this.panelUrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUrl.Location = new System.Drawing.Point(7, 7);
            this.panelUrl.Name = "panelUrl";
            this.panelUrl.Padding = new System.Windows.Forms.Padding(8);
            this.panelUrl.Size = new System.Drawing.Size(457, 40);
            this.panelUrl.TabIndex = 4;
            // 
            // EditUrl
            // 
            this.EditUrl.Caption = "&Url:";
            this.EditUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditUrl.Location = new System.Drawing.Point(8, 8);
            this.EditUrl.MinimumSize = new System.Drawing.Size(0, 25);
            this.EditUrl.Name = "EditUrl";
            this.EditUrl.Size = new System.Drawing.Size(441, 25);
            this.EditUrl.TabIndex = 2;
            // 
            // FormLink
            // 
            this.AcceptButton = this.ButtonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(471, 181);
            this.Controls.Add(this.panelWrapper);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLink";
            this.Text = "Insert a Link";
            this.Load += new System.EventHandler(this.FormLink_Load);
            this.panelWrapper.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.panelAnchor.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.panelUrl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelWrapper;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Panel panelAnchor;
        private DevNotepad.UI.Controls.LabelEdit EditAnchor;
        private System.Windows.Forms.Panel panelTitle;
        private DevNotepad.UI.Controls.LabelEdit EditTitle;
        private System.Windows.Forms.Panel panelUrl;
        private DevNotepad.UI.Controls.LabelEdit EditUrl;
        private Bevel.BevelControl bevel;


    }
}