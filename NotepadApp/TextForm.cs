using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace NotepadApp
{
    
    public class TextForm : Form
    {

        private TextBox textView = new TextBox();
        private String filePath;
        
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
            file.MenuItems.Add("Open", OpenClickEventHandler);
            file.MenuItems.Add("Save", SaveClickEventHandler);
            file.MenuItems.Add("Save as", SaveAsClickEventHandler);
            file.MenuItems.Add("Load 50 Fibonacci", FiftyFibonacciClickEventHandler);
            file.MenuItems.Add("Load 100 Fibonacci", OneHundredFibonacciClickEventHandler);

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

        private void OneHundredFibonacciClickEventHandler(object sender, EventArgs e)
        {
            FibonacciTextReader ftr = new FibonacciTextReader(100);
            LoadText(ftr);
        }

        private void FiftyFibonacciClickEventHandler(object sender, EventArgs e)
        {
            FibonacciTextReader ftr = new FibonacciTextReader(50);
            LoadText(ftr);
        }

        private void SaveAsClickEventHandler(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select a location to save the file";

            DialogResult result = saveFileDialog.ShowDialog();

            switch (result)
            {
                    case DialogResult.OK:
                        saveFileDialog.OpenFile().Close();
                        
                        filePath = saveFileDialog.FileName;
                        SaveClickEventHandler(sender, e);
                        break;
            }
            
        }

        private void SaveClickEventHandler(object sender, EventArgs e)
        {
            if (filePath == null)
            {
                SaveAsClickEventHandler(sender, e);
            }
            else
            {
                try
                {
                    StreamWriter sw = new StreamWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write));

                    sw.Write(textView.Text);
                    sw.Close();
                }
                catch (Exception err)
                {
                    Console.WriteLine(filePath);
                    Console.WriteLine(err.Message);
                }

            }
        }

        private void WordwrapClickEvent(object sender, EventArgs e)
        {
            textView.WordWrap = !textView.WordWrap;
        }

        private void OpenClickEventHandler(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a file to open";
            
            // Call the ShowDialog method to show the dialog box.
            DialogResult result = openFileDialog.ShowDialog();

            switch (result)
            {
                case DialogResult.OK:


                    filePath = openFileDialog.FileName;
                    Stream stream = openFileDialog.OpenFile();
                    
                    LoadText(new StreamReader(stream));
                    
                    stream.Close();
                    
                    break;
            }
            Console.WriteLine(filePath);
            
        }

        public void LoadText(TextReader tr)
        {
            textView.Text = tr.ReadToEnd();
        }
        
    }
}