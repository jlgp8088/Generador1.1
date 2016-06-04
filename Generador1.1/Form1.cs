using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generador1._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.companyname = textBox1.Text;
            Properties.Settings.Default.ruta = textBox2.Text;
            Properties.Settings.Default.header = textheader.Text;
            Properties.Settings.Default.footer = textfooter.Text;
            Properties.Settings.Default.observation = textBox3.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            string dir = openFileDialog1.FileName;
                            textBox2.Text = dir;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: No Se Pudo Leer el archivo original. Original error: " + ex.Message);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.companyname;
            textBox2.Text = Properties.Settings.Default.ruta;
            textheader.Text = Properties.Settings.Default.header;
            textfooter.Text = Properties.Settings.Default.footer;
            textBox3.Text = Properties.Settings.Default.observation;
        }
    }
}
