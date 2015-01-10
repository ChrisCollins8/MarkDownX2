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
            LoadDockSetup();
            SetupParserList();
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
    }
}

