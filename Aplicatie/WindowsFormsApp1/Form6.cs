using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using EasyTabs;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;

namespace WindowsFormsApp1
{
    public partial class Form6 : Form
    {

        protected TitleBarTabs ParentTabs
        {
            get
            {
                return (ParentForm as TitleBarTabs);
            }

        }

        public Form6()
        {
            InitializeComponent();
            this.Text = "User Form";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            populateDGV();
            populateDGV2();
        }

        int tabel = 0;
        private void Form6_Load(object sender, EventArgs e)
        {
            this.KeyDown += new KeyEventHandler(Form6_KeyDown);


        }

        public class Bills
        {
            public int ID { get; set; }
            public int Valoare { get; set; }
            public int Zi { get; set; }
            public int Luna { get; set; }
            public int An { get; set; }
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




        private void panel1_MouseHover(object sender, EventArgs e)
        {

        }

        private void Form6_MouseHover(object sender, EventArgs e)
        {
            this.panel1.Size = new Size(0, 0);
            this.panel2.Size = new Size(0, 0);
            tabel = 0;
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            this.panel1.Size = new Size(614, 267);
            this.panel2.Size = new Size(0, 0);
            tabel = 1;


        }

        

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
        int counter = 0;
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

        

        private void Form6_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //button2.PerformClick();

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

                } else if (tabel == 2)
                {
                    string html2 = string.Empty;
                    int zi = DateTime.Today.Day;
                    int an = DateTime.Today.Year;
                    int luna = DateTime.Today.Month;
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

                            string html23 = string.Empty;
                            string url23 = @"https://localhost:8080/deletegridproduct?id=" + id;
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

                            }
                            else if (form7.SelectedText == "PDF"){

                                if (tabel == 1)
                                {
                                    SavePDF(dataGridView1, "Facturi");

                                }
                                else
                                if (tabel == 2)
                                {
                                    SavePDF(dataGridView2, "Produse");
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Doriți să vă deconectați?", "Deconectare", MessageBoxButtons.YesNo);

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        Stopwatch watch = Form1.watch;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //label1.Text = string.Format("Timp scurs: {0:hh\\:mm\\:ss}", watch.Elapsed);
            //label2.Text = DateTime.Now.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            populateDGV();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            timer2.Stop();
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void SaveToCSV(DataGridView DGV)
        {
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.FileName = "Output.csv";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Data will be exported and you will be notified when it is ready.");
                if (File.Exists(filename))
                {
                    try
                    {
                        File.Delete(filename);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
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
                MessageBox.Show("Your file was generated and its ready for use.");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            tabel = 1;
            dataGridView1.Dock = DockStyle.Fill;
            panel1.Dock = DockStyle.Fill;
            panel1.BringToFront();
            dataGridView1.BringToFront();
        }

        public class Products
        {
            public int ID { get; set; }
            public string CPret { get; set; }
            public int Cantitate { get; set; }
            public string CTip { get; set; }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            tabel = 2;
            dataGridView2.Dock = DockStyle.Fill;
            panel2.Dock = DockStyle.Fill;
            panel2.BringToFront();
            dataGridView2.BringToFront();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(602, 215);
            this.panel1.Size = new Size(0, 0);
            tabel = 2;
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
        int counterrr;
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

                string cpret;
                string ctip;
                int cantitate;
                int id;


                cpret = dataGridView2.Rows[e.RowIndex].Cells["CPret"].Value.ToString();
                id = int.Parse(dataGridView2.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                cantitate = int.Parse(dataGridView2.Rows[e.RowIndex].Cells["Cantitate"].Value.ToString());
                ctip = dataGridView2.Rows[e.RowIndex].Cells["CTip"].Value.ToString();




                string html2 = string.Empty;
                string url2 = @"https://localhost:8080/updategridproduct?pret=" + cpret + "&cantitate=" + cantitate + "&tip=" + ctip + "&id=" + id;
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
           

            Paragraph heading = new Paragraph("Tabel de Date    - " +  namae);


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


        private void SavePDF(DataGridView datagridview,string namaee)
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



        }
    }


