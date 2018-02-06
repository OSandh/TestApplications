using MVPTest.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPTest.Presenter
{
    class PresenterTestView
    {
        // to handle callbacks to papa Form1
        private Form1 window;


        public ITestView _view { get; set; }

        public ITestView cameFrom { get; set; }

        public List<Person> PersonList { get; set; }

        public PresenterTestView(ITestView view, Form1 mainWindow)
        {
            PersonList = new List<Person>();
            _view = view;
            _view.EventAddPerson += AddPerson;
            _view.EventGoBack += GoBack;
            window = mainWindow;
        }

        private void GoBack()
        {
            MainView mainPanel = new MainView();
            PresenterMainView presenter = new PresenterMainView(mainPanel, window);
            window.ChangePanel(mainPanel, (PanelView)_view);
        }

        public void AddPerson()
        {
            TestView view = (TestView)_view;
            PersonList.Add(new Person(view.NameBox.Text));

            string s = "";
            foreach (var p in PersonList)
                s += p.Name + "\n";

            view.NameLabel.Text = s;
        }

    }
}
