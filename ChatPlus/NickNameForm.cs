using FluentFTP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ChatPlus
{
    public partial class NickNameForm : Form
    {
        public string url = "";
        public string user = "";
        public string pass = "";
        public string path = "";
        public string urlhttp = "";


        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public static void Drag_Form(IntPtr Handle, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public NickNameForm()
        {

            InitializeComponent();

            /*if (DirectoryExists(url, user, pass))
            {
                Console.WriteLine("Directory exists!");
            }
            else
            {
                Console.WriteLine("Directory does not exist.");
            }
            if (FileExists(url, user, pass))
            {
                Console.WriteLine("File exists!");
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }*/
            Star2();

        }



        private async void submitbtn_Click(object sender, EventArgs e)
        {
            if (nickbox.Text.Length > 25)
            {
                label3.Text = "Error: Nickname too long";
            }
            else if (nickbox.Text.Length < 2)
            {
                label3.Text = DateTime.Now + " - Nickname too short";
            }
            else if (nickbox.Text.Length >= 2 && nickbox.Text.Length <= 25)
            {
                if (Regex.IsMatch(nickbox.Text, "^[A-Za-z]+$"))
                {
                    // Save the nickname to the server

                    var client = new AsyncFtpClient(url, user, pass);
                    await client.Connect();

                    byte[] bytes = Encoding.UTF8.GetBytes(nickbox.Text);
                    await client.UploadBytes(bytes, path + "Users/" + GetMotherboardUUID() + "/nickname.txt", FtpRemoteExists.Overwrite, createRemoteDir: true);

                    await client.Disconnect();

                    label3.Text = DateTime.Now + " - Name saved";

                    var rnd = new Random(DateTime.Now.Millisecond);
                    int ticks = rnd.Next(300, 1000);
                    await Task.Delay(ticks);

                    MainForm myForm = new MainForm();
                    this.Hide();
                    myForm.ShowDialog();
                    this.Close();

                }
                else if (!Regex.IsMatch(nickbox.Text, "^[A-Za-z]+$"))
                {
                    label3.Text = DateTime.Now + " - Use only English letters";
                }
            }
        }

        public async void Star2()
        {
            /*var client = new AsyncFtpClient(url, user, pass, 21);
            client.Config.LogToConsole = true;

            try
            {
                await client.Connect();
                Console.WriteLine("Connected");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
            }*/

            var client = new AsyncFtpClient(url, user, pass, 21);
            await client.Connect();
            if (!await client.DirectoryExists(path + "Users/" + GetMotherboardUUID()))
            {
                //New User
                // Create the directory for the user
                label3.Text = DateTime.Now + " - Welcome new user";
                await client.CreateDirectory(path + "Users/" + GetMotherboardUUID());


                if (!await client.FileExists(path + "Users/" + GetMotherboardUUID() + "/nickname.txt"))
                {
                    // Create the nickname.txt file for the user
                    await client.UploadBytes(new byte[0], path + "Users/" + GetMotherboardUUID() + "/nickname.txt"); 
                }
                if (!await client.FileExists(path + "Users/" + GetMotherboardUUID() + "/chat.txt"))
                {
                    // Create the chat.txt file for the user
                    await client.UploadBytes(new byte[0], path + "Users/" + GetMotherboardUUID() + "/chat.txt");
                }
                if (!await client.FileExists(path + "Users/" + GetMotherboardUUID() + "/status.txt"))
                {
                    // Create the status.txt file for the user
                    await client.UploadBytes(new byte[0], path + "Users/" + GetMotherboardUUID() + "/status.txt");
                }
            }
            else if (await client.DirectoryExists(path + "Users/" + GetMotherboardUUID()))
            {
                // The User already exits
                WebClient web = new WebClient();
                string nickname = web.DownloadString(urlhttp + GetMotherboardUUID() + "/nickname.txt");
                if (nickname == String.Empty)
                {
                    //MessageBox.Show("Error: Time to get yourself a nickname");
                }
                else if (nickname != String.Empty)
                {
                    //MessageBox.Show("1");
                    MainForm myForm = new MainForm();
                    this.Hide();
                    myForm.ShowDialog();
                    this.Close();
                }
                else { MessageBox.Show("Error: Could not retrieve nickname from server."); }
                //MessageBox.Show("30");
            }

            await client.Disconnect();
        }

        public async void Start()
        {

            if (!DirectoryExists(url + "/Users/" + GetMotherboardUUID(), user, pass))
            {
                MessageBox.Show("1");
                WebRequest request = WebRequest.Create(url + "/Users/" + GetMotherboardUUID());
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(user, pass);
                request.GetResponse();

                if (!FileExists(url + "/Users/" + GetMotherboardUUID() + "/nickname.txt", user, pass))
                {
                    FtpWebRequest request3 = (FtpWebRequest)WebRequest.Create(url + "/Users/" + GetMotherboardUUID() + "/nickname.txt");
                    request3.Method = WebRequestMethods.Ftp.UploadFile;
                    request3.Credentials = new NetworkCredential(user, pass);
                    request3.ContentLength = 0;
                    request3.GetRequestStream();
                }
                MessageBox.Show("2");
            }
            else if (DirectoryExists(url + "/Users/" + GetMotherboardUUID(), user, pass))
            {
                MessageBox.Show("3");
                Console.WriteLine(urlhttp + GetMotherboardUUID() + "/nickname.txt");
                MessageBox.Show("000");

                using (WebClient webClient = new WebClient())
                {
                    // Pretend to be a normal web browser so the server doesn't block you
                    webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                    string url = urlhttp + GetMotherboardUUID() + "/nickname.txt";
                    string result = webClient.DownloadString(url);

                    MessageBox.Show(result);
                }

                MessageBox.Show("result");
                // Already exists
                WebClient web = new WebClient();
                Console.WriteLine(urlhttp + GetMotherboardUUID() + "/nickname.txt");
                string nickname = web.DownloadString(urlhttp + GetMotherboardUUID() + "/nickname.txt");
                MessageBox.Show("Your nickname is: " + nickname);
                if (nickname == String.Empty)
                {
                    MessageBox.Show("Error: Start to get a nickname");
                }
                else if (nickname != String.Empty)
                {
                    MessageBox.Show("1");
                    MainForm myForm = new MainForm();
                    this.Hide();
                    myForm.ShowDialog();
                    this.Close();
                }
                else { MessageBox.Show("Error: Could not retrieve nickname from server."); }
                MessageBox.Show("30");
            }

            MessageBox.Show("0");




            ///

            await Task.Delay(1500000);





            
        }

        public static string GetMotherboardUUID()
        {
            string uuid = string.Empty;
            using (ManagementClass mc = new ManagementClass("Win32_ComputerSystemProduct"))
            using (ManagementObjectCollection moc = mc.GetInstances())
            {
                foreach (ManagementObject mo in moc)
                {
                    uuid = mo.Properties["UUID"]?.Value?.ToString();
                    break;
                }
            }
            return uuid;
        }

        static bool DirectoryExists(string ftpUrl, string username, string password)
        {
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(username, password);

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    return true;
                }
            }
            catch (WebException ex)
            {
                try
                {
                    // Error 550 means the directory/file was not found
                    if (ex.Response is FtpWebResponse response && response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        return false;
                    }
                    throw;
                }
                catch
                {
                    return false;
                }
            }
        }
        static bool FileExists(string ftpUrl, string username, string password)
        {
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.Credentials = new NetworkCredential(username, password);

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    return true;
                }
            }
            catch (WebException ex)
            {
                // Error 550 means the file was not found
                if (ex.Response is FtpWebResponse response && response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    return false;
                }
                throw;
            }
        }

        private void upPanel_MouseMove(object sender, MouseEventArgs e)
        {
            Drag_Form(Handle, e);
        }


    }
}
