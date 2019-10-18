using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            this.Text = "Admin Form";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }



        private void Form5_Load(object sender, EventArgs e)
        {
            
            populateDGV();
            this.KeyDown += new KeyEventHandler(Form5_KeyDown);
         
            
        }


        private void UpdateRow()
        {

            int counter;
            

            // Iterate through the rows, skipping the Starting Balance row.
            for (counter = 1; counter < (dataGridView1.Rows.Count - 1);counter++)
            {
                Console.WriteLine("da");
                MessageBox.Show(dataGridView1.Rows[counter - 1].Cells["username"].Value.ToString());
            }



        }

        private void Form5_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                 btn_dec.PerformClick();
                
            }

            if (e.KeyCode == Keys.Delete)
            {


                int counterr = 0;
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {


                    
                    int indexx = int.Parse(row.Index.ToString());

                    int id;
                    id = int.Parse(dataGridView1.Rows[indexx].Cells["id"].Value.ToString());

                    string html23 = string.Empty;
                    string url23 = @"https://localhost:8080/deletegridu?id=" + id;
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
                if (counterr  == 0)
                {
                    MessageBox.Show("Baza de date offline", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                {
                    populateDGV();
                }


            }


        }

        public class RootObject
        {
            public int id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string email { get; set; }
            public int admin { get; set; }
        }

        



        public void populateDGV()
        {

            string html = string.Empty;
            string url = @"https://localhost:8080/users";
            try
            {
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
                    dataGridView1.DataSource = result;
                }
                dataGridView1.Columns["id"].ReadOnly = true;
                dataGridView1.Columns["password"].ReadOnly = true;
            }
            catch
            {
                MessageBox.Show("A cazut serverul");
            }
            
         


        }
        Stopwatch watch = Form1.watch;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            label1.Text = string.Format("Timp scurs: {0:hh\\:mm\\:ss}", watch.Elapsed);
            label2.Text = DateTime.Now.ToString();
        }

        private void btn_dec_Click_1(object sender, EventArgs e)
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


        int counter = 0;

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (counter == 0)
            {
                int admin;
                int id;
                string username;
                string email;



                admin = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["admin"].Value.ToString());
                id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());
                username = dataGridView1.Rows[e.RowIndex].Cells["username"].Value.ToString();
                email = dataGridView1.Rows[e.RowIndex].Cells["email"].Value.ToString();



                string html2 = string.Empty;
                string url2 = @"https://localhost:8080/updategriduser?id=" + id + "&admin=" + admin + "&username=" + username + "&email=" + email;
                // https://localhost:8080/updategriduser?username=test&email=asd&admin=1&id=6

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
            } else {
                counter = 0;
            }


        }

        private void dataGridView1_CellErrorTextChanged(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("No.");
        }

        public class SavingPlugin
        {

            public static void SaveVariable(string savename, TypeCode tc, object value, string Encryption_password)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + @"\" + savename + "." + tc.ToString();

                if (!File.Exists(path))
                {
                    var myfile = File.Create(path);
                    myfile.Close();
                    string val = value.ToString();
                    string encrypted = StringCipher.Encrypt(val, Encryption_password);
                    File.WriteAllText(path, encrypted);
                    File.SetAttributes(path, FileAttributes.Hidden);

                }
                else
                {
                    string txt = "";
                    try
                    {
                        txt = StringCipher.Decrypt(File.ReadAllText(path), Encryption_password);
                        File.SetAttributes(path, FileAttributes.Normal);
                        string val = value.ToString();
                        string encrypted = StringCipher.Encrypt(val, Encryption_password);
                        File.WriteAllText(path, encrypted);
                        File.WriteAllText(path, encrypted);
                        File.SetAttributes(path, FileAttributes.Hidden);
                    }
                    catch
                    {
                        MessageBox.Show("Incorrect password : " + Encryption_password + " for the variable : " + savename + "." + tc.ToString());
                    }

                }
            }

            public static object GetVariable(string savename, TypeCode tc, string Encryption_password)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + @"\" + savename + "." + tc.ToString();
                File.SetAttributes(path, FileAttributes.Normal);
                string txt = "";

                try
                {
                    txt = StringCipher.Decrypt(File.ReadAllText(path), Encryption_password);
                    File.SetAttributes(path, FileAttributes.Hidden);
                    var value = Convert.ChangeType(txt, tc);
                    return value;
                }
                catch
                {
                    MessageBox.Show("Incorrect password : " + Encryption_password + " for the variable : " + savename + "." + tc.ToString());
                    return null;
                }


            }

            public static void DeleteVariable(string savename, TypeCode tc)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + @"\" + savename + "." + tc.ToString();
                File.SetAttributes(path, FileAttributes.Normal);
                File.Delete(path);
            }

            public static bool Exists(string savename, TypeCode tc)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + @"\" + savename + "." + tc.ToString();
                bool _true = true;

                try
                {
                    File.SetAttributes(path, FileAttributes.Normal);
                    File.SetAttributes(path, FileAttributes.Hidden);
                }
                catch
                {
                    _true = false;
                }

                return _true;
            }
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static class StringCipher
        {
            // This constant is used to determine the keysize of the encryption algorithm in bits.
            // We divide this by 8 within the code below to get the equivalent number of bytes.
            private const int Keysize = 256;

            // This constant determines the number of iterations for the password bytes generation function.
            private const int DerivationIterations = 1000;

            public static string Encrypt(string plainText, string passPhrase)
            {
                // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
                // so that the same Salt and IV values can be used when decrypting.  
                var saltStringBytes = Generate256BitsOfRandomEntropy();
                var ivStringBytes = Generate256BitsOfRandomEntropy();
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
                {
                    var keyBytes = password.GetBytes(Keysize / 8);
                    using (var symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.BlockSize = 256;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;
                        using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                                {
                                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                    cryptoStream.FlushFinalBlock();
                                    // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                    var cipherTextBytes = saltStringBytes;
                                    cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                    cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                    memoryStream.Close();
                                    cryptoStream.Close();
                                    return Convert.ToBase64String(cipherTextBytes);
                                }
                            }
                        }
                    }
                }
            }

            public static string Decrypt(string cipherText, string passPhrase)
            {
                // Get the complete stream of bytes that represent:
                // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
                var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
                // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
                var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
                // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
                var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
                // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
                var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

                using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
                {
                    var keyBytes = password.GetBytes(Keysize / 8);
                    using (var symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.BlockSize = 256;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;
                        using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                        {
                            using (var memoryStream = new MemoryStream(cipherTextBytes))
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                                {
                                    var plainTextBytes = new byte[cipherTextBytes.Length];
                                    var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                    memoryStream.Close();
                                    cryptoStream.Close();
                                    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                                }
                            }
                        }
                    }
                }
            }

            private static byte[] Generate256BitsOfRandomEntropy()
            {
                var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    // Fill the array with cryptographically secure random bytes.
                    rngCsp.GetBytes(randomBytes);
                }
                return randomBytes;
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


        int counters = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                counters = int.Parse(SavingPlugin.GetVariable("counter", TypeCode.Int16, "awwdad").ToString());
            }
            catch
            {
                counters = 0;
                SavingPlugin.SaveVariable("counter", TypeCode.Int16, counters, "awwdad");
            } 
                counters++;
            SavingPlugin.SaveVariable("counter", TypeCode.Int16, counters, "awwdad");

            string randomname = "code" + counters;
            
            SavingPlugin.SaveVariable(randomname, TypeCode.String, RandomString(6), "awwdad");
            // write lines of text to the file
            MessageBox.Show(SavingPlugin.GetVariable(randomname, TypeCode.String, "awwdad").ToString());

        }  

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
                

            
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            

            // Add the row to the rows collection.
            
            
        }
    }
}







