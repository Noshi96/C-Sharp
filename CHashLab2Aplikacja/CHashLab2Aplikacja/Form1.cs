using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CHashLab1;
using CHashLab2;

namespace CHashLab2Aplikacja
{
    public partial class Form1 : Form
    {
        int port;
        string path;
        HTTPServer obiekt;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {        
            obiekt = new HTTPServer(textBox2.Text, (int)numericUpDown1.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            obiekt.stop();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string path = textBox2.Text;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int port = (int)numericUpDown1.Value;
        }
    }
}
