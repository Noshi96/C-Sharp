using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;


namespace CHashLab3Aplikacja
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        List<XmlNodeList> listOfFiles = new List<XmlNodeList> { };
        List<string> listOfFilesStr = new List<string> { };
        Boolean find = false;
        Boolean find2 = false;
        string pathToCsproj, pathOfSln, pathToFiles, sln, csprojFile, slnName;
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            makeFolder(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            richTextBox1.Text = string.Empty;
            listOfFiles.Clear();
            listOfFilesStr.Clear();
            find2 = false;

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
                string[] fileList = Directory.GetFiles(textBox1.Text);

                foreach (string s in fileList)
                {
                    richTextBox1.Text += Path.GetFileName(s);
                    slnName = Path.GetFileName(s);
                    richTextBox1.Text += "\n";                  

                    if (Path.GetExtension(s) == ".sln")
                    {
                        find = true;
                        find2 = true;
                        pathOfSln = s;
                        pathToFiles = s.Substring(0, s.LastIndexOf('.')) + "\\";
                    }
                }
                if(find == true)
                {
                    List<string> listOfStr = new List<string> { };
                    MessageBox.Show("Znaleziono plik SLN.");
                    sln = File.ReadAllText(pathOfSln);

                    var reg = new Regex("\".*?\"");
                    var matches = reg.Matches(sln);
                    foreach(var item in matches)
                    {
                        listOfStr.Add(item.ToString());
                    }
                    csprojFile = listOfStr[2].Split('\\').Last().Trim('"');
                    pathToCsproj = textBox1.Text + "\\" + listOfStr[2].Trim('"');

                    XmlDocument doc = new XmlDocument();
                    doc.Load(pathToCsproj);
                    listOfFiles.Add(doc.GetElementsByTagName("Compile"));
                    listOfFiles.Add(doc.GetElementsByTagName("EmbeddedResource"));
                    listOfFiles.Add(doc.GetElementsByTagName("None"));
                    foreach (XmlNodeList file in listOfFiles)
                    {
                        foreach(XmlNode item in file)
                        {
                            richTextBox1.Text += item.Attributes["Include"].Value + "\n";
                            listOfFilesStr.Add(item.Attributes["Include"].Value);
                        }
                    }
                    richTextBox1.Text += Path.GetFileName(pathToCsproj);
                    listOfFilesStr.Add(Path.GetFileName(pathToCsproj));
                    find = false;                    
                }
                else
                {
                    MessageBox.Show("Nie znaleziono pliku SLN, podaj inną ścieżkę!");
                }          
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void makeFolder(string path)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Podaj ścieżkę!");
            }
            else if(find2)
            {
                Directory.CreateDirectory(path + "\\kopia");
                if (!Directory.EnumerateFiles(path + "\\kopia").Any())
                {
                    foreach (string s in listOfFilesStr)
                    {
                        File.Copy((pathToFiles + s), path + "\\kopia\\" + s.Replace("Properties\\", ""));
                    }
                    File.Copy(pathOfSln, path + "\\kopia\\" + slnName);
                    MessageBox.Show("Wykonano pomyślnie kopie");
                }
                else
                {
                    MessageBox.Show("Folder już istnieje i są w nim pliki, proszę je usunąć!");
                }
            }
            else
            {
                MessageBox.Show("Nie można wykonać kopii, nie znaleziono pliku .sln!");
            }
        }
    }
}
