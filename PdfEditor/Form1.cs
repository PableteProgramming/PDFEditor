using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PdfEditor
{
    public partial class Form1 : Form
    {
        List<string> paths= new List<string>();
        List<string> names = new List<string>();
        string outputFilepath;
        public Form1()
        {
            InitializeComponent();
            if (!File.Exists("merge.exe"))
            {
                MessageBox.Show("merge.exe is needed and is not found ! Please put it in the same directory as the executable. You can always find it here: https://github.com/PableteProgramming/mergepdfPython","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Close();
            }
        }
        private bool spaceInString(string array)
        {
            foreach (char ss in array)
            {
                if (ss == ' ')
                {
                    return true;
                }
            }
            return false;
        }

        private List<string> MoveUp(List<string> ss, int index)
        {
            List<string> ssss = ss;
            string sss = ss[index];
            if (index > 0)
            {
                for (int i = 0; i < ss.Count; i++)
                {
                    if (i == index - 1)
                    {
                        string d = ss[i];
                        ssss[i] = sss;
                        ssss[i + 1] = d;
                    }
                }
            }
            return ssss;
        }

        private List<string> MoveDown(List<string> ss, int index)
        {
            List<string> ssss = ss;
            string sss = ss[index];
            if (index < ss.Count)
            {
                for (int i = 0; i < ss.Count; i++)
                {
                    if (i == index + 1)
                    {
                        string d = ss[i];
                        ssss[i] = sss;
                        ssss[i - 1] = d;
                    }
                }
            }
            return ssss;
        }

        private void Delete(List<string> s, int pos)
        {
            List<string> sss = new List<string>();
            s.RemoveAt(pos);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dialog dialog = new Dialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                paths.AddRange(dialog.path);
                names.AddRange(dialog.name);
                listBox1.Items.Clear();
                for(int i=0; i<names.Count(); i++)
                {
                    listBox1.Items.Add(names[i]);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteDialog dialog = new DeleteDialog();
            DialogResult result = dialog.ShowDialog();
            try
            {
                int s = listBox1.SelectedIndex;
                if (result == DialogResult.OK) //Delete
                {
                    Delete(names, s);
                    Delete(paths, s);
                }
                else if (result == DialogResult.Yes) //Move up
                {
                    names = MoveUp(names, s);
                    paths = MoveUp(paths, s);
                }
                if (result == DialogResult.No) //Move down
                {
                    names = MoveDown(names, s);
                    paths = MoveDown(paths, s);
                }
                listBox1.Items.Clear();
                for (int i = 0; i < names.Count; i++)
                {
                    listBox1.Items.Add(names[i]);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "pdf files(*.pdf)|*.pdf";
            dialog.InitialDirectory = ".";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string s = dialog.FileName;
                if (spaceInString(s))
                {
                    MessageBox.Show("File " + s + " not added because of spaces in it's path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    textBox1.Text = dialog.FileName;
                }
            }
        }
        private bool EndOfFileIs(string s, string e)
        {
            int j = 0;
            for (int i = s.Length - e.Length; i < s.Length; i++,j++)
            {
                if (s[i] != e[j])
                {
                    return false;
                }
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            outputFilepath = textBox1.Text;
            if (outputFilepath.Trim() != "")
            {
                if (!EndOfFileIs(outputFilepath, ".pdf"))
                {
                    outputFilepath += ".pdf";
                }
                string r = "";
                if (paths.Count != 0)
                {
                    foreach (string s in paths)
                    {
                        string s1 = s;
                        if (!EndOfFileIs(s1, ".pdf"))
                        {
                                
                            s1 += ".pdf";
                        }
                        if (File.Exists(s1))
                        {
                            r += s1 + "*";
                        }
                        else
                        {
                            MessageBox.Show("File " + s1 + " not found !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                            
                    }
                    if (r.Trim() != "")
                    {
                        r = r.Remove(r.Length - 1);
                        if (!File.Exists("merge.exe"))
                        {
                            MessageBox.Show("merge.exe is needed and is not found ! Please put it in the same directory as the executable. You can always find it here: https://github.com/PableteProgramming/mergepdfPython", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else {
                            System.Diagnostics.Process process = new System.Diagnostics.Process();
                            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                            startInfo.FileName = "cmd.exe";
                            startInfo.CreateNoWindow = true;
                            startInfo.Arguments = "/C merge.exe " + r + " " + outputFilepath;
                            process.StartInfo = startInfo;
                            process.Start();
                            process.WaitForExit();
                            MessageBox.Show("Pdfs merged !", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        
                    }
                    else {
                        MessageBox.Show("please select one or more input files !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("please select one or more input files !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill the output file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
