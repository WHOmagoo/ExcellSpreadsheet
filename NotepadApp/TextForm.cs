using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace NotepadApp
{
    
    public class TextForm : Form
    {

        private TextBox textView = new TextBox();
        private Stream stream = null;
        
        public TextForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Text Editor";

            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
            Size = new Size(screenBounds.Width / 2, screenBounds.Height / 2);
            CenterToScreen();

            Menu = new MainMenu();
            MenuItem file = new MenuItem("File");
            file.MenuItems.Add("Open", OpenClickEvent);
            file.MenuItems.Add("Save", SaveClickEvent);
            file.MenuItems.Add("Save as", SaveAsClickEvent);

            Menu.MenuItems.Add(file);
            
            MenuItem view = new MenuItem("View");

            view.MenuItems.Add("Wordwrap", WordwrapClickEvent);

            Menu.MenuItems.Add(view);
            
            
            
            
            
            //Begin initializing the textView
            textView.Multiline = true;
            textView.WordWrap = false;
            textView.ScrollBars = ScrollBars.Both;
            textView.Dock = DockStyle.Fill;
            textView.SelectionStart = 0;
            
            Controls.Add(textView);
        }

        private void SaveAsClickEvent(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select a location to save the file";

            DialogResult result = saveFileDialog.ShowDialog();

            switch (result)
            {
                    case DialogResult.OK:
                        stream = saveFileDialog.OpenFile();
                        SaveClickEvent(sender, e);
                        break;
            }
            
        }

        private void SaveClickEvent(object sender, EventArgs e)
        {
            if (stream == null)
            {
                SaveAsClickEvent(sender, e);
            }
            else
            {
                StreamWriter sw = new StreamWriter(stream);
                sw.Write(textView.Text);
                sw.Close();
                
            }
        }

        private void WordwrapClickEvent(object sender, EventArgs e)
        {
            textView.WordWrap = !textView.WordWrap;
        }

        private void OpenClickEvent(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a file to open";
            
            // Call the ShowDialog method to show the dialog box.
            DialogResult result = openFileDialog.ShowDialog();

            switch (result)
            {
                case DialogResult.OK:
                    stream = openFileDialog.OpenFile();
                    
                    LoadText(new StreamReader(stream));
                    
                    break;
            }
            
            
            
            Console.WriteLine("Finished");
        }

        public void LoadText(TextReader tr)
        {
            textView.Text = tr.ReadToEnd();
        }
        
    }
}