using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            this.Text = "User Form";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            populateDGV();
            populateDGV2();
            populateDGV3();
            populateDGV4();
            watch.Start();
        }

        int counter = 0;
        int tabel = 1;

        public class Bills
        {
            public int ID { get; set; }
            public int Valoare { get; set; }
            public int Zi { get; set; }
            public int Luna { get; set; }
            public int An { get; set; }
        }
        public class Products
        {
            public int ID { get; set; }
            public int CPret { get; set; }
            public int Cantitate { get; set; }
            public string CTip { get; set; }
        }
        public class Computers
        {
            public int ID { get; set; }
            public int Produs { get; set; }
            public string CPU { get; set; }
            public string GPU { get; set; }
            public int RAM { get; set; }
            public int Stocare { get; set; }
        }
        public class Phones
        {
            public int ID { get; set; }
            public int Produs { get; set; }
            public string Producator { get; set; }
            public string Model { get; set; }
            public int Stocare { get; set; }
            public int RAM { get; set; }
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
        private void SaveToCSV(DataGridView DGV)
        {
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.FileName = "Output.csv";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                
                if (File.Exists(filename))
                {
                    try
                    {
                        File.Delete(filename);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Eroare." + ex.Message);
                    }
                }
                int columnCount = DGV.ColumnCount;
                string columnNames = "";
                string[] output = new string[DGV.RowCount + 1];
                for (int i = 0; i < columnCount; i++)
                {
                    columnNames += DGV.Columns[i].Name.ToString() + ",";
                }
                output[0] += columnNames;
                for (int i = 1; (i - 1) < DGV.RowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        output[i] += DGV.Rows[i - 1].Cells[j].Value.ToString() + ",";
                    }
                }
                System.IO.File.WriteAllLines(sfd.FileName, output, System.Text.Encoding.UTF8);
                MessageBox.Show("Salvat.");
            }
        }

        public void populateDGV()
        {
            string html = string.Empty;
            string url = @"https://localhost:8080/bills";

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
                var result = JsonConvert.DeserializeObject<List<Bills>>(html);
                DataTable dt = new DataTable();
                dt = ToDataTable(result);

                dataGridView1.DataSource = dt;

            }
            dataGridView1.Columns["ID"].ReadOnly = true;


        }
        public void populateDGV2()
        {
            string html = string.Empty;
            string url = @"https://localhost:8080/products";

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
                var result = JsonConvert.DeserializeObject<List<Products>>(html);
                DataTable dt = new DataTable();
                dt = ToDataTable(result);

                dataGridView2.DataSource = dt;

            }
            dataGridView2.Columns["ID"].ReadOnly = true;


        }
        public void populateDGV3()
        {
            string html = string.Empty;
            string url = @"https://localhost:8080/computers";

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
                var result = JsonConvert.DeserializeObject<List<Computers>>(html);
                DataTable dt = new DataTable();
                dt = ToDataTable(result);

                dataGridView3.DataSource = dt;

            }
            dataGridView3.Columns["ID"].ReadOnly = true;
            dataGridView3.Columns["Produs"].ReadOnly = true;

        }
        public void populateDGV4()
        {
            string html = string.Empty;
            string url = @"https://localhost:8080/phones";

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
                var result = JsonConvert.DeserializeObject<List<Phones>>(html);
                DataTable dt = new DataTable();
                dt = ToDataTable(result);

                dataGridView4.DataSource = dt;

            }
            dataGridView4.Columns["ID"].ReadOnly = true;
            dataGridView4.Columns["Produs"].ReadOnly = true;


        }

        int counterrr=0;

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {


            if (counter == 0)
            {

                int id;
                int zi;
                int luna;
                int an;
                int valoare;



                valoare = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Valoare"].Value.ToString());
                id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                zi = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Zi"].Value.ToString());
                luna = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Luna"].Value.ToString());
                an = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["An"].Value.ToString());




                string html2 = string.Empty;
                string url2 = @"https://localhost:8080/updategridbill?valoare=" + valoare + "&zi=" + zi + "&luna=" + luna + "&an=" + an + "&id=" + id;
                // https://localhost:8080/updategridbill?valoare=130&zi=5&luna=6&an=2017&id=2
                Console.WriteLine(url2);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url2);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html2 = reader.ReadToEnd();
                }


                if (html2 == "Executat")
                {

                }
                else if (html2 == "Baza de date offline")
                {
                    MessageBox.Show("Baza de date offline", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                counter = 0;
            }

        }
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            counter = 1;
            // Don't throw an exception when we're done.
            e.ThrowException = false;

            // Display an error message.
            string txt = "Error with " +
                dataGridView1.Columns[e.ColumnIndex].HeaderText +
                "\n\n" + e.Exception.Message;
            MessageBox.Show(txt, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            // If this is true, then the user is trapped in this cell.
            e.Cancel = false;
        }


        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            counterrr = 1;
            // Don't throw an exception when we're done.
            e.ThrowException = false;

            // Display an error message.
            string txt = "Error with " +
                dataGridView1.Columns[e.ColumnIndex].HeaderText +
                "\n\n" + e.Exception.Message;
            MessageBox.Show(txt, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            // If this is true, then the user is trapped in this cell.
            e.Cancel = false;
        }
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            
            if (counterrr == 0)
            {
                int modpret = 0;
                if (dataGridView2.CurrentCell.ColumnIndex == 1)
                {
                    modpret = 1;
                }
                string cpret;
                string ctip;
                int cantitate;
                int id;


                cpret = dataGridView2.Rows[e.RowIndex].Cells["CPret"].Value.ToString();
                id = int.Parse(dataGridView2.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                cantitate = int.Parse(dataGridView2.Rows[e.RowIndex].Cells["Cantitate"].Value.ToString());
                ctip = dataGridView2.Rows[e.RowIndex].Cells["CTip"].Value.ToString();



                
                string html2 = string.Empty;
                string url2 = @"https://localhost:8080/updategridproduct?pret=" + cpret + "&cantitate=" + cantitate + "&tip=" + ctip + "&id=" + id +"&modpret=" + modpret;
                // https://localhost:8080/updategridbill?valoare=130&zi=5&luna=6&an=2017&id=2
                Console.WriteLine(url2);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url2);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html2 = reader.ReadToEnd();
                }


                if (html2 == "Executat")
                {

                }
                else if (html2 == "Baza de date offline")
                {
                    MessageBox.Show("Baza de date offline", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                counterrr = 0;
            }

        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (counter == 0)
            {

                int id;
                string cpu;
                string gpu;
                int stocare;
                int ram;


                id = int.Parse(dataGridView3.Rows[e.RowIndex].Cells["ID"].Value.ToString());

                cpu = dataGridView3.Rows[e.RowIndex].Cells["CPU"].Value.ToString();
                gpu = dataGridView3.Rows[e.RowIndex].Cells["GPU"].Value.ToString();
                stocare = int.Parse(dataGridView3.Rows[e.RowIndex].Cells["Stocare"].Value.ToString());
                ram = int.Parse(dataGridView3.Rows[e.RowIndex].Cells["RAM"].Value.ToString());




                string html2 = string.Empty;
                string url2 = @"https://localhost:8080/updategridcomputer?cpu=" + cpu + "&gpu=" + gpu + "&stocare=" + stocare + "&ram=" + ram + "&id=" + id;
                // https://localhost:8080/updategridcomputer?cpu=amd&gpu=amd&ram=16&stocare=500&id=1
                Console.WriteLine(url2);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url2);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html2 = reader.ReadToEnd();
                }


                if (html2 == "Executat")
                {

                }
                else if (html2 == "Baza de date offline")
                {
                    MessageBox.Show("Baza de date offline", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                counter = 0;
            }
        }
        private void dataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            counterrr = 1;
            // Don't throw an exception when we're done.
            e.ThrowException = false;

            // Display an error message.
            string txt = "Error with " +
                dataGridView1.Columns[e.ColumnIndex].HeaderText +
                "\n\n" + e.Exception.Message;
            MessageBox.Show(txt, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            // If this is true, then the user is trapped in this cell.
            e.Cancel = false;
        }

        private void dataGridView4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (counter == 0)
            {

                int id;
                string producator;
                string model;
                int stocare;
                int ram;


                id = int.Parse(dataGridView4.Rows[e.RowIndex].Cells["ID"].Value.ToString());

                producator = dataGridView4.Rows[e.RowIndex].Cells["Producator"].Value.ToString();
                model = dataGridView4.Rows[e.RowIndex].Cells["Model"].Value.ToString();
                stocare = int.Parse(dataGridView4.Rows[e.RowIndex].Cells["Stocare"].Value.ToString());
                ram = int.Parse(dataGridView4.Rows[e.RowIndex].Cells["RAM"].Value.ToString());




                string html2 = string.Empty;
                string url2 = @"https://localhost:8080/updategridphone?producator=" + producator + "&model=" + model + "&stocare=" + stocare + "&ram=" + ram + "&id=" + id;
                // https://localhost:8080/updategridphone?producator=amd&model=amd&stocare=1000&ram=500&id=2
                Console.WriteLine(url2);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url2);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html2 = reader.ReadToEnd();
                }


                if (html2 == "Executat")
                {

                }
                else if (html2 == "Baza de date offline")
                {
                    MessageBox.Show("Baza de date offline", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                counter = 0;
            }
        }
        private void dataGridView4_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            counterrr = 1;
            // Don't throw an exception when we're done.
            e.ThrowException = false;

            // Display an error message.
            string txt = "Error with " +
                dataGridView1.Columns[e.ColumnIndex].HeaderText +
                "\n\n" + e.Exception.Message;
            MessageBox.Show(txt, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            // If this is true, then the user is trapped in this cell.
            e.Cancel = false;
        }




        private void SavePDF(DataGridView datagridview, string namaee)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Documente PDF (*.pdf)|*.pdf";
                sfd.FileName = DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
                    string filename = sfd.FileName;
                    FileStream file = new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    PdfWriter.GetInstance(doc, file);
                    doc.Open();
                    GenerarDocumento(doc, datagridview, namaee);
                    doc.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        public void GenerarDocumento(Document document, DataGridView dataGridView1, string namae)
        {

            //se crea un objeto PdfTable con el # de columnas del dataGridView


            PdfPTable datatable = new PdfPTable(dataGridView1.ColumnCount);


            //asignamos algunas propiedades para el diseño del pdf

            datatable.DefaultCell.Padding = 3;

            float[] headerwidths = GetTamañoColumnas(dataGridView1);

            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 100;
            datatable.DefaultCell.BorderWidth = 2;
            datatable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;


            Paragraph heading = new Paragraph("Tabel de Date    - " + namae);


            heading.SpacingAfter = 18f;

            Paragraph headin2 = new Paragraph("Din:" + DateTime.Now.ToString());

            headin2.SpacingAfter = 18f;


            document.Add(heading);
            document.Add(headin2);

            //SE GENERA EL ENCABEZADO DE LA TABLA EN EL PDF

            for (int i = 0; i < dataGridView1.ColumnCount; i++)

            {

                datatable.AddCell(dataGridView1.Columns[i].HeaderText);

            }

            datatable.HeaderRows = 1;
            datatable.DefaultCell.BorderWidth = 1;

            //SE GENERA EL CUERPO DEL PDF

            for (int i = 0; i < dataGridView1.RowCount; i++)

            {

                for (int j = 0; j < dataGridView1.ColumnCount; j++)

                {

                    datatable.AddCell(dataGridView1[j, i].Value.ToString());

                }

                datatable.CompleteRow();

            }

            //Add PdfTable to the doc


            document.Add(datatable);


        }
        public float[] GetTamañoColumnas(DataGridView dg)

        {

            float[] values = new float[dg.ColumnCount];

            for (int i = 0; i < dg.ColumnCount; i++)

            {

                values[i] = (float)dg.Columns[i].Width;

            }

            return values;

        }

        public static string tiporange;
        private void Form8_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                button1.PerformClick();
            }

            if (e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add)
            {
                if (tabel == 1)
                {
                    string html2 = string.Empty;
                    int zi = DateTime.Today.Day;
                    int an = DateTime.Today.Year;
                    int luna = DateTime.Today.Month;
                    string url2 = @"https://localhost:8080/createbill?zi=" + zi + "&luna=" + luna + "&&an=" + an;
                    // https://localhost:8080/createbill?zi=3&luna=14&an=2444

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url2);
                    request.AutomaticDecompression = DecompressionMethods.GZip;
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html2 = reader.ReadToEnd();
                    }


                    if (html2 == "Executat")
                    {
                        populateDGV();
                    }
                    else if (html2 == "Baza de date offline")
                    {
                        MessageBox.Show("Baza de date offline", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if (tabel == 2 || tabel==3 || tabel==4 )
                {
                    switch (tabel)
                    {
                        case 2:tiporange = "Produs";break;
                        case 3:tiporange = "Calculator";break;
                        case 4:tiporange = "Telefon";break;
                    }

                    using (Form10 f10 = new Form10())
                    {
                        f10.ShowDialog(this);
                    }

                    /*string html2 = string.Empty;
                    string url2 = @"https://localhost:8080/createproduct";
                    // https://localhost:8080/createbill?zi=3&luna=14&an=2444

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url2);
                    request.AutomaticDecompression = DecompressionMethods.GZip;
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html2 = reader.ReadToEnd();
                    }


                    if (html2 == "Executat")
                    {
                        populateDGV2();
                    }
                    else if (html2 == "Baza de date offline")
                    {
                        MessageBox.Show("Baza de date offline", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    */




                }

                else if (tabel == 3)
                {

                }
            }

            if (e.KeyCode == Keys.Delete)
            {
                if (tabel == 1)
                {
                    if (dataGridView1.SelectedRows.Count == 0)
                    {

                    }
                    else
                    {
                        int counterr = 0;
                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {



                            int indexx = int.Parse(row.Index.ToString());

                            int id;
                            id = int.Parse(dataGridView1.Rows[indexx].Cells["ID"].Value.ToString());

                            string html23 = string.Empty;
                            string url23 = @"https://localhost:8080/deletegridbill?id=" + id;
                            // https://localhost:8080/deletegridu?id=14

                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url23);
                            request.AutomaticDecompression = DecompressionMethods.GZip;
                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            using (Stream stream = response.GetResponseStream())
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                html23 = reader.ReadToEnd();
                            }
                            

                            if (html23 == "Executat")
                            {
                                counterr = 1;
                            }

                        }
                        if (counterr == 0)
                        {
                            MessageBox.Show("Baza de date offline", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            populateDGV();
                        }

                    }
                }
                else if (tabel == 2)
                {

                    if (dataGridView2.SelectedRows.Count == 0)
                    {

                    }
                    else
                    {
                        int counterr = 0;
                        foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                        {



                            int indexx = int.Parse(row.Index.ToString());

                            int id;
                            id = int.Parse(dataGridView2.Rows[indexx].Cells["ID"].Value.ToString());
                            string ctip;

                            ctip = dataGridView2.Rows[indexx].Cells["CTip"].Value.ToString();
                            ctip=ctip.ToLower();
                            string html23 = string.Empty;
                            string url23 = @"https://localhost:8080/deletegridproduct?id=" + id+"&tip="+ctip;

                            // https://localhost:8080/deletegridu?id=14
                            Console.WriteLine(url23);
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url23);
                            request.AutomaticDecompression = DecompressionMethods.GZip;
                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            using (Stream stream = response.GetResponseStream())
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                html23 = reader.ReadToEnd();
                            }


                            if (html23 == "Executat")
                            {
                                counterr = 1;
                            }

                        }
                        if (counterr == 0)
                        {
                            MessageBox.Show("Baza de date offline", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            populateDGV2();
                        }


                    }
                }

                else if (tabel == 3)
                {


                }

            }

            if (e.Control & e.KeyCode.ToString() == "S")
            {
                if (tabel != 0)
                {
                    using (Form7 form7 = new Form7())
                    {
                        if (form7.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            if (form7.SelectedText == "CSV")
                            {
                                if (tabel == 1)
                                {
                                    SaveToCSV(dataGridView1);

                                }
                                else
                                if (tabel == 2)
                                {
                                    SaveToCSV(dataGridView2);
                                }
                                else
                                if (tabel == 3)
                                {
                                    SaveToCSV(dataGridView3);
                                }
                                else
                                if (tabel == 4)
                                {
                                    SaveToCSV(dataGridView4);
                                }

                            }
                            else if (form7.SelectedText == "PDF")
                            {

                                if (tabel == 1)
                                {
                                    SavePDF(dataGridView1, "Facturi");

                                }
                                else
                                if (tabel == 2)
                                {
                                    SavePDF(dataGridView2, "Produse");
                                }
                                else
                                if (tabel == 3)
                                {
                                    SavePDF(dataGridView3, "Calculatoare");
                                }
                                else
                                if (tabel == 4)
                                {
                                    SavePDF(dataGridView4, "Telefoane");
                                }


                            }
                        }
                    }

                }

                /* if (tabel == 1) {
                    SavePDF(dataGridView1);

                } if (tabel == 2)
                {
                    SaveToCSV(dataGridView2);
                }*/
            }

            if (e.KeyCode == Keys.G) {

                if (tabel == 2)
                {
                    if (dataGridView2.SelectedRows.Count == 0)
                    {

                    } else
                    {
                        int counterr = 0;
                        foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                        {



                            int indexx = int.Parse(row.Index.ToString());

                            int id;
                            id = int.Parse(dataGridView2.Rows[indexx].Cells["ID"].Value.ToString());
                            string html23 = string.Empty;
                            string url23 = @"https://localhost:8080/generatesale?produs=" + id;

                            // https://localhost:8080/deletegridu?id=14
                            Console.WriteLine(url23);
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url23);
                            request.AutomaticDecompression = DecompressionMethods.GZip;
                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            using (Stream stream = response.GetResponseStream())
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                html23 = reader.ReadToEnd();
                            }


                            if (html23 == "Executat")
                            {
                                counterr = 1;
                            }

                        }
                        if (counterr == 0)
                        {
                            MessageBox.Show("Baza de date offline", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            populateDGV2();
                        }

                    }
                }

            }

            if (e.KeyCode == Keys.R)
            {

                
                    
 
                    using (Form11 f8 = new Form11())
                    {

                        f8.ShowDialog(this);

                    }

               

            }

        }

        private void Form8_Load(object sender, EventArgs e)
        {
            this.KeyDown += new KeyEventHandler(Form8_KeyDown);
            label1.Text = string.Format("Timp scurs: {0:hh\\:mm\\:ss}", watch.Elapsed);
            label2.Text = DateTime.Now.ToString();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["Facturi"])//your specific tabname
            {
                tabel = 1;
            }

            if (tabControl1.SelectedTab == tabControl1.TabPages["Produse"])//your specific tabname
            {
                tabel = 2;
            }

            if (tabControl1.SelectedTab == tabControl1.TabPages["Calculatoare"])//your specific tabname
            {
                tabel = 3;
            }

            if (tabControl1.SelectedTab == tabControl1.TabPages["Telefoane"])//your specific tabname
            {
               
                tabel = 4;
            }
        }

        Stopwatch watch = Form1.watch;
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = string.Format("Timp scurs: {0:hh\\:mm\\:ss}", watch.Elapsed);
            label2.Text = DateTime.Now.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Doriți să închideți aplicația?", "Deconectare", MessageBoxButtons.YesNo);
            {
                switch (r)
                {
                    case DialogResult.Yes:
                        {
                            using (Form1 f1 = new Form1())
                            {
                                this.Hide();
                                f1.ShowDialog(this);
                                this.Close();
                            }
                            break;
                        }
                    case DialogResult.No:
                        {

                            break;
                        }
                }
            }
        }


        public static int pretgrape;
        public static int idgrape;
        public static string tipgrape;
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (dataGridView2.CurrentCell.ColumnIndex == 0)
            {
                pretgrape = int.Parse(dataGridView2.Rows[e.RowIndex].Cells["CPret"].Value.ToString());
                idgrape = int.Parse(dataGridView2.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                tipgrape= dataGridView2.Rows[e.RowIndex].Cells["CTip"].Value.ToString();
                using (Form9 f9 = new Form9())
                {
                    f9.ShowDialog(this);
                }
            }
        }

        private void Form8_MouseEnter(object sender, EventArgs e)
        {
            populateDGV();
            populateDGV2();
            populateDGV3();
            populateDGV4();
        }

    }
}
