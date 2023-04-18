using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditTextE5.Controls
{
    public class MainTabControl : TabControl
    {
        private const string TAB_CONTROL_NAME = "MainTabControl";
        private TabControlContextMenuStrip _contextMenuStrip;

        public MainTabControl()
        {
            _contextMenuStrip = new TabControlContextMenuStrip();
            Name = TAB_CONTROL_NAME; 
            ContextMenuStrip = _contextMenuStrip;
            Dock = DockStyle.Fill;
        }
    }
}
