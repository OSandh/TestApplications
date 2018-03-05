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
    public delegate void InvokeLabel1();
    class Server
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
            // kallar Instance()
            tcpServer = this;
            threadServer = new Thread(tcpServer.ThreadListener);
            threadServer.Start();
        }

        public void TieToForm(Form1 form)
        {
            this.form = form;
        }


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

                    lock (clientList)
                    {
                        clientList.Add(new HandleClient(this, client, form));
                    }
                    UpdateLabel("Connected!");
                }

            }catch(SocketException e)
            {
                
            }
            finally
            {
                tcpListener.Stop();
            }
        }

        public void UpdateLabel(string text)
        {
            form?.Invoke(new InvokeLabel1(
                        () => { form.UpdateLabel(text); }
                        ));
        }


        
    }
}
