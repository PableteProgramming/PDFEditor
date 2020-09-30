using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }

        private List<string> MoveUp(List<string> ss, string sss)
        {
            List<string> ssss = ss;
            int index = ss.IndexOf(sss);
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

        private List<string> MoveDown(List<string> ss, string sss)
        {
            List<string> ssss = ss;
            int index = ss.IndexOf(sss);
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

        private void Delete(List<string> s, string ss)
        {
            List<string> sss = new List<string>();
            int index = s.IndexOf(ss);
            s.RemoveAt(index);
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
                string s = names[listBox1.SelectedIndex];
                string ss = paths[listBox1.SelectedIndex];
                if (result == DialogResult.OK) //Delete
                {
                    Delete(names, s);
                    Delete(paths, ss);
                }
                else if (result == DialogResult.Yes) //Move up
                {
                    names = MoveUp(names, s);
                    paths = MoveUp(paths, ss);
                }
                if (result == DialogResult.No) //Move down
                {
                    names = MoveDown(names, s);
                    paths = MoveDown(paths, ss);
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
                textBox1.Text = dialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            outputFilepath = textBox1.Text;
            if (outputFilepath.Trim() != "")
            {
                string r = "";
                foreach(string s in paths)
                {
                    r += s+":";
                }
                r=r.Remove(r.Length-1);
                //System.Diagnostics.Process process = new System.Diagnostics.Process();
                //System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //startInfo.FileName = "cmd.exe";
                //startInfo.Arguments = "/C merge "+r+" "+outputFilepath;
                //process.StartInfo = startInfo;
                //process.Start();
                //process.WaitForExit();
            }
            else
            {
                MessageBox.Show("Please fill the output file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
