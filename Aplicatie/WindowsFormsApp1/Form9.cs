using Newtonsoft.Json;
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
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
            populateDGV1();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = tip;
            try
            {
                if (tip == "Calculator")
                {
                    populateDGV2();

                }
                else if (tip == "Telefon")
                {
                    populateDGV3();
                }
                Thread.Sleep(100);
                populateChart();
            }
            catch { MessageBox.Show("Eroare");this.Close(); }
        }

        int id = Form8.idgrape;
        string tip = Form8.tipgrape;
        int pret = Form8.pretgrape;

        private void Form9_Load(object sender, EventArgs e)
        {
          
            

        }

        public class PriceHistory
        {
            public int Produs { get; set; }
            public int Pret { get; set; }
            public DateTime Date { get; set; }
        }
        public class Calculator
        {
            public int ID { get; set; }
            public int Produs { get; set; }
            public string CPU { get; set; }
            public string GPU { get; set; }
            public int RAM { get; set; }
            public int Stocare { get; set; }
        }
        public class Telefon
        {
            public int ID { get; set; }
            public int Produs { get; set; }
            public string Producator { get; set; }
            public string Model { get; set; }
            public int Stocare { get; set; }
            public int RAM { get; set; }
        }
        public class Chart
        {
            public DateTime Data { get; set; }
            public int Counter { get; set; }
        }
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        public void populateDGV1()
        {
            string html = string.Empty;
            string url = @"https://localhost:8080/product?id="+id.ToString();

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
                var result = JsonConvert.DeserializeObject<List<PriceHistory>>(html);
                DataTable dt = new DataTable();
                dt = ToDataTable(result);

                dataGridView1.DataSource = dt;
                

                List<int> array1 = new List<int>();
                List<DateTime> array2 = new List<DateTime>();

                for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                {
                    int va = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                    DateTime va2 = Convert.ToDateTime(dataGridView1.Rows[i].Cells[2].Value);
                    array1.Add(va);
                    array2.Add(va2);
                    Console.WriteLine(va2);
                }
                array1.Add(pret);
                string data = DateTime.Now.ToString();//"yyyy-MM-dd HH:mm:ss"
                Console.WriteLine(data);
                DateTime va3 = Convert.ToDateTime(data);
                array2.Add(va3);

                chart1.Series["Series3"].IsVisibleInLegend = false;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
                //chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {
                    chart1.Series["Series3"].Points.AddXY(array2[i], array1[i]);
                }

            }
            


        }

        public void populateChart()
        {
            string html = string.Empty;
            string url = @"https://localhost:8080/getsalesgraph?product=" + id.ToString();

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
                var result = JsonConvert.DeserializeObject<List<Chart>>(html);
                DataTable dt = new DataTable();
                dt = ToDataTable(result);

                dataGridView1.DataSource = dt;


                List<int> array1 = new List<int>();
                List<DateTime> array2 = new List<DateTime>();

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    int va = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                    DateTime va2 = Convert.ToDateTime(dataGridView1.Rows[i].Cells[0].Value);
                    array1.Add(va);
                    array2.Add(va2);
                    Console.WriteLine(va2);
                }
                
                chart2.Series["Series1"].IsVisibleInLegend = false;
                chart2.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                chart2.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
                //chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    chart2.Series["Series1"].Points.AddXY(array2[i], array1[i]);
                }

            }


        }

        public void populateDGV2()
        {
            string html = string.Empty;
            string url = @"https://localhost:8080/calculator?id=" + id.ToString();

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
                var result = JsonConvert.DeserializeObject<List<Calculator>>(html);
                DataTable dt = new DataTable();
                dt = ToDataTable(result);
                dataGridView1.DataSource = dt;
            }
            string cpu = dataGridView1.Rows[0].Cells["CPU"].Value.ToString();
            string gpu = dataGridView1.Rows[0].Cells["GPU"].Value.ToString();
            string ram = dataGridView1.Rows[0].Cells["RAM"].Value.ToString();
            string stocare = dataGridView1.Rows[0].Cells["Stocare"].Value.ToString();

            label1.Text = "Număr calculator=" + id.ToString();
            label2.Text = "CPU:" +cpu;
            label3.Text = "GPU:" + gpu;
            label4.Text = "RAM:" + ram;
            label5.Text = "Stocare:" + stocare;
        }


        public void populateDGV3()
        {
            string html = string.Empty;
            string url = @"https://localhost:8080/telefon?id=" + id.ToString();

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
                var result = JsonConvert.DeserializeObject<List<Telefon>>(html);
                DataTable dt = new DataTable();
                dt = ToDataTable(result);
                dataGridView1.DataSource = dt;
            }
            string producator = dataGridView1.Rows[0].Cells["Producator"].Value.ToString();
            string model = dataGridView1.Rows[0].Cells["Model"].Value.ToString();
            string stocare = dataGridView1.Rows[0].Cells["Stocare"].Value.ToString();
            string ram = dataGridView1.Rows[0].Cells["RAM"].Value.ToString();

            //label1.Text = "Număr telefon= " + id.ToString();
            label1.Visible =false;
            label2.Text = "Producator: " + producator;
            label3.Text = "Model: " + model;
            label4.Text = "RAM(GB): " + ram;
            label5.Text = "Stocare(GB): " + stocare;
        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
