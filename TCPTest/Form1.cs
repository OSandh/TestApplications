using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPTest
{
    public partial class Form1 : Form
    {
        Server server = null;

        public Label Label1 { get { return label1; } set { label1 = value; } }
        public ListBox ListBox { get { return listBox1; } set { listBox1 = value; } }
        public Form1()
        {
            InitializeComponent();
        }

        public void UpdateLabel(string text)
        {
            this.label1.Text = text;
        }

        public void AddToPointList(string text)
        {
            this.listBox1.Items.Add(text);
        }

        private void buttonServer_Click(object sender, EventArgs e)
        {
            server = new Server();
            server.TieToForm(this);
            buttonClient.IsAccessible = false;
            buttonServer.IsAccessible = false;
        }

        private void buttonClient_Click(object sender, EventArgs e)
        {
            ClientForm clientForm = new ClientForm();
            clientForm.Show();
        }
    }
}
