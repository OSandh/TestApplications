using MVPTest.Presenter;
using MVPTest.View;
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
        private PresenterTestView testPresenter;
        private PresenterMainView mainPresenter;
        public Form1()
        {
            InitializeComponent();
            currentPanel = new MainView();
            mainPresenter = new PresenterMainView((MainView)currentPanel, this);
            this.Controls.Add(currentPanel);
        }

        public void ChangePanel(PanelView view, PanelView cameFrom)
        {
            this.Controls.Remove(cameFrom);
            this.Controls.Add(view);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Ma testPanel = new TestView();
            //presenter = new PresenterTestView(testPanel);
            //this.Controls.Remove(currentPanel);
            //this.Controls.Add(testPanel);
        }

    }
}
