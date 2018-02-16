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
        public ITestView _view { get; set; }

        public List<Person> PersonList { get; set; }

        public PresenterTestView(ITestView view)
        {
            PersonList = new List<Person>();
            _view = view;
            _view.EventAddPerson += AddPerson;
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
