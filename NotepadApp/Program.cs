using System;
using System.Windows.Forms;

namespace NotepadApp
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new TextForm());
        }
    }
}