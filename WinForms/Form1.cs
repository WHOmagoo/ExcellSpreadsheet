using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            var button1 = new Button();
            button1.Size = new Size(40, 40);
            button1.Location = new Point(30, 30);
            button1.Text = "Click me";
            this.Controls.Add(button1);
            button1.Click += new EventHandler(button1_Click);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World");
        }
    }
}