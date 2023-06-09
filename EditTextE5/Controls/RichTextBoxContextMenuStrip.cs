﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditTextE5.Controls
{
    public class RichTextBoxContextMenuStrip : ContextMenuStrip
    {
        private const string NAME = "RtbContextMenuStrip";

        private RichTextBox _richtextBox;

        public RichTextBoxContextMenuStrip(RichTextBox richTextBox)
        {
            _richtextBox = richTextBox;

            var cut = new ToolStripMenuItem("Couper");
            var copy = new ToolStripMenuItem("Copier");
            var paste = new ToolStripMenuItem("Coller");
            var selectAll = new ToolStripMenuItem("Sélectionner tout");

            cut.Click += (s, e) => _richtextBox.Cut();
            copy.Click += (s, e) => _richtextBox.Copy();
            paste.Click += (s, e) => _richtextBox.Paste();
            selectAll.Click += (s, e) => _richtextBox.SelectAll();

            Items.AddRange(new ToolStripItem[] { cut, copy, paste, selectAll });
        }
    }
}
