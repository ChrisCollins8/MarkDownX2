using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarkDownX2.GUI.Colors;
using MarkDownX2.Helpers;
using MarkDownX2.ParserFramework;
using WeifenLuo.WinFormsUI.Docking;
using MarkDownX2.GUI.UserControls;
using System.Threading;

namespace MarkDownX2.GUI.Forms
{
    public partial class FormMain : Form
    {

        private double dockPaneRightWidth = 0;
        private double dockPaneBottomHeight = 0;

        public FormMain()
        {
            InitializeComponent();
            ToolStripManager.Renderer = new MarkDownX2Renderer();
            StartUpdateCheck();
            LoadDockSetup();
            SetupParserList();
            ReadSettings();
        }

        private void ReadSettings()
        {
            displayLineNumbersToolStripMenuItem.Checked = GlobalSettings.Settings.DisplayLineNumbers;
            displayFormattingMarksToolStripMenuItem.Checked = GlobalSettings.Settings.DisplayFormattingMarks;
            syntaxHighlightingToolStripMenuItem.Checked = GlobalSettings.Settings.SyntaxHighlighting;
            fullScreenToolStripMenuItem.Checked = GlobalSettings.Settings.FullScreen;
            wordWrapToolStripMenuItem.Checked = GlobalSettings.Settings.WordWrap;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            DocumentsHelper.NewDocument(this);
            
        }

        private void SetupParserList()
        {

            foreach (IMarkdownParser parser in PreviewHelper.Parsers)
            {
                //SelectParser.Add(parser.Name);
                ToolStripMenuItem item = new ToolStripMenuItem();

                item.Text = parser.Name;
                item.Tag = parser;
                item.Click += item_Click;
                SelectParser.DropDownItems.Add(item);
            }
            SelectParser.Text = PreviewHelper.CurrentParser.Name;
        }

        void item_Click(object sender, EventArgs e)
        {
            try
            {
                IMarkdownParser parser = (IMarkdownParser)(((ToolStripMenuItem)sender).Tag);
                PreviewHelper.CurrentParser = parser;
                SelectParser.Text = parser.Name;
            }
            catch { }
        }

        private void LoadDockSetup()
        {
            PreviewHelper.PreviewForm.Show(DockingPanel, DockState.DockRight);
            DockingPanel.DockRightPortion = (this.ClientSize.Width / 2);
            dockPaneRightWidth = (DockingPanel.DockRightPortion / this.ClientSize.Width);
            dockPaneBottomHeight = (DockingPanel.DockBottomPortion / this.ClientSize.Height);         
            //
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocumentsHelper.NewDocument(this);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocumentsHelper.Open(this);
        }

        private void FormMain_ResizeBegin(object sender, EventArgs e)
        {
               
        }

        private void FormMain_ResizeEnd(object sender, EventArgs e)
        {
            
            
        }


        private void DockingPanel_Resize(object sender, EventArgs e)
        {
            DockingPanel.DockRightPortion = dockPaneRightWidth * this.ClientSize.Width;
            DockingPanel.DockBottomPortion = dockPaneBottomHeight * this.ClientSize.Height;

            dockPaneRightWidth = (DockingPanel.DockRightPortion / this.ClientSize.Width);
            dockPaneBottomHeight = (DockingPanel.DockBottomPortion / this.ClientSize.Height);         
        }

        private void DockingPanel_DockChanged(object sender, EventArgs e)
        {
            dockPaneRightWidth = (DockingPanel.DockRightPortion / this.ClientSize.Width);
            dockPaneBottomHeight = (DockingPanel.DockBottomPortion / this.ClientSize.Height);    
        }

        private void DockingPanel_SizeChanged(object sender, EventArgs e)
        {
            dockPaneRightWidth = (DockingPanel.DockRightPortion / this.ClientSize.Width);
            dockPaneBottomHeight = (DockingPanel.DockBottomPortion / this.ClientSize.Height);   
        }

        public void SetStatus(int Length, int Lines, int CurLine, int CurCol, int SelStart, int SelEnd, int Words)
        {
            labelDetails.Text = String.Format("Length: {0}   Lines: {1}", Length, Lines);
            labelPosition.Text = String.Format("Line: {0}   Column: {1}   Select: {2}-{3}   Words: {4}", CurLine, CurCol, SelStart, SelEnd, Words);
        }

