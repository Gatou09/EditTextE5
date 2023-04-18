using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EditTextE5.Controls
{
    internal class MainMenuStrip : MenuStrip
    {
        private const string NAME = "MainMenuStrip";

        public MainMenuStrip()
        {
            Name = NAME;
            Dock = DockStyle.Top;

            FileDropDownMenu();
            EditDropDownMenu();
        }

        public void FileDropDownMenu()
        {
            var fileDropDownMenu = new ToolStripMenuItem("Fichier");

            var newMenu = new ToolStripMenuItem("Nouveau", null, null, Keys.Control | Keys.N);
            var openMenu = new ToolStripMenuItem("Ouvrir...", null, null, Keys.Control | Keys.O);
            var saveMenu = new ToolStripMenuItem("Enregistrer", null, null, Keys.Control | Keys.S);
            var saveAsMenu = new ToolStripMenuItem("Enregistrer sous...", null, null, Keys.Control | Keys.Shift | Keys.N);
            var quitMenu = new ToolStripMenuItem("Quitter", null, null, Keys.Alt | Keys.F4);

            fileDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { newMenu, openMenu, saveMenu, saveAsMenu, quitMenu });

            Items.Add(fileDropDownMenu);
        }

        public void EditDropDownMenu()
        {
            var editDropDownMenu = new ToolStripMenuItem("Edition");

            var cancelMenu = new ToolStripMenuItem("Annuler", null, null, Keys.Control | Keys.Z);
            var restoreMenu = new ToolStripMenuItem("Restaurer", null, null, Keys.Control | Keys.Y);

            editDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { cancelMenu, restoreMenu });

            Items.Add(editDropDownMenu);
        }
    }
}
