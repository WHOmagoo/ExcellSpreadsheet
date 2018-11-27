using System;
using System.Windows.Forms;

namespace Spreadsheet
{
    public class AboutForm : Form
    {
        public AboutForm()
        {
            Text = "About Simple Spreadsheet";
            
            RichTextBox box = new RichTextBox();
            box.Multiline = true;
            box.DetectUrls = true;
            box.Text = "Simple Spreadsheet";
            box.ReadOnly = true;
            box.AppendText(Environment.NewLine);
            box.AppendText("v 5.0");
            box.AppendText(Environment.NewLine);
            box.AppendText("Hugh McGough. hugh.mcgough@wsu.edu");
            box.AppendText(Environment.NewLine);
            box.AppendText(Convert.ToChar(169) + " 2018 Hugh McGough");
            box.AppendText(Environment.NewLine);
            box.AppendText("Licence: https://creativecommons.org/licenses/by-nc-sa/4.0/");
            box.Dock = DockStyle.Fill;
            box.LinkClicked += richTextBox1_LinkClicked;
            Controls.Add(box);
        }
        
        public System.Diagnostics.Process p = new System.Diagnostics.Process();  

        private void richTextBox1_LinkClicked(object sender,   
            System.Windows.Forms.LinkClickedEventArgs e)  
        {  
            // Call Process.Start method to open a browser  
            // with link text as URL.  
            p = System.Diagnostics.Process.Start("IExplore.exe", e.LinkText);  
        }
    }
}