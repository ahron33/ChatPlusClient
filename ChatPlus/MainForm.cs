using FluentFTP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace ChatPlus
{
    public partial class MainForm : Form
    {
        public string url = "82.198.229.125";
        public string user = "u158938735";
        public string pass = "ch!a+=rm/9gZ$4Cr";
        public string path = "/domains/darkorange-raven-406010.hostingersite.com/public_html/ChatPlus/";
        public int counts = 0;
        private string lastChat = "";
        private static readonly HttpClient http = new HttpClient();
        public MainForm()
        {
            InitializeComponent();
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

        public async void SendNessage1Async()
        {
            if (typebox.Text.Length > 150)
            {
                //  Display an error message if the message is too long
                label2.Text = DateTime.Now + " - Message is too long. Please limit your message to 150 characters.";
            }
            else if (typebox.Text.Length < 1)
            {
                //  Display an error message if the message is too short
                label2.Text = DateTime.Now + " - Message is too short. Please enter at least 1 character.";
            }
            else  if (typebox.Text.Length >= 1 && typebox.Text.Length <= 150)
            {
                //  Send the message in the chatbox

                typebox.Enabled = false;
                button1.Enabled = false;
                var client = new AsyncFtpClient(url, user, pass);
                await client.Connect();


                string nickname = await http.GetStringAsync("https://darkorange-raven-406010.hostingersite.com/ChatPlus/Users/" + GetMotherboardUUID() + "/nickname.txt");
                byte[] bytes = Encoding.UTF8.GetBytes($"{ToBoldSans(nickname)} {DateTime.Now}\n{typebox.Text}");
                await client.UploadBytes(bytes, path + "Users/" + GetMotherboardUUID() + "/chat.txt");

                byte[] bytes2 = Encoding.UTF8.GetBytes("Pending");
                await client.UploadBytes(bytes2, path + "Users/" + GetMotherboardUUID() + "/status.txt");

                await client.Disconnect();



                Console.WriteLine("https://darkorange-raven-406010.hostingersite.com/ChatPlus/Users/" + GetMotherboardUUID().Trim() + "/status.txt");
                while (true)
                {
                    counts++;

                    await Task.Delay(700);

                    if (await http.GetStringAsync("https://darkorange-raven-406010.hostingersite.com/ChatPlus/Users/" + GetMotherboardUUID().Trim() + "/status.txt") == "Pending")
                    {
                        typebox.Enabled = true;
                        button1.Enabled = true;
                        typebox.Clear();
                        label2.Text = DateTime.Now + " - Message Sent";
                        break;
                    }

                    Console.WriteLine("Status: " );
                }
                MessageBox.Show("message sentout ?");
                //SendNessageFixedAsync();
            }
        }
        public async Task<bool> SendNessage2Async()
        {
            if (counts < 20)
            {
                counts++;

                await Task.Delay(700);

                WebClient web = new WebClient();
                string status = web.DownloadString("https://darkorange-raven-406010.hostingersite.com/ChatPlus/Users/" + GetMotherboardUUID() + "/status.txt");

                if (status == "MessageSent")
                {
                    typebox.Enabled = true;
                    typebox.Clear();
                    label2.Text = DateTime.Now + " - Message Sent";
                    return false;
                }

                Console.WriteLine("Status: " + status);

                return await SendNessage2Async();
            }
            else if (counts > 20)
            {
                typebox.Enabled = true;
                typebox.Clear();
                label2.Text = DateTime.Now + " - Did not send the Message";
                return false;
            }
            return true;
        }


        public async Task<bool> SendNessageFixedAsync()
        {
            WebClient web = new WebClient();

            while (typebox.Enabled == false)
            {
                await Task.Delay(700);

                string status = web.DownloadString("https://darkorange-raven-406010.hostingersite.com/ChatPlus/Users/" + GetMotherboardUUID() + "/status.txt");


                if (status == "MessageSent")
                {
                    MessageBox.Show("ff");
                    typebox.Enabled = true;
                    typebox.Clear();
                    label2.Text = DateTime.Now + " - Message Sent";
                    return false;
                }

                Console.WriteLine("Status: " + status);
            }



            /*if (counts < 20)
            {
                counts++;

                await Task.Delay(700);

                WebClient web = new WebClient();
                string status = web.DownloadString("https://darkorange-raven-406010.hostingersite.com/ChatPlus/Users/" + GetMotherboardUUID() + "/status.txt");

                if (status == "MessageSent")
                {
                    typebox.Enabled = true;
                    typebox.Clear();
                    label2.Text = DateTime.Now + " - Message Sent";
                    return false;
                }

                Console.WriteLine("Status: " + status);

                return await SendNessage2Async();
            }
            else if (counts > 20)
            {
                typebox.Enabled = true;
                typebox.Clear();
                label2.Text = DateTime.Now + " - Did not send the Message";
                return false;
            }*/
            return false;
        }


        public async void ChatPlusIsOnline()
        {
            while (true)
            {
                WebClient web = new WebClient();
                string chat = web.DownloadString("https://darkorange-raven-406010.hostingersite.com/ChatPlus/Chat/chat.txt");
                await Task.Delay(200);
                richTextBox1.Text = chat;
                await Task.Delay(300);
            }
        }


        private async Task ChatLoopAsync()
        {
            const string url = "https://darkorange-raven-406010.hostingersite.com/ChatPlus/Chat/chat.txt";

            while (true)
            {
                try
                {
                    string chat = await http.GetStringAsync(url + "?t=" + DateTime.UtcNow.Ticks);

                    if (chat != lastChat)
                    {
                        lastChat = chat;
                        UpdateChatBox(chat);
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception) { }

                try { await Task.Delay(500); }
                catch (OperationCanceledException) { break; }
            }
        }
        static string ToBoldSans(string s) =>
    string.Concat(s.Select(c =>
        c >= 'A' && c <= 'Z' ? char.ConvertFromUtf32(0x1D5D4 + c - 'A') :
        c >= 'a' && c <= 'z' ? char.ConvertFromUtf32(0x1D5EE + c - 'a') :
        c >= '0' && c <= '9' ? char.ConvertFromUtf32(0x1D7EC + c - '0') :
        c.ToString()));

        private void UpdateChatBox(string text)
        {
            // keep the scrollbar at the bottom only if the user was already there
            richTextBox1.Text = text;
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendNessage1Async();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ChatLoopAsync();
        }
    }
}
