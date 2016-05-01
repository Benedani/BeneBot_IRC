using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace IRCbot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Form1.CheckForIllegalCrossThreadCalls = false;
            this.FormClosed += new FormClosedEventHandler(OnClose);
            InitializeComponent();
        }

        #region vars
        public string SERVER;
        private int PORT = 6667;
        private string USER = "USER CSharpBot 8 * :I'm a C# irc bot"; 
        private string NICK = "MEOW";
        public string CHANNEL;
        public string password;
        public StreamWriter writer;
        NetworkStream stream;
        TcpClient irc;
        public StreamReader reader;
        Thread oThread;
        MsgHandler handlemsg;
        Thread thread2;
        DoRandomMsg randommsg;
        public List<string> users = new List<string> { };
        public Dictionary<string, sbyte> likes = new Dictionary<string, sbyte> { };
        #endregion

        public void AddUserToList(string name, bool update)
        {
            users.Add(name);
            if (!likes.ContainsKey(name))
            {
                if (File.Exists(@"C:\\BeneBot\benebot-likes.txt"))
                {
                    sbyte val = 0;
                    StreamReader read = new StreamReader(@"C:\\BeneBot\benebot-likes.txt");

                    while (true)
                    {
                        if (!read.EndOfStream)
                        {
                            string line = read.ReadLine();
                            if (line.Contains(name))
                            {
                                string[] args = line.Split(' ');
                                val = sbyte.Parse(args[2]);
                                break;
                            }
                        }
                        else
                        break;
                    }
                    read.Close();
                    likes.Add(name, val); // Get the saved value
                }
                else
                {
                    if (!Directory.Exists(@"C:\\BeneBot"))
                    {
                        Directory.CreateDirectory(@"C:\\BeneBot");
                    }
                    File.Create(@"C:\\BeneBot\benebot-likes.txt");
                    likes.Add(name, 0); // neutral
                }
            }
            if (update) UpdateUserList();
        }

        public void DelUserFromList(string name, bool update)
        {
            users.Remove(name);
            if (update) UpdateUserList();
        }

        public void UpdateUserList()
        {
            userlist_tbox.ResetText();
            foreach (string name in users)
            {
                userlist_tbox.AppendText(name + "\r\n");
            }
        }

        public string AppendText
        {
            get { return console_tbox.Text; }
            set { console_tbox.AppendText(value + "\r\n"); }
        }

        public void OnClose(object sender, EventArgs e)
        {
            if (oThread != null)
            {
                irc.Close();
                oThread.Abort();
                oThread.Join();
                thread2.Abort();
                thread2.Join();
            }
        }

        public void append(string text)
        {
            console_tbox.AppendText(text + "\r\n");
        }

        void Connect()
        {
            console_tbox.AppendText("Connecting...\r\n");
            SERVER = textBox1.Text;
            CHANNEL = textBox2.Text;
            password = logintbox.Text;
            try
            {
                irc = new TcpClient(SERVER, PORT);
                stream = irc.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                writer.WriteLine(USER);
                writer.Flush();
                writer.WriteLine("NICK " + NICK);
                writer.Flush();
                handlemsg = new MsgHandler(this);
                oThread = new Thread(new ThreadStart(handlemsg.Read));
                oThread.Start();
                randommsg = new DoRandomMsg(this);
                thread2 = new Thread(new ThreadStart(randommsg.ProcessRandomMsg));
                thread2.Start();
                connbutton.Text = "Disconnect";
            }
            catch
            {
                console_tbox.AppendText("Failed to join\r\n");
            }
        }

        void Disconnect()
        {
            console_tbox.AppendText("Disconnecting...\r\n");
            irc.Close();
            oThread.Abort();
            oThread.Join();
            oThread = null;
            thread2.Abort();
            thread2.Join();
            thread2 = null;
            users.Clear();
            UpdateUserList();
            connbutton.Text = "Connect";
        }

        private void onClickConnect(object sender, EventArgs e)
        {
            if (connbutton.Text == "Connect")
            {
                Connect();
            }
            else
            {
                Disconnect();
            }
        }

        private void onClickSend(object sender, EventArgs e)
        {
            SendMessage(sendmsg_box.Text);
        }

        void SendMessage(string text)
        {
            if (handlemsg.respondtouser)
            {
                if (sendmsg_box.Text != "")
                {
                    if (sendmsg_box.Text.ToCharArray()[0] == '/')
                    {
                        List<string> args = sendmsg_box.Text.Split(' ').ToList();
                        string cmd = args[0];
                        args.RemoveAt(0);

                        if (cmd == "/me")
                        {
                            string argument = "";
                            foreach (string word in args)
                            {
                                argument += " " + word;
                            }
                            writer.WriteLine("PRIVMSG " + CHANNEL + " \x0001ACTION" + argument + "\x0001");
                            writer.Flush();
                            console_tbox.AppendText("* MEOW" + argument + "\r\n");
                        }
                    }
                    else
                    {
                        writer.WriteLine("PRIVMSG " + CHANNEL + " " + sendmsg_box.Text);
                        writer.Flush();
                        console_tbox.AppendText("MEOW:" + sendmsg_box.Text + "\r\n");
                    }
                }
            }
            else
            {
                console_tbox.AppendText("You are not connected!\r\n");
            }
            sendmsg_box.Text = "";
        }

        private void onEnterSend(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                SendMessage(sendmsg_box.Text);
            }
        }
    }
}