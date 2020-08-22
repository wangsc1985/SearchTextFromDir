using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchTextFromDir
{
    public partial class Form1 : Form
    {
        List<string> fileInfos = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            GetFilesCount(new DirectoryInfo(Directory.GetCurrentDirectory()));
            foreach (var a in fileInfos)
            {
                var stream = File.OpenText(a);
                var content = stream.ReadToEnd();
                if (content.Contains(textBox1.Text))
                {
                    listBox1.Items.Add(a);
                }
            }

        }

        public void GetFilesCount(DirectoryInfo dirInfo)
        {
            foreach (var file in dirInfo.GetFiles())
            {
                fileInfos.Add(file.FullName);
            }
            foreach (DirectoryInfo subdir in dirInfo.GetDirectories())
            {
                GetFilesCount(subdir);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            var path = listBox1.SelectedItem.ToString();


            Process process = new Process();
            process.StartInfo.FileName = @"D:\Program Files\Notepad++\notepad++.exe";
            process.StartInfo.Arguments = path;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.CreateNoWindow = false;
            process.Start();
            process.WaitForExit();
            process.Close();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.textBox1.SelectAll();
        }
    }
}
