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

        public static bool listContains(int[] list, int startingIndex, int number)
        {
            for (int i = startingIndex; i < list.Length; i++)
            {
                if (list[i] == number)
                {
                    return true;
                }
            }

            return false;
        }
    }
}