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
using System.Security.Cryptography;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            watch.Start();
            this.KeyDown += new KeyEventHandler(Form3_KeyDown);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string chars1;
            chars1 = RandomString(6);
            MessageBox.Show(chars1);
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
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
        Stopwatch watch = Form1.watch;
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Doriți să vă deconectați?", "Deconectare", MessageBoxButtons.YesNo);
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

        private void label2_Click(object sender, EventArgs e)
        {



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = string.Format("Time elapsed: {0:hh\\:mm\\:ss}", watch.Elapsed);
            label3.Text = DateTime.Now.ToString();
        }
        private void Form3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                button2.PerformClick();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var password = textBox1.Text;
            var hash = SecurePasswordHasher.Hash(password);
            var hash1 = SecurePasswordHasher.Hash(password);

            var password2 = textBox2.Text;
            var hash4 = SecurePasswordHasher.Hash(password2);
            var result = SecurePasswordHasher.Verify("avc", hash);
            if (result) { MessageBox.Show("1"); }
            Console.WriteLine(hash4);
            Console.WriteLine(hash);
        }

        private void button4_Click(object sender, EventArgs e)
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

        int counter=0;
        private void button5_Click(object sender, EventArgs e)
        {
            counter = int.Parse(SavingPlugin.GetVariable("counter", TypeCode.Int16, "awwdad").ToString());
            counter++;
            SavingPlugin.SaveVariable("counter", TypeCode.Int16, counter, "awwdad");

            string randomname = "code" + counter;
            Console.WriteLine(counter);
            SavingPlugin.SaveVariable(randomname, TypeCode.String, RandomString(6), "awwdad");
            // write lines of text to the file
            MessageBox.Show(SavingPlugin.GetVariable(randomname, TypeCode.String, "awwdad").ToString());

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            MessageBox.Show(RunningPath);
        }
    }
}