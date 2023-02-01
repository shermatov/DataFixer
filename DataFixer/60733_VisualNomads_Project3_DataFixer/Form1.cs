using System;
using System.IO;
using System.Windows.Forms;

namespace _60733_VisualNomads_Project3_DataFixer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                date.openFileDate(textBox1.Text);
                Form2 fm = new Form2();
                fm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please Load to txt file");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ofd.Filter = "text File | *.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
