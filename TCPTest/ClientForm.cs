﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPTest
{
    public delegate void InvokeButtonSend();

    public partial class ClientForm : Form
    {
        private Thread threadClient = null;
        private double points = -1;
        private string message = "";

        public ClientForm()
        {
            InitializeComponent();
            trackBar1.Value = 10;
            
            threadClient = new Thread(RunClient);
           // threadClient.IsBackground = false;
            threadClient.Start();
        }
        
        public void RunClient()
        {
            TcpClient client = null;

            
            try
            {
                // get ip from public serverlist
                //FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://files.000webhost.com/simhoppServers.txt");
                //request.Method = WebRequestMethods.Ftp.UploadFile;
                WebClient request = new WebClient();
                string url = "ftp://files.000webhost.com/simhoppServers.txt";

                // Get network credentials.
                request.Credentials = new NetworkCredential("oskarsandh", "simmalungt1");

                string ip = "";

                try
                {
                    byte[] bytes = request.DownloadData(url);
                    ip = System.Text.Encoding.UTF8.GetString(bytes);
                }
                catch
                {
                    // do something
                }

                Int32 port = 27015;
                client = new TcpClient(ip, port);

                StreamReader sr = new StreamReader(client.GetStream());
                StreamWriter sw = new StreamWriter(client.GetStream());

                String str = "";

                while (!str.StartsWith("quit"))
                {
                    str = sr.ReadLine();

                    // server har skickat ut att den vill ha något
                    if (str.StartsWith("give"))
                    {
                        sw.WriteLine(" ");
                        ToggleButtonSend();
                    }
                    // knappen är tryckt
                    else if (str.StartsWith("open") && sendPoints)
                    {
                        ToggleButtonSend();
                        sw.WriteLine(message);
                        sendPoints = false;
                    }

                    message = "";
                    sw.Flush();
                }
            }
            catch (IOException ioe)
            {
                client?.Close();
            }
            finally
            {
                client?.Close();
            }
        }

        private void ToggleButtonSend()
        {
            this.Invoke(new InvokeButtonSend(
                () => { this.buttonSend.Enabled = !(this.buttonSend.Enabled); }
                ));
        }

        bool sendPoints = false;
        private void Button1_Click(object sender, EventArgs e)
        {
            message = "Points " + points;
            sendPoints = true;
        }

        
        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            points = (double)trackBar1.Value / 2;
            labelPoints.Text = points.ToString();
        }
    }
}
