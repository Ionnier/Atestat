using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
            this.Text = "Adauga " + tip;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        string tip = Form8.tiporange.ToLower();

        private void Form10_Load(object sender, EventArgs e)
        {
            if (tip == "calculator")
            {
                comboBox1.SelectedIndex = comboBox1.FindStringExact("Calculator");
            } else if (tip == "telefon")
            {
                comboBox1.SelectedIndex = comboBox1.FindStringExact("Telefon");
            }

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Telefon")
            {
                button1.Visible = true;
                panel1.Size = new Size(202, 10);
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                panel2.Size = new Size(245, 200);
                panel2.Location = new Point(331, 28);



            } else if (comboBox1.Text == "Calculator")
            {
                button1.Visible = true;
                panel1.Size = new Size(202, 194);
                panel2.Size = new Size(245, 10);
                panel2.Location = new Point(331, 218);
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Telefon")
            {
                string html = string.Empty;
                string url = @"https://localhost:8080/createfullproduct1?pret=" + textBox1.Text + "&cantitate=" + textBox2.Text + "&tip=Telefon";

                // https://localhost:8080/createfullproduct1?pret=500&cantitate=100&tip=Calculator&cpu=Intel&gpu=Nvidia&ram=8&stocare=500
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
                if (html == "Baza de date  offline") { MessageBox.Show("Baza de date offline"); }
                else
                {
                    int id;
                    id = int.Parse(html);

                    string html2 = string.Empty;
                    string url2 = @"https://localhost:8080/createfullphone2?pret=" + textBox1.Text + "&producator=" + textBox7.Text + "&model=" + textBox8.Text + "&ram=" + textBox10.Text + "&stocare=" + textBox9.Text +"&id="+id;
                    // https://localhost:8080/createfullphone2?id=3&pret=400&producator=apple&model=6s&ram=8&stocare=500
                    // https://localhost:8080/createfullproduct2?id=3&pret=400&cpu=intel&gpu=nvidia&ram=8%stocare=500
                    HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url2);
                    request2.AutomaticDecompression = DecompressionMethods.GZip;

                    using (HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse())
                    using (Stream stream2 = response2.GetResponseStream())
                    using (StreamReader reader2 = new StreamReader(stream2))
                    {
                        html2 = reader2.ReadToEnd();
                    }
                    if (html2 == "Executat")
                    {
                        this.Close();
                    }
                    else { MessageBox.Show("Eroare"); }

                }
            




                } else if (comboBox1.Text == "Calculator")
            {

                    string html = string.Empty;
                    string url = @"https://localhost:8080/createfullproduct1?pret="+textBox1.Text+"&cantitate="+textBox2.Text+"&tip=Calculator";

                    // https://localhost:8080/createfullproduct1?pret=500&cantitate=100&tip=Calculator&cpu=Intel&gpu=Nvidia&ram=8&stocare=500
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.AutomaticDecompression = DecompressionMethods.GZip;

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }
                    if (html == "Baza de date  offline") { MessageBox.Show("Baza de date offline"); }
                    else 
                    {
                        int id;
                        id = int.Parse(html);

                    string html2 = string.Empty;
                    string url2 = @"https://localhost:8080/createfullproduct2?pret=" + textBox1.Text + "&cpu=" + textBox3.Text + "&gpu=" + textBox4.Text + "&ram=" + textBox5.Text + "&stocare=" + textBox6.Text+"&id="+id;
                    Console.WriteLine(url2);
                    // https://localhost:8080/createfullphone2?id=3&pret=400&producator=apple&model=6s&ram=8&stocare=500
                    // https://localhost:8080/createfullproduct2?id=3&pret=400&cpu=intel&gpu=nvidia&ram=8%stocare=500
                    HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url2);
                    request2.AutomaticDecompression = DecompressionMethods.GZip;

                    using (HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse())
                    using (Stream stream2 = response2.GetResponseStream())
                    using (StreamReader reader2 = new StreamReader(stream2))
                    {
                        html2 = reader2.ReadToEnd();
                    }
                    if (html2 == "Executat")
                    {
                        this.Close();
                    } else { MessageBox.Show("Eroare");}


                }

                }
        }
    }
}
