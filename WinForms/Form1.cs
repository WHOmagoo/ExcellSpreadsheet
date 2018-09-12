using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace WinForms
{
    public partial class Form1 : Form
    {
        
        private ArrayList texts = new ArrayList();
        private int textsCount = 0;
        private TextBox textView = new TextBox();
        private string text = "";
        
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {

            textView.Size = new Size(300, 300);
            textView.Location = new Point(0,0);
            textView.Multiline = true;
            textView.WordWrap = true;
            textView.Enabled = false;
            
            Random r = new Random();
            
            int[] nums = new int[10000];
            
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = r.Next(0,20000);
            }
            
            Dictionary<int, int> dictionary = new Dictionary<int, int>(20000);
            

            DateTime startTime = DateTime.UtcNow;
            
            int duplicates = 0;
            foreach (var i in nums)
            {                
                if (dictionary.ContainsKey(i))
                {
                    duplicates++;
                }
                else
                {
                    dictionary.Add(i,i);
                }
            }

            DateTime endTime = DateTime.UtcNow;

            String dictionaryResult = "Dictionary:" + System.Environment.NewLine + "Duplicates: " + duplicates + " | Time: " + (endTime - startTime);
            
            Console.WriteLine(dictionaryResult);
            Console.WriteLine(endTime - startTime);

            startTime = DateTime.UtcNow;

            duplicates = 0;

            for (int i = 0; i < nums.Length;i++)
            {
                if (Program.listContains(nums, i + 1, nums[i]))
                {
                    duplicates++;
                }
            }

            endTime = DateTime.UtcNow;

            String oNResult = "O(1) space complexity:" + System.Environment.NewLine + "Duplicates: " + duplicates + " | Time: " + (endTime - startTime);
            
            Console.WriteLine(oNResult);


            startTime = DateTime.UtcNow;

            duplicates = 0;

            Array.Sort(nums);

            int prevNum = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                if (prevNum == nums[i])
                {
                    duplicates++;
                }

                prevNum = nums[i];
            }

            endTime = DateTime.UtcNow;

            String sortedResult = "Sorted Array:" + System.Environment.NewLine +
                                  "Duplicates: " + duplicates + " | Time: " + (endTime - startTime);
            
            Console.WriteLine(sortedResult);


            addText(dictionaryResult);
            addText(oNResult);
            addText(sortedResult);

            textView.Text = text;
            Console.WriteLine(text);
            
            Controls.Add(textView);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Form1_Load");
        }

        public void addText(String text)
        {
            this.text += text + System.Environment.NewLine + System.Environment.NewLine;
        }
    }
}