using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Form1 : Form
    {
        
        private ArrayList texts = new ArrayList();
        private int textsCount = 0;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
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
                try
                {
                    dictionary.Add(i, i);
                }
                catch (ArgumentException e)
                {
                    duplicates++;
                }
            }

            DateTime endTime = DateTime.UtcNow;

            String dictionaryResult = "There are " + duplicates + " duplicates, it took " +
                                      (endTime - startTime) + " for dictionary to finish";
            
            Console.WriteLine(dictionaryResult);

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

            String oNResult = "There are " + duplicates + " duplicates, it took " + (endTime - startTime) +
                              " for O(1) space complexity to finish";
            
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

            String sortedResult = "There are " + duplicates + " duplicates, it took " + (endTime - startTime) +
                                  " for sorted array";
            
            Console.WriteLine(sortedResult);


            Form1 form = new Form1();


            form.addText(sortedResult);
            form.addText(oNResult);
            form.addText(sortedResult);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Form1_Load");
        }

        public void addText(String text)
        {
            TextBox textView = new TextBox();
            
            textView.Text = text;
            textView.Location = new Point(20, textsCount * 60);
            textView.Size = new Size(500, 60);
            text.Insert(textsCount, text);
            textsCount++;
            this.Controls.Add(textView);
        }
    }
}