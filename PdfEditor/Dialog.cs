using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PdfEditor
{
    public partial class Dialog : Form
    {
        public string[] path;
        public string[] name;
        int mov;
        int movX;
        int movY;
        public Dialog()
        {
            InitializeComponent();
        }

        private List<string[]> DeleteElementsWithSpaces(string[] array, string[] array2)
        {
            List<string[]> retur = new List<string[]>();
            List<string> r = new List<string>();
            List<string> r1 = new List<string>();
            for(int i=0; i < array.Length; i++)
            {
                string s = array[i];
                string s1 = array2[i];
                r.Add(s);
                r1.Add(s1);
            }
            string[] ret = new string[r.Count];
            string[] rett = new string[r1.Count];
            for(int i=0; i < r.Count; i++)
            {
                ret[i] = r[i];
            }
            for (int i = 0; i < r1.Count; i++)
            {
                rett[i] = r1[i];
            }
            retur.Add(ret);
            retur.Add(rett);
            return retur;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = ".";
            dialog.Multiselect = true;
            dialog.Filter = "pdf files (*.pdf)|*.pdf";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string[] pathtemp= dialog.FileNames;
                string[] nametemp = dialog.SafeFileNames;
                path = pathtemp;
                name = nametemp;
                string s = "";
                for(int i=0; i < name.Length; i++)
                {
                    s += path[i] + "*";
                }
                textBox1.Text = s;
            }
        }
        private string[] Separate(string s, string ss)
        {
            List<string> sss = new List<string>();
            string ssss = "";
            for (int i=0; i < s.Length; i++)
            {
                if (s[i].ToString() != ss)
                {
                    ssss += s[i];
                }
                else
                {
                    sss.Add(ssss);
                    ssss = "";
                }
            }
            string[] r= new string[sss.Count];
            for(int i=0; i < sss.Count; i++)
            {
                r[i] = sss[i];
            }
            return r;
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }
        private List<string[]> GetFileNames(string[] array)
        {
            List<string[]> ret = new List<string[]>();
            List<string> arr1= new List<string>();
            List<string> arr = new List<string>();
            for(int i=0; i < array.Length; i++)
            {
                string filename = Path.GetFileName(array[i]);
                if (filename.Trim() != "")
                {
                    arr.Add(array[i]);
                    arr1.Add(filename);
                }
            }
            string[] r = new string[arr.Count];
            string[] r1 = new string[arr1.Count];
            for (int i = 0; i < arr.Count; i++)
            {
                r[i] = arr[i];
            }
            for (int i = 0; i < arr1.Count; i++)
            {
                r1[i] = arr1[i];
            }
            ret.Add(r);
            ret.Add(r1);
            return ret;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            path = Separate(textBox1.Text, "*");
            List<string[]> s = GetFileNames(path);
            path = s[0];
            name = s[1];
            string p = textBox1.Text;
            if (p.Trim() != "")
            {
                bool d = true;
                for(int i=0; i < path.Length; i++)
                {
                    if (!File.Exists(path[i]))
                    {
                        MessageBox.Show("File " + path[i] + " not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        d = false;
                        break;
                    }
                }
                if(d)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show("Please fill the file name path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
