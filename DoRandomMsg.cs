using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IRCbot
{
    public class DoRandomMsg
    {
        Form1 form;
        public DoRandomMsg(Form1 form)
        {
            this.form = form;
        }

        public void ProcessRandomMsg()
        {
            while (true)
            {
                string response = "";
                string response2 = "";
                byte randval = (byte)new Random().Next(5);
                switch (randval)
                {
                    case 0:
                        response = "MEOW?";
                        break;
                    case 1:
                        response = "Someone MEOW to me! :3 ?";
                        break;
                    case 2:
                        response = "Hello";
                        break;
                    case 3:
                        response = "Dogs suck, Cats ftw, Meow to me pls!";
                        break;
                    case 4:
                        response = "Error 6669001337, shutting down...";
                        response2 = "Just kidding.";
                        break;
                    default:
                        break;
                }
                if (response2 != "")
                {
                    form.writer.WriteLine("PRIVMSG " + form.CHANNEL + " " + response);
                    form.writer.Flush();
                    Thread.Sleep(1000);
                    form.writer.WriteLine("PRIVMSG " + form.CHANNEL + " " + response2);
                    form.writer.Flush();
                }
                else
                {
                    form.writer.WriteLine("PRIVMSG " + form.CHANNEL + " " + response);
                    form.writer.Flush();
                }
                Thread.Sleep(298796);
            }
        }
    }
}
