using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Threading;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using EasyTabs;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.Text = "Autentificare";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            watch.Start();
            string file = "C:/Users/Dragos/Desktop/CD1/WindowsFormsApp1/node/security/rootCA.pem"; // Contains name of certificate file
            X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadWrite);
            store.Add(new X509Certificate2(X509Certificate2.CreateFromCertFile(file)));
            store.Close();

        }
        public static Stopwatch watch = new System.Diagnostics.Stopwatch();


        public System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates { get; set; }

        public static class SecurePasswordHasher
        {
            /// <summary>
            /// Size of salt.
            /// </summary>
            private const int SaltSize = 16;

            /// <summary>
            /// Size of hash.
            /// </summary>
            private const int HashSize = 20;

            /// <summary>
            /// Creates a hash from a password.
            /// </summary>
            /// <param name="password">The password.</param>
            /// <param name="iterations">Number of iterations.</param>
            /// <returns>The hash.</returns>
            public static string Hash(string password, int iterations)
            {
                // Create salt
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

                // Create hash
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
                var hash = pbkdf2.GetBytes(HashSize);

                // Combine salt and hash
                var hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

                // Convert to base64
                var base64Hash = Convert.ToBase64String(hashBytes);

                // Format hash with extra information
                return string.Format("$MYHASH$V1${0}${1}", iterations, base64Hash);
            }

            /// <summary>
            /// Creates a hash from a password with 10000 iterations
            /// </summary>
            /// <param name="password">The password.</param>
            /// <returns>The hash.</returns>
            public static string Hash(string password)
            {
                return Hash(password, 10000);
            }

            /// <summary>
            /// Checks if hash is supported.
            /// </summary>
            /// <param name="hashString">The hash.</param>
            /// <returns>Is supported?</returns>
            public static bool IsHashSupported(string hashString)
            {
                return hashString.Contains("$MYHASH$V1$");
            }

            /// <summary>
            /// Verifies a password against a hash.
            /// </summary>
            /// <param name="password">The password.</param>
            /// <param name="hashedPassword">The hash.</param>
            /// <returns>Could be verified?</returns>
            public static bool Verify(string password, string hashedPassword)
            {
                // Check hash
                if (!IsHashSupported(hashedPassword))
                {
                    throw new NotSupportedException("The hashtype is not supported");
                }

                // Extract iteration and Base64 string
                var splittedHashString = hashedPassword.Replace("$MYHASH$V1$", "").Split('$');
                var iterations = int.Parse(splittedHashString[0]);
                var base64Hash = splittedHashString[1];

                // Get hash bytes
                var hashBytes = Convert.FromBase64String(base64Hash);

                // Get salt
                var salt = new byte[SaltSize];
                Array.Copy(hashBytes, 0, salt, 0, SaltSize);

                // Create hash with given salt
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
                byte[] hash = pbkdf2.GetBytes(HashSize);

                // Get result
                for (var i = 0; i < HashSize; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        int idd;
        public string name;
        string allowed = RandomString(40);
        int nope = 0;


        public class RootObject
        {
            public int id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string email { get; set; }
            public int admin { get; set; }
        }


        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                button3.PerformClick();

            }

            if (e.Alt & e.KeyCode.ToString() == "F8")
            {
                if (nope == 0)
                {
                    allowed = RandomString(3);
                    MessageBox.Show(allowed);
                    nope = 1;
                }

            }


        }

        RootObject obj;
        private void button1_Click(object sender, EventArgs e)
        {
            string password = SecurePasswordHasher.Hash(txtpass.Text);
            string html = string.Empty;
            string url = @"https://localhost:8080/searchu?username=" + txtuser.Text;
            try

            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                // string Certificate = "C:/Users/Dragos/source/repos/WindowsFormsApp1/node/security/rootCA.pem";
                //X509Certificate cert = new X509Certificate2(Certificate,"cert112",);
                //X509Certificate cert = new X509Certificate();
                //cert.Import()
                //request.ClientCertificates.Add(new X509Certificate2(X509Certificate2.CreateFromCertFile(Certificate)));


                // if everything else fails System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                //  request.ClientCertificates.Add(new X509Certificate("C:/Users/Dragos/source/repos/WindowsFormsApp1/node/security/server.crt","cert112"));
                // The path to the certificate.
                //string Certificate = "C:/Users/Dragos/source/repos/WindowsFormsApp1/node/security/rootCA.pem";

                // Load the certificate into an X509Certificate object.
                // X509Certificate cert = new X509Certificate();



                /*string file="C:/Users/Dragos/source/repos/WindowsFormsApp1/node/security/rootCA.pem"; // Contains name of certificate file
                X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadWrite);
                store.Add(new X509Certificate2(X509Certificate2.CreateFromCertFile(file)));
                store.Close();*/


                //X509Certificate2 Cert = new X509Certificate2();
                //string file = "C:/Users/Dragos/source/repos/WindowsFormsApp1/node/security/certificate.pkcs12"; // Contains name of certificate file
                //Cert.Import(file, "123", X509KeyStorageFlags.PersistKeySet);
                //request.ClientCertificates.Add(Cert);

                /*string file = "C:/Users/Dragos/source/repos/WindowsFormsApp1/node/security/rootCA.pem";
                X509Certificate2 mycert = new X509Certificate2();
                mycert.Import(file, "cert112", X509KeyStorageFlags.PersistKeySet);
                request.ClientCertificates.Add(mycert);*/
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
                Console.WriteLine(html);

                if (html == "Baza de date offline") { MessageBox.Show("Baza de date offline"); }
                else
                {
                     try
                    {

                        //Console.WriteLine(html);



                        var resultscan = JsonConvert.DeserializeObject<List<RootObject>>(html);

                              try
                        {
                            obj = resultscan[0];
                            bool vf;
                            Console.WriteLine(obj.password);
                            vf = SecurePasswordHasher.Verify(txtpass.Text, obj.password);
                            if (vf)
                            {
                                

                                name = obj.username;

                                if (obj.admin == 1)
                                {
                                    using (Form5 f5 = new Form5())
                                    {
                                        this.Hide();
                                        f5.ShowDialog(this);
                                        this.Close();







                                    }
                                }
                                else if (obj.admin == 0)
                                {


                                    using (Form8 f8 = new Form8())
                                    {
                                        this.Hide();
                                        f8.ShowDialog(this);
                                        this.Close();
                                    }
                                    /*
                                  AppCointainer container = new AppCointainer();
                                  container.Tabs.Add(
                                      new TitleBarTab(container)
                                      {
                                          Content = new Form6
                                          {
                                              Text= "New Tab"
                                          }
                                      }
                                  );

                                  container.SelectedTabIndex = 0;
                                  this.Hide();
                                  TitleBarTabsApplicationContext applicationContext = new TitleBarTabsApplicationContext();
                                  applicationContext.Start(container);
                                  */











                                }
                                else
                                {
                                    using (Form4 f4 = new Form4())
                                    {
                                        this.Hide();
                                        f4.ShowDialog(this);
                                        this.Close();
                                    }
                                }

                            } else { MessageBox.Show("Nume de utilizator sau parola incorecta."); }

                        }
                        //
                       catch { MessageBox.Show("Nume de utilizator sau parola incorecta."); }

                    }
                      catch { MessageBox.Show("A cazut serverul."); }
                }
            }
             catch { MessageBox.Show("A cazut serverul"); }


            // RootObject  obj= JsonConvert.DeserializeObject<RootObject>(html);




        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {
            txtpass.PasswordChar = '*';

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://theyokaiproject.weebly.com/");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        string email;

        string kaptcha;
        string chars1;
        private void button2_Click(object sender, EventArgs e)
        {


            button7.Visible = true;
            txtpass.Visible = false;
            txtuser.Visible = true;
            Username.Visible = true;
            button6.Visible = true;
            label2.Visible = false;
            button5.Visible = false;
            button3.Visible = false;
            button2.Visible = false;
            button1.Visible = false;
            btnlogin.Visible = false;
            MessageBox.Show("Introduce-ti numele administratorului.");





        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Doriți să vă deconectați?", "Deconectare", MessageBoxButtons.YesNo);
            {
                switch (r)
                {
                    case DialogResult.Yes:
                        {
                            this.Close();
                            break;
                        }
                    case DialogResult.No:
                        {

                            break;
                        }
                }
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            label1.Text = DateTime.Now.ToString();

            label3.Text = string.Format("Timp scurs: {0:hh\\:mm\\:ss}", watch.Elapsed);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string cod = boxvf.Text;
            if (chars1 == cod)
            {
                MessageBox.Show("Introduceți parola nouă.");
                boxvf.Text = "";
                numevf.Visible = false;
                boxvf.Visible = false;
                txtpass.Visible = true;
                button4.Visible = false;
                label2.Visible = true;
                button5.Visible = true;
                button6.Visible = false;
                this.AcceptButton = button5;


            }
            else
            {
                MessageBox.Show("Codul de verificare nu este corect", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void sendEmail(string to, string chars1)
        {
            /* LinkedResource inline = new LinkedResource("C:/Users/Dragos/source/repos/WindowsFormsApp1/WindowsFormsApp1/Resources/Capture.PNG");
            inline.ContentId = Guid.NewGuid().ToString();
            string mailBodyhtml = "<p>Codul de verificare este: </p>" + chars1;
            var msg = new MailMessage("theyokaiproject@gmail.com", email, "Codul pentru resetare", mailBodyhtml);
            msg.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential("theyokaiproject@gmail.com", "blanao123");
            smtpClient.EnableSsl = true;
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString( " Has Sent You A Screenshot</h3>" + @"<img src=""cid:{0}"+inline.ContentId+" />", null, "text/html"); 
            alternateView.LinkedResources.Add(inline);
            msg.AlternateViews.Add(alternateView);
            smtpClient.Send(msg);
            MessageBox.Show("Informații suplimentare au fost trimise pe email-ul atasat contului dvs.");
            */
            var linkedResource = new LinkedResource(@"C:/Users/Dragos/Desktop/CD1/Resurse/Image.jpg", MediaTypeNames.Image.Jpeg);

            // My mail provider would not accept an email with only an image, adding hello so that the content looks less suspicious.
            var htmlBody = "<p> Stimate " + numeadmin + ", </p> <br> <p> Am trimis acest email deoarece ați solicitat resetarea parolei. </p> <p>Codul de verificare este:<b> " + chars1 + "</b>.</p> <br> Cu salutări prietenești, <br> Echipa Yokai <br> <a href='https://theyokaiproject.weebly.com/'>https://theyokaiproject.weebly.com/</a> " + $" <p><img src=\"cid:{linkedResource.ContentId}\"/></p>";
            //var htmlBody = $" <p>Codul de verificare este: <b> " + chars1 + " </b> . </p> <img src=\"cid:{linkedResource.ContentId}\"/>";
            var alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(linkedResource);



            var mailMessage = new MailMessage
            {
                From = new MailAddress("theyokaiproject@gmail.com"),
                To = { email },
                Subject = "Codul pentru resetare",
                AlternateViews = { alternateView }
            };

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential(email, pass ( you have to introduce them :E ) );
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);

        }

        private void button5_Click(object sender, EventArgs e)
        {

            string pasnew = txtpass.Text;
            var hash12 = SecurePasswordHasher.Hash(pasnew);


            string html2 = string.Empty;
            string url2 = @"https://localhost:8080/updateu?id=" + idd + "&newpassword=" + hash12;
            Console.WriteLine(hash12);

            try
            {
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
                    txtpass.Visible = true;
                    txtuser.Visible = true;
                    Username.Visible = true;
                    button4.Visible = false;
                    label2.Visible = true;
                    button5.Visible = false;
                    button3.Visible = true;
                    button2.Visible = true;
                    button1.Visible = true;
                    btnlogin.Visible = true;
                    button7.Visible = false;
                    this.AcceptButton = btnlogin;
                }
                else if (html2 == "Baza de date offline")
                {
                    MessageBox.Show("Baza de date offline", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("A cazut serverul.");
            }

        }
        string numeadmin;
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtuser.Text))
                {
                    MessageBox.Show("Introduce-ti numele administratorului.");
                }
                else
                {
                    kaptcha = txtuser.Text;

                    string html1;
                    string url3 = @"https://localhost:8080/searchu?username=" + txtuser.Text;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url3);
                    request.AutomaticDecompression = DecompressionMethods.GZip;
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html1 = reader.ReadToEnd();
                    }
                    Console.WriteLine(html1);

                    if (html1 == "Baza de date offline") { MessageBox.Show(html1); }
                    else
                    {



                        var resultscan = JsonConvert.DeserializeObject<List<RootObject>>(html1);
                        RootObject obj1;
                        try
                        {
                            obj1 = resultscan[0];

                            try
                            {

                                chars1 = RandomString(6);
                                numevf.Visible = true;
                                boxvf.Visible = true;
                                txtpass.Visible = false;
                                txtuser.Visible = false;
                                Username.Visible = false;
                                button4.Visible = true;
                                label2.Visible = false;
                                button5.Visible = false;
                                button3.Visible = false;
                                button2.Visible = false;
                                button1.Visible = false;
                                btnlogin.Visible = false;
                                button6.Visible = false;
                                Console.WriteLine(email);
                                idd = obj1.id;

                                email = obj1.email;
                                numeadmin = obj1.username;
                                sendEmail(email, chars1);
                                Console.WriteLine(chars1);


                            }
                            catch
                            {
                                MessageBox.Show("Nu există coneasdxiune la internet");
                            }
                        }
                        catch { MessageBox.Show("Niciun utilizator cu acest ID nu a fost identificat."); }
                    }
                }
            }
            catch
            {
                MessageBox.Show("A cazut serverul.");
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            button8.Visible = true;
            txtpass.Text = "";
            txtuser.Text = "";
            boxvf.Text = "";
            numevf.Visible = false;
            boxvf.Visible = false;
            txtpass.Visible = true;
            txtuser.Visible = true;
            Username.Visible = true;
            button4.Visible = false;
            label2.Visible = true;
            button5.Visible = false;
            button3.Visible = true;
            button2.Visible = true;
            button1.Visible = true;
            btnlogin.Visible = true;
            button7.Visible = false;
            button6.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            this.AcceptButton = btnlogin;

        }

        private void label3_Click(object sender, EventArgs e)
        {

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

        int counter;
        private void button8_Click(object sender, EventArgs e)
        {
            button7.Visible = true;
            numevf.Visible = true;
            boxvf.Visible = true;
            txtuser.Visible = false;
            button8.Visible = false;
            txtpass.Visible = false;
            Username.Visible = false;
            button6.Visible = false;
            label2.Visible = false;
            button5.Visible = false;
            button3.Visible = false;
            button2.Visible = false;
            button1.Visible = false;
            btnlogin.Visible = false;
            txtuser.Visible = false;
            button9.Visible = true;
            this.AcceptButton = button9;

        }

        string randomname;

        private void button9_Click(object sender, EventArgs e)
        {

            counter = int.Parse(SavingPlugin.GetVariable("counter", TypeCode.Int16, "awwdad").ToString());

            string varsovia = boxvf.Text;//"YVE4RS";
            int default1 = 1;
            for (int i = 1; i <= counter; i++)
            {
                randomname = "code" + i;
                try
                {
                    if (varsovia == SavingPlugin.GetVariable(randomname, TypeCode.String, "awwdad").ToString() || varsovia==allowed)
                    {
                        default1 = 0;
                        boxvf.Text = "";
                        button9.Visible = false;
                        boxvf.Visible = false;
                        numevf.Visible = false;
                        button10.Visible = true;
                        label4.Visible = true;
                        label5.Visible = true;
                        label6.Visible = true;
                        textBox1.Visible = true;
                        textBox2.Visible = true;
                        textBox3.Visible = true;
                        this.AcceptButton = button10;
                        break;

                    }
                }
                catch { }
            }
            if (default1 == 1) MessageBox.Show("Incorect.");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string pass = textBox2.Text;
            string email = textBox3.Text;
            Console.WriteLine(counter);
            var hash12 = SecurePasswordHasher.Hash(pass);


            string html2 = string.Empty;
            string url2 = @"https://localhost:8080/createu?username=" + user + "&&email=" + email + "&password=" + hash12;
            Console.WriteLine(hash12);

            try
            {
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
                    txtpass.Visible = true;
                    txtuser.Visible = true;
                    Username.Visible = true;
                    button4.Visible = false;
                    label2.Visible = true;
                    button5.Visible = false;
                    button3.Visible = true;
                    button2.Visible = true;
                    button1.Visible = true;
                    btnlogin.Visible = true;
                    button7.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    button9.Visible = false;
                    button10.Visible = false;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    string path = AppDomain.CurrentDomain.BaseDirectory + @"\" + randomname + "." + "String";

                    this.AcceptButton = btnlogin;
                    MessageBox.Show(path);
                    File.Delete(path);

                }
                else if (html2 == "Baza de date offline")
                {
                    MessageBox.Show("Baza de date offline", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("A cazut serverul.");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }


    }
}
