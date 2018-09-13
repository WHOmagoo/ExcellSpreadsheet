using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Form1 : Form
    {
        private TextBox textView = new TextBox();
        private string text = "";
        
        public Form1()
        {
            Text = "Hugh McGough - 11479833";
            
            InitializeComponent();
        }

        private void InitializeTextBox()
        {
            textView.Multiline = true;
            textView.WordWrap = true;
            textView.Dock = DockStyle.Fill;
            textView.ReadOnly = true; 
            textView.Text = text;
            textView.SelectionStart = 0;
            
            Controls.Add(textView);
        }
        
        private void InitializeComponent()
        {
            //Randomize strings
            Random r = new Random();
            int[] nums = new int[10000];

            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = r.Next(0,20000);
            }


            //Temp variable initializer

            Stopwatch sw = new Stopwatch();
            TimeSpan timeTaken;
            
            //Start counting duplicates by using a dictionary

            Dictionary<int, int> dictionary = new Dictionary<int, int>();

            sw.Start();
            
            int duplicates = 0;
            foreach (var i in nums)
            {
                if (dictionary.ContainsKey(i))
                {
                    duplicates++;
                }
                else
                {
                    dictionary.Add(i, i);
                }
            }

            sw.Stop();
            timeTaken = sw.Elapsed; sw.Reset();

            String dictionaryResult = "Dictionary:" + System.Environment.NewLine + "Duplicates: " + duplicates + " | Time: " + timeTaken + System.Environment.NewLine +
                                      "This is O(n) because we only go through the list once and the time taken to look up if a dictionary contains a key is O(1)";
            
            //End duplicate counting with dictionary
           
            
            //Start counting duplicates by using an O(1) space efficiency algorithm

            sw.Start();

            duplicates = 0;

            for (int i = 0; i < nums.Length;i++)
            {
                if (listContains(nums, i + 1, nums[i]))
                {
                    duplicates++;
                }
            }

            sw.Stop();
            timeTaken = sw.Elapsed; sw.Reset();

            String oNResult = "O(1) space complexity:" + System.Environment.NewLine + "Duplicates: " + duplicates + " | Time: " + timeTaken;
            
            
            //End counting duplicates by using an O(1) space efficiency algorithm


            
            //Begin counting duplicates by sorting and counting

            duplicates = 0;
            
            Array.Sort(nums);
            
            sw.Start();

            int prevNum = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                if (prevNum == nums[i])
                {
                    duplicates++;
                }

                prevNum = nums[i];
            }

            sw.Stop();
            timeTaken = sw.Elapsed; sw.Reset();

            String sortedResult = "Sorted Array:" + System.Environment.NewLine + "Duplicates: " + duplicates + " | Time: " + timeTaken;



            text = dictionaryResult + System.Environment.NewLine + System.Environment.NewLine + oNResult + System.Environment.NewLine + System.Environment.NewLine + sortedResult;
            
            InitializeTextBox();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Form1_Load");
        }
        
        private bool listContains(int[] list, int startingIndex, int number)
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