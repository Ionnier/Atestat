using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            watch.Start();
            this.KeyDown += new KeyEventHandler(Form2_KeyDown);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;pwd=rootpassword;Initial Catalog='pcgarage';database=pcgarage; SslMode=none");
        MySqlCommand command;
        private void Form2_Load(object sender, EventArgs e)
        {
            populateDGV();
        }
              

        public void populateDGV()
        {
            string selectQuery = "SELECT * FROM produs";
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, conn);
            adapter.Fill(table);
            dataprodus.DataSource = table;
        }
        

        private void dataprodus_MouseClick(object sender, MouseEventArgs e)
        {
            txttip.Text = dataprodus.CurrentRow.Cells[1].Value.ToString();
            txtpret.Text = dataprodus.CurrentRow.Cells[2].Value.ToString();
            txtcantitate.Text = dataprodus.CurrentRow.Cells[3].Value.ToString();

        }

        public void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public void closeConnection()
        {
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        
        public void executeMyQuery (string query)
        {
            try
            {
                openConnection();
                command = new MySqlCommand(query, conn);
                if (command.ExecuteNonQuery() == 1)
                {
                    txttip.Text = "";
                    txtpret.Text = "";
                    txtcantitate.Text = "";
                    MessageBox.Show("Executat");
                
                } else
                {
                    MessageBox.Show("Eroare");
                }
            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                closeConnection();

            }

        }

        private void buttoninsert_Click(object sender, EventArgs e)
        {
            string insertQuery = "INSERT INTO produs(tip,pret,cantitate) VALUES('"+txttip.Text+ "','" + txtpret.Text + "','" + txtcantitate.Text + "' )";
            executeMyQuery(insertQuery);
            populateDGV();
            
        }

        private void buttonupdate_Click(object sender, EventArgs e)
        {
            string updateQuery = "UPDATE produs SET tip='" + txttip.Text + "' , pret = '" + txtpret.Text + "' , cantitate= '" + txtcantitate.Text + "' WHERE cod_produs=" + int.Parse(dataprodus.CurrentRow.Cells[0].Value.ToString()); ;
            executeMyQuery(updateQuery); 
            populateDGV();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string deleteQuery = "DELETE FROM `produs` WHERE cod_produs= " + int.Parse(dataprodus.CurrentRow.Cells[0].Value.ToString());
            executeMyQuery(deleteQuery);
            populateDGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlDataReader mdr;
            string select = " SELECT * FROM produs WHERE cod_produs = " + txtid.Text;
            command = new MySqlCommand(select, conn);
            openConnection();
            mdr = command.ExecuteReader();

            if (mdr.Read())
            {
                txtcantitate.Text = mdr.GetString("cantitate");
                txtpret.Text = mdr.GetString("pret");
                txttip.Text = mdr.GetString("tip");
                txtid.Text = "";
            } else
            {

                MessageBox.Show("Produsul nu este gasit sau nu este in baza de date");
            }
            closeConnection();  
        }

        private void dataprodus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtcantitate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txttip_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult r  =  MessageBox.Show("Doriți să vă deconectați?", "Deconectare", MessageBoxButtons.YesNo);
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
                    case DialogResult.No :
                        {
                            
                            break;
                        }
                }
            } 
            

            
        }
        Stopwatch watch =  Form1.watch;
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            label5.Text = string.Format("Time elapsed: {0:hh\\:mm\\:ss}", watch.Elapsed);
            label6.Text = DateTime.Now.ToString(); 

        }
        
        private void Form2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                button3.PerformClick();
            }
        }
            private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void txtid_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
