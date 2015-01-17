using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkDownX2.GUI.Colors
{
    public class MarkDownX2Renderer : ToolStripProfessionalRenderer
    {
        public MarkDownX2Renderer()
            : base(new MarkDownX2Colors())
        {
            this.RoundedEdges = false;
        }

    }
}
