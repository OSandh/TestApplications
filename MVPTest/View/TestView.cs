using MVPTest.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVPTest
{
    class TestView : PanelView, ITestView
    {
        private Button button1;
        private Label label1;
        private Button backBtn;

        public event DelegateGoBack EventGoBack;
        public event DelegateAddPerson EventAddPerson = null;

        public TextBox NameBox { get; }

        public Label NameLabel { get; set; }

        public TestView()
            : base()
        {
            this.label1 = new Label();
            this.NameBox = new TextBox();
            this.button1 = new Button();
            this.NameLabel = new Label();
            backBtn = new Button();
            //
            // backBtn
            //
            backBtn.Text = "Go back";
            // 
            // panel1
            // 
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(12, 12);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(360, 337);
            this.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter name";
            // 
            // nameText
            // 
            this.NameBox.Location = new System.Drawing.Point(7, 21);
            this.NameBox.Name = "nameText";
            this.NameBox.Size = new System.Drawing.Size(100, 20);
            this.NameBox.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(113, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            // 
            // nameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(7, 172);
            this.NameLabel.Name = "nameLabel";
            this.NameLabel.Size = new System.Drawing.Size(0, 13);
            this.NameLabel.TabIndex = 3;
        }

        //add button event
        private void button1_Click(object sender, EventArgs e)
        {
            this.EventAddPerson.Invoke();
        }
    }
}
