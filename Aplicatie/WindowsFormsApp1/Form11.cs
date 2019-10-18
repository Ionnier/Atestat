using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }



        public class Comenzi
        {
            public int ID { get; set; }
            public DateTime Data { get; set; }
            public int Produs { get; set; }
            public int COUNT { get; set; }
        }

        public class RootObject
        {
            public int Produs { get; set; }
            public int Numar { get; set; }
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
        private void button1_Click(object sender, EventArgs e)
        {
            
            monthCalendar1.Visible = false;
            button1.Visible = false;
            if (start == end)
            {
                populateDGV(start);
                Thread.Sleep(1000);
                mostsoldday1(start);
                dataGridView1.Visible = true;
                try
                {
                    label2.Text = "Cel mai vandut produs este:"+ dataGridView2.Rows[0].Cells[2].Value.ToString();
                    label2.Visible = true;
                }
                catch { label2.Text = "No data"; label2.Visible = true; dataGridView1.Visible = false; }

            } else
            {
                populateDGV2(start, end);
                try
                {
                    label2.Text = "Cel mai vandut produs este:" + dataGridView1.Rows[0].Cells[0].Value.ToString();
                    label2.Visible = true;
                    dataGridView1.Visible = true;
                }
                catch { label2.Text = "No data";label2.Visible = true;dataGridView1.Visible = false; }
                }
        }
        string start=DateTime.Now.ToString("yyyy-MM-dd");
        string end=DateTime.Now.ToString("yyyy-MM-dd");
        public void populateDGV(string dataaici)
        {
            string html = string.Empty;
            string url = @"https://localhost:8080/getsales?data="+dataaici;
            Console.WriteLine(url);
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
                var result = JsonConvert.DeserializeObject<List<Comenzi>>(html);
                DataTable dt = new DataTable();
                dt = ToDataTable(result);
  
                dataGridView1.DataSource = dt;

            }
            


        }

        public void populateDGV2(string datastart, string dataend)
        {
            string html = string.Empty;
            string url = @"https://localhost:8080/getsales2?start="+datastart+"&end="+dataend;

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
                var result = JsonConvert.DeserializeObject<List<RootObject>>(html);
                DataTable dt = new DataTable();
                dt = ToDataTable(result);

                dataGridView1.DataSource = dt;

            }



        }
        public void mostsoldday1(string dataaici)
        {
            string html = string.Empty;
            string url = @"https://localhost:8080/getmostsold?data=" + dataaici;

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
                var result = JsonConvert.DeserializeObject<List<Comenzi>>(html);
                DataTable dt = new DataTable();
                dt = ToDataTable(result);
                dataGridView2.DataSource = dt;

            }

        }
       
        
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

                 button1.Visible = true;
                 start = monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd");
                 end = monthCalendar1.SelectionRange.End.ToString("yyyy-MM-dd");
            
        }

        
    }
}