        private void SelectParser_Click(object sender, EventArgs e)
        {

        }

        private void readmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocumentsHelper.OpenFile(this, PathHelper.ReadmeFile);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ToolbarMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ButtonNew_Click(object sender, EventArgs e)
        {
            DocumentsHelper.NewDocument(this);
        }

        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            DocumentsHelper.Open(this);
        }

        private void FormMain_MdiChildActivate_1(object sender, EventArgs e)
        {
            FormDocument child = ActiveMdiChild as FormDocument;

            if (child != null)
            {
                ToolStripManager.Merge(child.ToolbarMain, ToolbarMain);
                ButtonSaveAll.Enabled = true;
                child.ToolbarMain.Hide();
                child.FormClosing += delegate(object sender2, FormClosingEventArgs fe)
                {
                    ToolStripManager.RevertMerge(ToolbarMain, child.ToolbarMain);
                    
                };
                child.Deactivate += delegate(object sender2, EventArgs fe)
                {
                    ToolStripManager.RevertMerge(ToolbarMain, child.ToolbarMain);
                };
                child.FormClosed += delegate(object sender2, FormClosedEventArgs fe)
                {
                    if (DocumentsHelper.ActiveDocs == 1)
                    {
                        ButtonSaveAll.Enabled = false;
                    }
                };
            }
        }
        

        public void StartUpdateCheck()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (sender, args) =>
            {
                labelDetails.Text = "Checking for updates...";
                if (UpdateHelper.CheckForUpdate())
                {
                    Notify.BalloonTipTitle = "MarkDownX2 Update Available";
                    Notify.Text = "MarkDownX2 Update Available. Click to Download";
                    Notify.BalloonTipText = "There is an update available.\nClick here to download and install the latest version of MarkDownX2";
                    Notify.Click += Notify_Click;
                    Notify.BalloonTipClicked += Notify_BalloonTipClicked;
                    Notify.ShowBalloonTip(10000);
                }
                else
                {
                    Notify.Visible = false;
                }
            };
            worker.RunWorkerAsync();
        }

        void Notify_BalloonTipClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Downloading");
        }

        void Notify_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Downloading");
        }


        #region View Menu items

        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fullScreenToolStripMenuItem.Checked = !fullScreenToolStripMenuItem.Checked;
            GlobalSettings.Settings.FullScreen = fullScreenToolStripMenuItem.Checked;
        }

        
        private void displayLineNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            displayLineNumbersToolStripMenuItem.Checked = !displayLineNumbersToolStripMenuItem.Checked;
            GlobalSettings.Settings.DisplayLineNumbers = displayLineNumbersToolStripMenuItem.Checked;
            GlobalSettings.UpdateSettings();
        }

        private void livePreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            livePreviewToolStripMenuItem.Checked = !livePreviewToolStripMenuItem.Checked;
            
        }

        private void horizontalLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            horizontalLayoutToolStripMenuItem.Checked = !horizontalLayoutToolStripMenuItem.Checked;
        }

        private void displayFormattingMarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            displayFormattingMarksToolStripMenuItem.Checked = !displayFormattingMarksToolStripMenuItem.Checked;
            GlobalSettings.Settings.DisplayFormattingMarks = displayFormattingMarksToolStripMenuItem.Checked;
            GlobalSettings.UpdateSettings();
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wordWrapToolStripMenuItem.Checked = !wordWrapToolStripMenuItem.Checked;
            GlobalSettings.Settings.WordWrap = wordWrapToolStripMenuItem.Checked;
            GlobalSettings.UpdateSettings();
        }

        private void syntaxHighlightingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            syntaxHighlightingToolStripMenuItem.Checked = !syntaxHighlightingToolStripMenuItem.Checked;
            GlobalSettings.Settings.SyntaxHighlighting = syntaxHighlightingToolStripMenuItem.Checked;
            GlobalSettings.UpdateSettings();
        }

        private void showColumnGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showColumnGuideToolStripMenuItem.Checked = !showColumnGuideToolStripMenuItem.Checked;
            GlobalSettings.Settings.ShowColumnGuide = (showColumnGuideToolStripMenuItem.Checked ? 80 : 0);
            GlobalSettings.UpdateSettings();
        }

        private void toolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void statusbarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

    }
}


