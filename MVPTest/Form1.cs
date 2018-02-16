using MVPTest.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVPTest
{
    public partial class Form1 : Form
    {
        private Panel currentPanel;
        private PresenterTestView presenter;
        public Form1()
        {
            InitializeComponent();
            currentPanel = panel1;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TestView testPanel = new TestView();
            presenter = new PresenterTestView(testPanel);
            this.Controls.Remove(currentPanel);
            this.Controls.Add(testPanel);
        }

    }
}
