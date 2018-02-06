using MVPTest.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPTest.Presenter
{
    class PresenterMainView
    {
        public IMainView _view { get; set; }

        // to handle callbacks to papa Form1
        private Form1 window;


        public PresenterMainView(IMainView view, Form1 mainWindow)
        {
            _view = view;
            _view.EventGotoAdd += GotoAdd;
            window = mainWindow;
        }

        private void GotoAdd()
        {
            TestView testPanel = new TestView();
            PresenterTestView presenter = new PresenterTestView(testPanel, window);
            window.ChangePanel(testPanel, (PanelView) _view);
        }
    }
}
