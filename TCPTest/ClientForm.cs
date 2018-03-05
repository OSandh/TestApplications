using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPTest
{
    public partial class ClientForm : Form
    {
        private Thread threadClient = null;
        private double point = -1;
        public ClientForm()
        {
            InitializeComponent();

            threadClient = new Thread(RunClient);
            threadClient.Start();
        }
        
        public void RunClient()
        {
            TcpClient client = null;

            
            try
            {
                Int32 port = 27015;
                client = new TcpClient("127.0.0.1", port);

                StreamReader sr = new StreamReader(client.GetStream());
                StreamWriter sw = new StreamWriter(client.GetStream());

                String str = "";

                while (!str.StartsWith("quit"))
                {
                    str = point.ToString();

                    if (!str.Equals("-1"))
                    {
                        sw.WriteLine(point);
                    }

                    point = -1;
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

        private void button1_Click(object sender, EventArgs e)
        {
            point = Convert.ToDouble(comboBoxPoint.SelectedItem);
        }
    }
}
