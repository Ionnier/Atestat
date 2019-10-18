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
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Form4_KeyDown);
            watch2.Start();
            
        }

        private void Form4_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btn_logout.PerformClick();
            }
        } 

        private void btn_logout_Click(object sender, EventArgs e)
        {

            DialogResult r = MessageBox.Show("Doriți să vă deconectați?", "Deconectare", MessageBoxButtons.YesNo);
            
            switch (r)
                {
                    case DialogResult.Yes:
                        {
                        watch2.Stop();
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
        Stopwatch watch = Form1.watch;
        public static Stopwatch watch2 = new System.Diagnostics.Stopwatch ();
     
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = string.Format("Time elapsed: {0:hh\\:mm\\:ss}", watch.Elapsed);
            label3.Text = DateTime.Now.ToString();
            label4.Text = string.Format("Timp petrecut uitandu-va la statistici acum: {0:hh\\:mm\\:ss} ", watch2.Elapsed);
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void Save(string time)
        {
            using (StreamWriter write = new StreamWriter("yes.txt"))
            {
                write.WriteLine(time);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string text = string.Format("{0:hh\\:mm\\:ss}", watch2.Elapsed);
            Console.WriteLine(text);
            Save(text);
        }
    } 
}

