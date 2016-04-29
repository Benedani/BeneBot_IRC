using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRCbot
{
    public class RespondToMsg
    {
        string message;
        Form1 origform;
        
        public RespondToMsg(string message, Form1 form)
        {
            this.message = message;
            origform = form;
        }

        public void Respond()
        {
            List<char> msgchar = message.ToCharArray().ToList();
            List<char> parsename = new List<char> { };
            while (msgchar[0] != ':')
            {
                parsename.Add(msgchar[0]);
                msgchar.RemoveAt(0);
            }
            msgchar.RemoveAt(0);

            string msg = "";
            foreach (char chr in msgchar)
            {
                msg += chr.ToString();
            }

            string name = "";
            foreach (char chr in parsename)
            {
                name += chr.ToString();
            }

            List<string> lines = msg.Split(' ').ToList();
            if (lines[0] == "MEOW:")
            {
                lines.RemoveAt(0);
                foreach (string str in lines)
                {
                    ulong meaning = GetMeaningFor(str);
                    string response = "";
                    if (GetMeaningFor(str) == 1)
                    {
                        byte randval = (byte)new Random().Next(6);
                        switch (randval)
                        {
                            case 0:
                                response = "Hello meow :3";
                                break;
                            case 1:
                                response = "Hey there!!";
                                break;
                            case 2:
                                response = "Hii :3";
                                break;
                            case 3:
                                response = "Hai to you too!";
                                break;
                            case 4:
                                response = "Hei Myu Lol";
                                break;
                            case 5:
                                response = "ERROR 404... er... hi";
                                break;
                            default:
                                break;
                        }
                        origform.writer.WriteLine("PRIVMSG " + origform.CHANNEL + " " + response);
                        origform.writer.Flush();
                        return;
                    }
                    if (GetMeaningFor(str) == 4)
                    {
                        origform.writer.WriteLine("PRIVMSG " + origform.CHANNEL + " Illuminati Confirmed!");
                        origform.writer.Flush();
                        return;
                    }
                    if (GetMeaningFor(str) == 2)
                    {
                        byte randval = (byte)new Random().Next(2);
                        switch (randval)
                        {
                            case 0:
                                response = "Did you mean " + str +" " + name;
                                break;
                            case 1:
                                response = "Correction: " + str + " "+ name;
                                break;
                            default:
                                break;
                        }
                        origform.writer.WriteLine("PRIVMSG " + origform.CHANNEL + " " + response);
                        origform.writer.Flush();
                        return;
                    }
                    if (GetMeaningFor(str) == 3)
                    {
                        byte randval = (byte)new Random().Next(5);
                        switch (randval)
                        {
                            case 0:
                                response = "MEOW to you too, " + name + "!";
                                break;
                            case 1:
                                response = "MYU, " + name + "!";
                                break;
                            case 2:
                                response = "Yay I love MEOWing :D";
                                break;
                            case 3:
                                response = "Eh, dogs are better... LOL APRIL FOOLS!! wait its almost may";
                                break;
                            case 4:
                                response = "MEOWy MEOWs to everyone! Especially " + name + "!";
                                break;
                            default:
                                break;
                        }
                        origform.writer.WriteLine("PRIVMSG " + origform.CHANNEL + " " + response);
                        origform.writer.Flush();
                        return;
                    }
                }
                origform.writer.WriteLine("PRIVMSG " + origform.CHANNEL + " I don't understand. Remember that this AI is pretty new right now.");
                origform.writer.Flush();
            }
            return;
        }

        ulong GetMeaningFor(string stri)
        {
            string str = stri.ToLower();
            if (str.Contains("hi") || str.Contains("hello") || str.Contains("hey there") || str.Contains("hai"))
            {
                return 1;
            }
            else if (str.Contains("fuck") || str.Contains("fuk") || str.Contains("bitch") || str.Contains("biatch") || str.Contains("b1tch") || str.Contains("shit") || str.Contains("sh1t"))
            {
                return 2;
            }
            else if (str.Contains("meow") || str.Contains("myu"))
            {
                return 3;
            }
            else if (str.Contains("/o\\"))
            {
                return 4;
            }
            return 0;
        }
    }
}