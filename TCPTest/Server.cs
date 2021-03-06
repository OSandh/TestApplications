﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPTest
{
    public delegate void InvokeLabel1();
    public delegate void InvokeJudgeListView();

    public class Server
    {
        private static Server tcpServer = null;

        public int IdClient { get; set; } = 0;

        public List<HandleClient> clientList { get; set; } = new List<HandleClient>();

        private Form1 form = null;

        public static Server Instance()
        {
            if (tcpServer == null)
                tcpServer = new Server();
            return tcpServer;
        }
        
        private Int32 port = 27015;
        private IPAddress localAddress = IPAddress.Parse("127.0.0.1");
        private TcpListener tcpListener = null;
        private Thread threadServer = null;
        
        public Server()
        {
            // Hämta ditt ip
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localAddress = ip;
                }
            }

            UploadServerList();

            // kallar Instance()
            tcpServer = this;
            threadServer = new Thread(tcpServer.ThreadListener);
            //threadServer.IsBackground = true;
            threadServer.Start();
        }

        private void UploadServerList()
        {
            
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://files.000webhost.com/simhoppServers.txt");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // Get network credentials.
            request.Credentials = new NetworkCredential("oskarsandh", "simmalungt1");

            // Write the text's bytes into the request stream.
            string text = localAddress.ToString();
            request.ContentLength = text.Length;

            using (Stream request_stream = request.GetRequestStream())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                request_stream.Write(bytes, 0, text.Length);
                request_stream.Close();
            }
        }

        internal void RequestPoints()
        {
            foreach(var client in clientList)
            {
                client.AcceptPoints = true;
                client.StreamWriter.WriteLine("give");
                client.StreamWriter.Flush();
            }
        }

        public void TieToForm(Form1 form)
        {
            this.form = form;
        }

        // lyssnar efter clienter
        private void ThreadListener()
        {
            try
            {
                tcpListener = new TcpListener(localAddress, port);
                tcpListener.Start();

                while (true)
                {
                    UpdateLabel("Waiting for connection...");

                    TcpClient client = tcpListener.AcceptTcpClient();

                    UpdateJudgeListView(new HandleClient(this, client, (clientList.Count + 1), form));
                }

            }catch(SocketException e)
            {
                
            }
            finally
            {
                tcpListener.Stop();
            }
        }

        public void KillThreads()
        {
            threadServer.IsBackground = true;
            foreach (var client in clientList)
            {
                client.ThreadClient.IsBackground = true;
            }
        }

        public void UpdateLabel(string text)
        {
            form?.Invoke(new InvokeLabel1(
                        () => { form.UpdateLabel(text); }
                        ));
        }

        public void UpdateJudgeListView(HandleClient client)
        {
            lock (clientList)
            {
                clientList.Add(client);
            }

            form?.Invoke(new InvokeJudgeListView(
                () => { form.AddToClientList(client); }
                ));

        }
        
    }
}
