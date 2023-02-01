using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _60733_VisualNomads_Project3_DataFixer
{
    public partial class Form2 : Form
    {
        bool src = false;
        string category;
        public Form2()
        {
            InitializeComponent();
            recordCount.Text = (date.count/2).ToString()+ " Records";
            for (int i = 0; i < date.Category.Count; i++)
            {
                comboBox1.Items.Add(date.Category[i]);
            }
            date.sort();
            date.category();
            date.fullName();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                src = true;
                for (int i = 0; i < date.name.Count; i++)
                {
                    if (textBox1.Text == date.name[i])
                    {
                        listBox1.Items.Clear();
                        category = date.Searc(i);
                        listBox1.Items.Add(category + textBox1.Text);
                    }
                }
                if(textBox1.Text == "")
                {
                    MessageBox.Show("No result Founded");
                }
            }
            else
            {
                MessageBox.Show("Please enter you correct name");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            src = false;
            listBox1.Items.Clear();
            if (comboBox1.Text == "All Employees")
            {
                for (int i = 0; i < date.fNameProduct.Count; i++)
                {
                    listBox1.Items.Add("Sales Manager: " + date.fNameProduct[i]);
                }
                for (int i = 0; i < date.fNameSales.Count; i++)
                {

                    listBox1.Items.Add("Senior Developer: " + date.fNameSales[i]);
                }
                for (int i = 0; i < date.fNameSenior.Count; i++)
                {
                    listBox1.Items.Add("Junior Developer: " + date.fNameSenior[i]);
                }
                for (int i = 0; i < date.fNameJunior.Count; i++)
                {
                    listBox1.Items.Add("Product Manager: " + date.fNameJunior[i]);
                }
            }
            else if (comboBox1.Text == "Senior Developer")
            {
                for (int i = 0; i < date.fNameSenior.Count; i++)
                {
                    listBox1.Items.Add("Senior Developer: " + date.fNameSenior[i]);
                }
            }
            else if (comboBox1.Text == "Junior Developer")
            {
                for (int i = 0; i < date.fNameJunior.Count; i++)
                {
                    listBox1.Items.Add("Junior Developer: " + date.fNameJunior[i]);
                }
            }
            else if (comboBox1.Text == "Product Manager")
            {
                for (int i = 0; i < date.fNameProduct.Count; i++)
                {
                    listBox1.Items.Add("Product Manager: " + date.fNameProduct[i]);
                }
            }
            else if (comboBox1.Text == "Sales Manager")
            {
                for (int i = 0; i < date.fNameSales.Count; i++)
                {
                    listBox1.Items.Add("Sales Manager: " + date.fNameSales[i]);
                }
            }
            listRecords.Text = "Number of Records: " + listBox1.Items.Count.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                if (src)
                {
                    string listText = listBox1.Items[listBox1.SelectedIndex].ToString();
                    string[] name = listText.Split();
                    paramert.Text = date.person[0];
                    fistName.Text = name[2];
                    lastName.Text = name[3];
                    id.Text = "";
                    birth.Text = "";
                    role.Text = name[0] + " " + name[1].ToString().TrimEnd(':');
                    if (date.person[3] == "F")
                    {
                        gender.Text = "Female";
                    }
                    else if (date.person[3] == "M")
                    {
                        gender.Text = "Male";
                    }
                    string dBith = date.person[1].ToString();
                    for (int i = 0; i < dBith.Length; i++)
                    {
                        birth.Text += dBith[i];
                        if (i == 3)
                        {
                            id.Text = date.person[2] + date.person[0] + birth.Text;
                            birth.Text += "-";
                        }
                        else if (i == 5)
                        {
                            birth.Text += "-";
                        }
                    }
                }
                else
                {
                    string listText = listBox1.Items[listBox1.SelectedIndex].ToString();
                    string[] name = listText.Split();
                    date.selectedPersons(listBox1.SelectedIndex, name[2] + " " + name[3], comboBox1.Text);
                    paramert.Text = date.person[0];
                    fistName.Text = name[2];
                    lastName.Text = name[3];
                    id.Text = "";
                    birth.Text = "";
                    role.Text = name[0] + " " + name[1].ToString().TrimEnd(':');
                    if (date.person[3] == "F")
                    {
                        gender.Text = "Female";
                    }
                    else if (date.person[3] == "M")
                    {
                        gender.Text = "Male";
                    }
                    string dBith = date.person[1].ToString();
                    for (int i = 0; i < dBith.Length; i++)
                    {
                        birth.Text += dBith[i];
                        if (i == 3)
                        {
                            id.Text = date.person[2] + date.person[0] + birth.Text;
                            birth.Text += "-";
                        }
                        else if (i == 5)
                        {
                            birth.Text += "-";
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select to Person");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(listBox1.Items.Count != 0)
            {
                if (src)
                {
                    date.saveSearc(category, listBox1.Items[listBox1.SelectedIndex].ToString());
                }
                else
                {
                    date.save(comboBox1.Text);
                }
            }
        }
    }
}
