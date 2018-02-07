using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVPTest.View
{
    class MainView : PanelView, IMainView
    {
        private Button GotoAdd { get; set; }

        public event DelegateGotoAdd EventGotoAdd = null;

        public MainView()
            : base()
        {
            //
            // GotoAdd Button
            //
            GotoAdd = new Button
            {
                Name = "gotoAdd",
                Location = new System.Drawing.Point((this.Size.Width / 2) - 35, (this.Size.Height / 2) - 10),
                Size = new System.Drawing.Size(70, 20),
                Text = "Add Person",
                UseVisualStyleBackColor = true
            };
            GotoAdd.Click += new EventHandler(this.GotoAdd_Click);
            GotoAdd.TabIndex = 0;

            //
            // MainMenuPanel (this)
            //
            this.Name = "MainMenu";
            this.TabIndex = 0;
            this.Controls.Add(GotoAdd);

        }


        private void GotoAdd_Click(object sender, EventArgs e)
        {
            this.EventGotoAdd.Invoke();
        }
    }
}
