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
            for (int i = 0; i < ss.Count; i++)
            {
                if (i == index - 1)
                {
                    string d = ss[i];
                    ssss[i] = sss;
                    ssss[i + 1] = d;
                }
            }

            return ssss;
        }

        private List<string> MoveDown(List<string> ss, string sss)
        {
            List<string> ssss = ss;
            int index = ss.IndexOf(sss);
            for (int i = 0; i < ss.Count; i++)
            {
                if (i == index + 1)
                {
                    string d = ss[i];
                    ssss[i] = sss;
                    ssss[i - 1] = d;
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
            string s = names[listBox1.SelectedIndex];
            if (result == DialogResult.OK) //Delete
            {
                Delete(names, s);
            }
            else if (result == DialogResult.Yes) //Move up
            {
                names = MoveUp(names, s);
            }
            if (result == DialogResult.No) //Move down
            {
                names = MoveDown(names, s);
            }
            listBox1.Items.Clear();
            for (int i = 0; i < names.Count(); i++)
            {
                listBox1.Items.Add(names[i]);
            }
        }
    }
}
