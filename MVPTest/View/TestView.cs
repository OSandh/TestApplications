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
        private Button addBtn;
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
            this.addBtn = new Button();
            this.NameLabel = new Label();
            backBtn = new Button();

            // 
            // panel1
            // 
            this.Controls.Add(backBtn);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.label1);
            this.Name = "panel1";
            this.TabIndex = 0;
            //
            // backBtn
            //
            backBtn.Location = new System.Drawing.Point(5, 5);
            backBtn.Name = "backBtn";
            backBtn.Size = new System.Drawing.Size(75, 20);
            backBtn.Text = "Go back";
            backBtn.UseVisualStyleBackColor = true;
            backBtn.Click += new EventHandler(this.BackButton_Click);
            backBtn.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter name";
            // 
            // nameText
            // 
            this.NameBox.Location = new System.Drawing.Point(5, 50);
            this.NameBox.Name = "nameText";
            this.NameBox.Size = new System.Drawing.Size(100, 20);
            this.NameBox.TabIndex = 2;
            // 
            // button1
            // 
            this.addBtn.Location = new System.Drawing.Point(105, 50);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 20);
            this.addBtn.TabIndex = 3;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new EventHandler(this.AddBtn_Click);
            // 
            // nameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(5, 80);
            this.NameLabel.Name = "nameLabel";
            this.NameLabel.Size = new System.Drawing.Size(0, 100);
            this.NameLabel.TabIndex = 4;
        }

        private void BackButton_Click(object sender, EventArgs e) => this.EventGoBack.Invoke();

        //add button event
        private void AddBtn_Click(object sender, EventArgs e) => this.EventAddPerson.Invoke();
    }
}
