//Hugh McGough - 114798333

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

        private MenuItem wordWrap;
        
        public TextForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            //Initialize the form properties
            Text = "Untitled - Text Editor";

            //Sets the size to half the width and height of the screen for default
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
            Size = new Size(screenBounds.Width / 2, screenBounds.Height / 2);
            CenterToScreen();
            
            
            //Begin initializing the textView
            textView.Multiline = true;
            textView.WordWrap = false;
            textView.ScrollBars = ScrollBars.Both;
            textView.Dock = DockStyle.Fill;
            textView.SelectionStart = 0;
            textView.Text = "";

            filePath = null;
            
            //Initialize menu items
            Menu = new MainMenu();
            
            MenuItem file = new MenuItem("File");
            file.MenuItems.Add("Open", OpenClickEventHandler);
            file.MenuItems.Add("New", NewClickEventHandler);
            file.MenuItems.Add("Save", SaveClickEventHandler);
            file.MenuItems.Add("Save as", SaveAsClickEventHandler);
            file.MenuItems.Add("Load 50 Fibonacci", FiftyFibonacciClickEventHandler);
            file.MenuItems.Add("Load 100 Fibonacci", OneHundredFibonacciClickEventHandler);

            Menu.MenuItems.Add(file);
            
            
            MenuItem view = new MenuItem("View");
   
            wordWrap = new MenuItem("Word Wrap", WordwrapClickEvent);
            wordWrap.Checked = textView.WordWrap;
            view.MenuItems.Add(wordWrap);

            Menu.MenuItems.Add(view);
            
            Controls.Add(textView);
        }

        private void NewClickEventHandler(object sender, EventArgs e)
        {
            InitializeComponent();
        }

        private void OneHundredFibonacciClickEventHandler(object sender, EventArgs e)
        {
            FibonacciTextReader ftr = new FibonacciTextReader(100);
            Text = "100 Fibonacci Numbers - Text Editor";
            LoadText(ftr);
        }

        private void FiftyFibonacciClickEventHandler(object sender, EventArgs e)
        {
            FibonacciTextReader ftr = new FibonacciTextReader(50);
            Text = "50 Fibonacci Numbers - Text Editor";
            LoadText(ftr);
        }

        private void SaveAsClickEventHandler(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select a location to save the file";
            saveFileDialog.Filter = "Text Files (*.txt) | *.txt";
            
            DialogResult result = saveFileDialog.ShowDialog();

            switch (result)
            {
                    case DialogResult.OK:
                        Text = saveFileDialog.FileName + " - Text Editor";                        
                        filePath = saveFileDialog.FileName;
                        //Calls the SaveClickEventHandler to save the code, that way there isn't two ways to save a file to test
                        SaveClickEventHandler(sender, e);
                        break;
            }
            
        }

        private void SaveClickEventHandler(object sender, EventArgs e)
        {
            if (filePath == null)
            {
                //If the file was not loaded from a file, prompt user to save as a file instead
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
                    string message = filePath + Environment.NewLine + err.Message;
                    MessageBox.Show(this, "Error Saving File", message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void WordwrapClickEvent(object sender, EventArgs e)
        {
            textView.WordWrap = !textView.WordWrap;
            
            //Set the checked property of the wordWrap MenuItem, that way the user can tell if word wrap is on in case there are no items being wrapped
            wordWrap.Checked = textView.WordWrap;

        }

        private void OpenClickEventHandler(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a file to open";
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Text Files (*.txt) | *.txt|All files (*.*) | *.*";
            
            // Call the ShowDialog method to show the dialog box.
            DialogResult result = openFileDialog.ShowDialog();

            switch (result)
            {
                case DialogResult.OK:


                    try
                    {
                        filePath = openFileDialog.FileName;
                        Stream stream = openFileDialog.OpenFile();

                        Text = openFileDialog.FileName + " - Text Editor";
                        LoadText(new StreamReader(stream));

                        stream.Close();
                    }
                    catch (Exception err)
                    {
                        string message = filePath + Environment.NewLine + err.Message;
                        MessageBox.Show(this, "Error opening file", message, MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }

                    break;
            }
            Console.WriteLine(filePath);
            
        }

        private void LoadText(TextReader tr)
        {
            textView.Text = tr.ReadToEnd();
        }
        
    }
}