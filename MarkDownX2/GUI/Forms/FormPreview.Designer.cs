namespace MarkDownX2.GUI.Forms
{
    partial class FormPreview
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
            this.components = new System.ComponentModel.Container();
            Awesomium.Windows.Forms.DataPakSourceProvider dataPakSourceProvider1 = new Awesomium.Windows.Forms.DataPakSourceProvider();
            Awesomium.Core.WebPreferences webPreferences1 = new Awesomium.Core.WebPreferences(true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreview));
            this.browser = new Awesomium.Windows.Forms.WebControl(this.components);
            this.PreviewMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.WebSesession = new Awesomium.Windows.Forms.WebSessionProvider(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // browser
            // 
            this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browser.Location = new System.Drawing.Point(0, 0);
            this.browser.Size = new System.Drawing.Size(284, 262);
            this.browser.TabIndex = 0;
            this.browser.ShowJavascriptDialog += new Awesomium.Core.JavascriptDialogEventHandler(this.Awesomium_Windows_Forms_WebControl_ShowJavascriptDialog);
            this.browser.ShowContextMenu += new Awesomium.Core.ShowContextMenuEventHandler(this.Awesomium_Windows_Forms_WebControl_ShowContextMenu);
            this.browser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Awesomium_Windows_Forms_WebControl_MouseDown);
            // 
            // PreviewMenu
            // 
            this.PreviewMenu.Name = "PreviewMenu";
            this.PreviewMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // WebSesession
            // 
            this.WebSesession.DataSources.AddRange(new Awesomium.Windows.Forms.DataSourceProvider[] {
            dataPakSourceProvider1});
            webPreferences1.FileAccessFromFileURL = true;
            webPreferences1.UniversalAccessFromFileURL = true;
            webPreferences1.WebSecurity = false;
            this.WebSesession.Preferences = webPreferences1;
            this.WebSesession.Views.Add(this.browser);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(73, 126);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // FormPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.browser);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPreview";
            this.Text = "Preview";
            this.Load += new System.EventHandler(this.FormPreview_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormPreview_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Awesomium.Windows.Forms.WebControl browser;
        private System.Windows.Forms.ContextMenuStrip PreviewMenu;
        private Awesomium.Windows.Forms.WebSessionProvider WebSesession;
        private System.Windows.Forms.Button button1;

    }
}