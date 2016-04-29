using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace IRCbot
{
    public class MsgHandler
    {
        #region vars
        string inputLine;
        public bool respondtouser = false;
        #endregion

        Form1 form;
        public MsgHandler(Form1 form)
        {
            this.form = form;
        }

        private void AddText(string str)
        {
            string name = form.AppendText;
            form.AppendText = str;
        }

        public void Read()
        {
            while (true)
            {
                try
                {
                    inputLine = form.reader.ReadLine();
                    if (inputLine != null)
                    {
                        try
                        {
                            string[] splitInput = inputLine.Split(new Char[] { ' ' });
                            if (respondtouser)
                            {
                                string finalline;
                                if (inputLine.Contains('!'))
                                {
                                    List<string> words = inputLine.Split(' ').ToList();
                                    List<char> chars = words[0].ToList();
                                    chars.RemoveAt(0);
                                    List<char> temp = new List<char> { };
                                    int x = 0;
                                    while (chars[x] != '!')
                                    {
                                        temp.Add(chars[x]);
                                        x++;
                                    }
                                    string name = "";
                                    x = 0;
                                    try
                                    {
                                        while (true)
                                        {
                                            name += temp[x];
                                            x++;
                                        }
                                    }
                                    catch
                                    {
                                    }
                                    if (splitInput[1] != "JOIN" && splitInput[1] != "PART")
                                    {
                                        words.RemoveRange(0, 3);

                                        finalline = name;
                                        x = 0;
                                        try
                                        {
                                            while (true)
                                            {
                                                finalline += words[x];
                                                finalline += " ";
                                                x++;
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    else
                                    {
                                        if (splitInput[1] == "JOIN")
                                        {
                                            finalline = name + " joined " + form.CHANNEL + ".";
                                            form.AddUserToList(name, true);
                                        }
                                        else if (splitInput[1] == "PART")
                                        {
                                            finalline = name + " left " + form.CHANNEL + ".";
                                            form.DelUserFromList(name, true);
                                        }
                                        else
                                        {
                                            finalline = "Unhandled exception with user message";
                                        }
                                    }
                                }
                                else finalline = inputLine;
                                if (splitInput[0] != "PING")
                                {
                                    AddText(finalline);
                                    if (finalline.Contains(':'))
                                    {
                                        RespondToMsg rtm = new RespondToMsg(finalline, form);
                                        Thread oThread = new Thread(new ThreadStart(rtm.Respond));
                                        oThread.Start();
                                        oThread.Join();
                                    }
                                }
                            }
                            else
                            {
                                if (splitInput[0] != "PING")
                                {
                                    AddText(inputLine);
                                }
                            }
                            if (splitInput[0] == "PING")
                            {
                                string PongReply = splitInput[1];
                                form.writer.WriteLine("PONG " + PongReply);
                                form.writer.Flush();
                            }

                            switch (splitInput[1])
                            {
                                case "001":
                                    string JoinString = "JOIN " + form.CHANNEL;
                                    form.writer.WriteLine(JoinString);
                                    form.writer.Flush();
                                    break;
                                case "353":
                                    List<string> words = splitInput.ToList();
                                    words.RemoveRange(0, 5);
                                    ushort x = 0;
                                    foreach (string origname in words)
                                    {
                                        string name = origname;
                                        List<char> namech = name.ToCharArray().ToList();
                                        if (namech[0] == '+' || namech[0] == '%' || namech[0] == '@' || namech[0] == '&' || namech[0] == '~' || namech[0] == ':')
                                        {
                                            namech.RemoveAt(0);
                                            name = "";
                                            foreach (char chr in namech)
                                            {
                                                name += chr;
                                            }
                                        }
                                        try
                                        {
                                            string test = words[x + 2]; // If there is still another name ahead
                                            form.AddUserToList(name, false);
                                        }
                                        catch
                                        {
                                            form.AddUserToList(name, true);
                                        }
                                        x++;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        catch { }

                        if (inputLine.Contains("End of /NAMES"))
                        {
                            respondtouser = true;
                            form.writer.WriteLine("PRIVMSG NickServ identify " + form.password);
                            form.writer.Flush();
                        }
                    }
                }
                catch { }
            }
        }
    }
}
