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

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = ".";
            dialog.Multiselect = true;
            dialog.Filter = "pdf files (*.pdf)|*.pdf";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.FileNames;
                name = dialog.SafeFileNames;
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

        private void buttonOk_Click(object sender, EventArgs e)
        {
            path = Separate(textBox1.Text, "*");
            string p = textBox1.Text;
            if (p.Trim() != "")
            {
                bool d = true;
                for(int i=0; i < path.Length; i++)
                {
                    if (!File.Exists(path[i]))
                    {
                        MessageBox.Show("File " + path + " not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
