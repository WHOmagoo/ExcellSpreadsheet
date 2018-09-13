using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForms
{
    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());

        }
    }
}