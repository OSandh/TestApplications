using System;
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

    
    public delegate void InvokeListBox();

    class HandleClient
    {
        public int IdClient { get; set; } = 0;
        public IPEndPoint EndPoint { get; set; } = null;
        public NetworkStream NetworkStream { get; set; } = null;
        public StreamReader StreamReader { get; set; } = null;
        public StreamWriter StreamWriter { get; set; } = null;
        public TcpClient Client { get; set; } = null;
        public Thread ThreadClient { get; set; } = null;
        public Server Server { get; set; } = null;

        private Form1 form;

        public HandleClient(Server server, TcpClient client, Form1 form)
        {
            this.form = form;
            this.Server = server;
            this.Client = client;

            ThreadClient = new Thread(ClientThread);
            ThreadClient.Name = "Client " + ++IdClient;
            ThreadClient.Start();

        }

        public void ClientThread()
        {
            string msg = "";

            
            try
            {
                NetworkStream = Client.GetStream();
                StreamReader = new StreamReader(NetworkStream);
                StreamWriter = new StreamWriter(NetworkStream);

                while (true)
                {
                    msg = StreamReader.ReadLine();

                    if (msg == null || msg.StartsWith("quit"))
                    {
                        break;
                    }
                    else
                    {
                        AddPointToList(msg);
                    }
                }

            }
            catch (IOException ioe) { }
            finally
            {
                Client.Close();
                ThreadClient.IsBackground = true;

                foreach(var client in Server.clientList)
                {
                    if(this == client)
                    {
                        Server.clientList.Remove(client);
                        break;
                    }

                }

                ThreadClient.Abort();
                
            }
            
        }

        public void AddPointToList(string text)
        {
            form.Invoke(new InvokeListBox(
                () => { lock (form.ListBox) { form.AddToPointList(text); }  }
                ));
        }
    }
}
