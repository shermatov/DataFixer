using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _60733_VisualNomads_Project3_DataFixer
{
    public static class date
    {
        public static List<string> name = new List<string>();
        public static List<string> idName = new List<string>();
        public static Dictionary<int, string> ID = new Dictionary<int, string>();
        public static int count;
        public static List<string> Category = new List<string>() {"All Employees","Senior Developer", "Junior Developer", "Product Manager", "Sales Manager" };
        public static List<string> Sales = new List<string>();
        public static List<string> Senior = new List<string>();
        public static List<string> Junior = new List<string>();
        public static List<string> Product = new List<string>();
        public static List<string> fNameSales = new List<string>();
        public static List<string> fNameSenior = new List<string>();
        public static List<string> fNameJunior = new List<string>();
        public static List<string> fNameProduct = new List<string>();
        public static string[] person;

        public static void openFileDate(string path)
        {
            try
            {
                int a = 0;
                count = File.ReadAllLines(path).Length;
                StreamReader reader = new StreamReader(path);
                for (int i = 0; i < count; i++)
                {
                    string[] text = reader.ReadLine().Split(' ');
                    if (text[0] == "Name:")
                    {
                        name.Add(text[1] + " " + text[2]);
                    }
                    else if (text[0] == "ID:")
                    {
                        ID.Add(a, text[1]);
                        a++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void sort()
        {
            try
            {
                for (int i = 0; i < name.Count; i++)
                {
                    string[] idname = name[i].Split(' ');
                    string fname = idname[0], lname = idname[1];
                    idName.Add(fname[0].ToString() + fname[1].ToString() + lname[0].ToString() + lname[1].ToString() + lname[2].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void category()
        {
            try
            {
                for (int i = 0; i < ID.Count; i++)
                {
                    string[] idtext = ID[i].Split('-');
                    if (idtext[2] == "PM")
                    {
                        Product.Add(ID[i]);
                    }
                    else if (idtext[2] == "JD")
                    {
                        Junior.Add(ID[i]);
                    }
                    else if (idtext[2] == "SM")
                    {
                        Sales.Add(ID[i]);
                    }
                    else if (idtext[2] == "SD")
                    {
                        Senior.Add(ID[i]);
                    }
                    else
                    {
                        MessageBox.Show("Correct Category");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void fullName()
        {
            try
            {
                for (int i = 0; i < Sales.Count; i++)
                {
                    string[] sales = Sales[i].Split('-');
                    for (int j = 0; j < idName.Count; j++)
                    {
                        if (sales[0] == idName[j])
                        {
                            fNameSales.Add(name[j]);
                        }
                    }
                }
                for (int i = 0; i < Senior.Count; i++)
                {
                    string[] sales = Senior[i].Split('-');
                    for (int j = 0; j < idName.Count; j++)
                    {
                        if (sales[0] == idName[j])
                        {
                            fNameSenior.Add(name[j]);
                        }
                    }
                }
                for (int i = 0; i < Junior.Count; i++)
                {
                    string[] sales = Junior[i].Split('-');
                    for (int j = 0; j < idName.Count; j++)
                    {
                        if (sales[0] == idName[j])
                        {
                            fNameJunior.Add(name[j]);
                        }
                    }
                }
                for (int i = 0; i < Product.Count; i++)
                {
                    string[] sales = Product[i].Split('-');
                    for (int j = 0; j < idName.Count; j++)
                    {
                        if (sales[0] == idName[j])
                        {
                            fNameProduct.Add(name[j]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void selectedPersons(int index, string Name, string category)
        {
            try
            {
                if(category == "All Employees")
                {
                    for (int i = 0; i < name.Count; i++)
                    {
                        if(name[i] == Name)
                        {
                            for (int j = 0; j < ID.Count; j++)
                            {
                                string[] idname = ID[j].Split('-');
                                if (idname[0] == idName[i])
                                {
                                    person = idname;
                                }
                            }
                        }
                    }
                }
                else if (category == "Senior Developer")
                {
                    string[] idname = Senior[index].Split('-');
                    person = idname;
                }
                else if (category == "Junior Developer")
                {
                    string[] idname = Junior[index].Split('-');
                    person = idname;
                }
                else if (category == "Product Manager")
                {
                    string[] idname = Product[index].Split('-');
                    person = idname;
                }
                else if (category == "Sales Manager")
                {
                    string[] idname = Sales[index].Split('-');
                    person = idname;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void save(string category)
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Text files(*.txt)|*.txt";
                if (saveFile.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = saveFile.FileName;
                File.WriteAllText(filename, "+++++++++++++++ Fixed Data +++++++++++++++\n");
                if (category == "All Employees")
                {
                    File.AppendAllText(filename, "# Number of Records: " + name.Count + "\n");
                    File.AppendAllText(filename, "# Job Role: Product " + category + "\n\n");
                    saveProduct("Product Manager", saveFile);
                    saveSales("Sales Manager", saveFile);
                    saveSenior("Senior Developer", saveFile);
                    saveJunior("Junior Developer", saveFile);
                }
                else if (category == "Senior Developer")
                {
                    File.AppendAllText(filename, "# Number of Records: " + Senior.Count + "\n");
                    File.AppendAllText(filename, "# Job Role: Product " + category + "\n\n");
                    saveSenior(category,saveFile);
                }
                else if (category == "Junior Developer")
                {
                    File.AppendAllText(filename, "# Number of Records: " + Junior.Count + "\n");
                    File.AppendAllText(filename, "# Job Role: Product " + category + "\n\n");
                    saveJunior(category, saveFile);
                }
                else if (category == "Product Manager")
                {
                    File.AppendAllText(filename, "# Number of Records: " + Product.Count + "\n");
                    File.AppendAllText(filename, "# Job Role: Product " + category + "\n\n");
                    saveProduct(category, saveFile);
                }
                else if (category == "Sales Manager")
                {
                    File.AppendAllText(filename, "# Number of Records: " + Sales.Count + "\n");
                    File.AppendAllText(filename, "# Job Role: Product " + category + "\n\n");
                    saveSales(category, saveFile);
                }
                MessageBox.Show("file saved successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void saveSales(string category, SaveFileDialog saveFile)
        {
            string filename = saveFile.FileName;
            string dBith, birth;
            string[] senior;
            for (int i = 0; i < Sales.Count; i++)
            {
                birth = "";
                senior = Sales[i].ToString().Split('-');
                File.AppendAllText(filename, "Paramert: " + senior[0] + "\n");
                string[] name = fNameSales[i].ToString().Split(' ');
                dBith = senior[1].ToString();
                for (int j = 0; j < dBith.Length; j++)
                {
                    birth += dBith[j];
                    if (j == 3)
                    {
                        File.AppendAllText(filename, "Id: " + senior[2] + senior[0] + birth + "\n");
                        birth += "-";
                    }
                    else if (j == 5)
                    {
                        birth += "-";
                    }
                }
                File.AppendAllText(filename, "Fist Name: " + name[0] + "\n");
                File.AppendAllText(filename, "Last Name: " + name[1] + "\n");
                if (senior[3] == "F")
                {
                    File.AppendAllText(filename, "Gender: Female\n");
                }
                else if (senior[3] == "M")
                {
                    File.AppendAllText(filename, "Gender: Male\n");
                }
                File.AppendAllText(filename, "Date of Birth: " + birth + "\n");
                File.AppendAllText(filename, "Role: " + category + "\n");
                File.AppendAllText(filename, "------------------------------------------" + "\n");
            }

        }
        public static void saveSenior(string category, SaveFileDialog saveFile)
        {
            string filename = saveFile.FileName;
            string dBith, birth;
            string[] senior;
            for (int i = 0; i < Senior.Count; i++)
            {
                birth = "";
                senior = Senior[i].ToString().Split('-');
                File.AppendAllText(filename, "Paramert: " + senior[0] + "\n");
                string[] name = fNameSenior[i].ToString().Split(' ');
                dBith = senior[1].ToString();
                for (int j = 0; j < dBith.Length; j++)
                {
                    birth += dBith[j];
                    if (j == 3)
                    {
                        File.AppendAllText(filename, "Id: " + senior[2] + senior[0] + birth + "\n");
                        birth += "-";
                    }
                    else if (j == 5)
                    {
                        birth += "-";
                    }
                }
                File.AppendAllText(filename, "Fist Name: " + name[0] + "\n");
                File.AppendAllText(filename, "Last Name: " + name[1] + "\n");
                if (senior[3] == "F")
                {
                    File.AppendAllText(filename, "Gender: Female\n");
                }
                else if (senior[3] == "M")
                {
                    File.AppendAllText(filename, "Gender: Male\n");
                }
                File.AppendAllText(filename, "Date of Birth: " + birth + "\n");
                File.AppendAllText(filename, "Role: " + category + "\n");
                File.AppendAllText(filename, "------------------------------------------" + "\n");
            }
        }
        public static void saveJunior(string category, SaveFileDialog saveFile)
        {
            string filename = saveFile.FileName;
            string dBith, birth;
            string[] senior;
            for (int i = 0; i < Junior.Count; i++)
            {
                birth = "";
                senior = Junior[i].ToString().Split('-');
                File.AppendAllText(filename, "Paramert: " + senior[0] + "\n");
                string[] name = fNameJunior[i].ToString().Split(' ');
                dBith = senior[1].ToString();
                for (int j = 0; j < dBith.Length; j++)
                {
                    birth += dBith[j];
                    if (j == 3)
                    {
                        File.AppendAllText(filename, "Id: " + senior[2] + senior[0] + birth + "\n");
                        birth += "-";
                    }
                    else if (j == 5)
                    {
                        birth += "-";
                    }
                }
                File.AppendAllText(filename, "Fist Name: " + name[0] + "\n");
                File.AppendAllText(filename, "Last Name: " + name[1] + "\n");
                if (senior[3] == "F")
                {
                    File.AppendAllText(filename, "Gender: Female\n");
                }
                else if (senior[3] == "M")
                {
                    File.AppendAllText(filename, "Gender: Male\n");
                }
                File.AppendAllText(filename, "Date of Birth: " + birth + "\n");
                File.AppendAllText(filename, "Role: " + category + "\n");
                File.AppendAllText(filename, "------------------------------------------" + "\n");
            }
        }
        public static void saveProduct(string category, SaveFileDialog saveFile)
        {
            string filename = saveFile.FileName;
            string dBith, birth;
            string[] senior;
            for (int i = 0; i < Product.Count; i++)
            {
                birth = "";
                senior = Product[i].ToString().Split('-');
                File.AppendAllText(filename, "Paramert: " + senior[0] + "\n");
                string[] name = fNameProduct[i].ToString().Split(' ');
                dBith = senior[1].ToString();
                for (int j = 0; j < dBith.Length; j++)
                {
                    birth += dBith[j];
                    if (j == 3)
                    {
                        File.AppendAllText(filename, "Id: " + senior[2] + senior[0] + birth + "\n");
                        birth += "-";
                    }
                    else if (j == 5)
                    {
                        birth += "-";
                    }
                }
                File.AppendAllText(filename, "Fist Name: " + name[0] + "\n");
                File.AppendAllText(filename, "Last Name: " + name[1] + "\n");
                if (senior[3] == "F")
                {
                    File.AppendAllText(filename, "Gender: Female\n");
                }
                else if (senior[3] == "M")
                {
                    File.AppendAllText(filename, "Gender: Male\n");
                }
                File.AppendAllText(filename, "Date of Birth: " + birth + "\n");
                File.AppendAllText(filename, "Role: " + category + "\n");
                File.AppendAllText(filename, "------------------------------------------" + "\n");
            }

        }
        public static string Searc(int id)
        {
            for (int i = 0; i < ID.Count; i++)
            {
                person = ID[i].ToString().Split('-');
                string category = "";
                if (idName[id] == person[0])
                {
                    if (person[2] == "PM")
                    {
                        category = "Product Manager: ";
                    }
                    else if (person[2] == "JD")
                    {
                        category = "Junior Developer: ";
                    }
                    else if (person[2] == "SM")
                    {
                        category = "Sales Manager: ";
                    }
                    else if (person[2] == "SD")
                    {
                        category = "Senior Developer: ";
                    }
                    return category;
                }
            }
            return "";
        }
        public static void saveSearc(string category,string Name)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Text files(*.txt)|*.txt";
            if (saveFile.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFile.FileName;
            string dBith, birth = "";
            File.WriteAllText(filename, "+++++++++++++++ Fixed Data +++++++++++++++\n");
            File.AppendAllText(filename, "# Number of Records: " + 1 + "\n");
            File.AppendAllText(filename, "# Job Role: Product " + category + "\n\n");
            File.AppendAllText(filename, "Paramert: " + person[0] + "\n");
            string[] name = Name.Split(' ');
            dBith = person[1].ToString();
            for (int j = 0; j < dBith.Length; j++)
            {
                birth += dBith[j];
                if (j == 3)
                {
                    File.AppendAllText(filename, "Id: " + person[2] + person[0] + birth + "\n");
                    birth += "-";
                }
                else if (j == 5)
                {
                    birth += "-";
                }
            }
            File.AppendAllText(filename, "Fist Name: " + name[0] + "\n");
            File.AppendAllText(filename, "Last Name: " + name[1] + "\n");
            if (person[3] == "F")
            {
                File.AppendAllText(filename, "Gender: Female\n");
            }
            else if (person[3] == "M")
            {
                File.AppendAllText(filename, "Gender: Male\n");
            }
            File.AppendAllText(filename, "Date of Birth: " + birth + "\n");
            File.AppendAllText(filename, "Role: " + category + "\n");
            File.AppendAllText(filename, "------------------------------------------" + "\n");
            MessageBox.Show("file saved successfully");
        }
    }
}
