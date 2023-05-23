using EditTextE5.Objects;
using EditTextE5.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace EditTextE5.Controls
{
    public class MainMenuStrip : MenuStrip
    {
        private const string NAME = "MainMenuStrip";

        private MainForm _form;
        private FontDialog _fontDialog;
        private OpenFileDialog _openFileDialog;
        private SaveFileDialog _saveFileDialog;
        public MainMenuStrip()
        {
            Name = NAME;
            Dock = DockStyle.Top;

            _fontDialog = new FontDialog();
            _openFileDialog = new OpenFileDialog();
            _saveFileDialog = new SaveFileDialog();

            FileDropDownMenu();
            EditDropDownMenu();

            HandleCreated += (s, e) =>
            {
                _form = FindForm() as MainForm;
            };
        }

        public void FileDropDownMenu()
        {
            var fileDropDownMenu = new ToolStripMenuItem("Fichier");

            var newFile = new ToolStripMenuItem("Nouveau", null, null, Keys.Control | Keys.N);
            var open = new ToolStripMenuItem("Ouvrir...", null, null, Keys.Control | Keys.O);
            var save = new ToolStripMenuItem("Enregistrer", null, null, Keys.Control | Keys.S);
            var saveAs = new ToolStripMenuItem("Enregistrer sous...", null, null, Keys.Control | Keys.Shift | Keys.S);
            var quit = new ToolStripMenuItem("Quitter", null, null, Keys.Alt | Keys.F4);

            newFile.Click += (s, e) =>
            {
                var tabControl = _form.MainTabControl;
                var tabCount = tabControl.TabCount;

                var fileName = $"Sans titre {tabCount + 1}";
                var file = new TextFile(fileName);
                var rtb = new CustomRichTextBox();

                tabControl.TabPages.Add(file.SafeFileName);

                var newTabPage = tabControl.TabPages[tabCount];

                newTabPage.Controls.Add(rtb);

                _form.Session.Files.Add(file);

                tabControl.SelectedTab = newTabPage;

                _form.CurrentFile = file;
                _form.CurrentRtb = rtb;
            };

            open.Click += async (s, e) =>
            {
                if (_openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var tabControl = _form.MainTabControl;
                    var tabCount = tabControl.TabCount;

                    var file = new TextFile(_openFileDialog.FileName);

                    var rtb = new CustomRichTextBox();

                    _form.Text = $"{file.FileName} - Notepad.NET";

                    using (StreamReader reader = new StreamReader(file.FileName))
                    {
                        file.Contents = await reader.ReadToEndAsync();
                    }

                    rtb.Text = file.Contents;

                    tabControl.TabPages.Add(file.SafeFileName);
                    tabControl.TabPages[tabCount].Controls.Add(rtb);

                    _form.Session.Files.Add(file);
                    _form.CurrentRtb = rtb;
                    _form.CurrentFile = file;
                    tabControl.SelectedTab = tabControl.TabPages[tabCount];
                }
            };

            save.Click += async (s, e) =>
            {
                var currentFile = _form.CurrentFile;
                var currentRtbText = _form.CurrentRtb.Text;

                if (currentFile.Contents != currentRtbText)
                {
                    if (File.Exists(currentFile.FileName))
                    {
                        using (StreamWriter writer = File.CreateText(currentFile.FileName))
                        {
                            await writer.WriteAsync(currentFile.Contents);
                        }
                        currentFile.Contents = currentRtbText;
                        _form.Text = currentFile.FileName;
                        _form.MainTabControl.SelectedTab.Text = currentFile.FileName;
                    }
                    else
                    {
                        saveAs.PerformClick();
                    }

                }
            };

            saveAs.Click += async (s, e) =>
            {
                if (_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var newFileName = _saveFileDialog.FileName;
                    var alredyExists = false;

                    foreach (var file in _form.Session.Files)
                    {
                        if (file.FileName == newFileName)
                        {
                            MessageBox.Show("Ce fichier est déjà ouvert dans EditTextE5", "ERREUR",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            alredyExists = true;
                            break;
                        }
                    }

                    if (!alredyExists)
                    {
                        var file = new TextFile(newFileName) { Contents = _form.CurrentRtb.Text };

                        var oldFile = _form.Session.Files.Where(x => x.FileName == _form.CurrentFile.FileName).First();

                        _form.Session.Files.Replace(oldFile, file);

                        using (StreamWriter writer = File.CreateText(file.FileName))
                        {
                            await writer.WriteAsync(file.Contents);
                        }

                        _form.MainTabControl.SelectedTab.Text = file.SafeFileName;
                        _form.Text = file.FileName;
                        _form.CurrentFile = file;
                    }            
                }
            };

            fileDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { newFile, open, save, saveAs, quit });

            Items.Add(fileDropDownMenu);
        }

        public void EditDropDownMenu()
        {
            var editDropDown = new ToolStripMenuItem("Edition");

            var undo = new ToolStripMenuItem("Annuler", null, null, Keys.Control | Keys.Z);
            var redo = new ToolStripMenuItem("Restaurer", null, null, Keys.Control | Keys.Y);

            undo.Click += (s, e) => { if (_form.CurrentRtb.CanUndo) _form.CurrentRtb.Undo(); };
            redo.Click += (s, e) => { if (_form.CurrentRtb.CanRedo) _form.CurrentRtb.Redo(); };

            editDropDown.DropDownItems.AddRange(new ToolStripItem[] { undo, redo });

            Items.Add(editDropDown);
        }

    }
}
