using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPTest.View
{
    public delegate void DelegateGotoAdd();

    interface IMainView
    {
        event DelegateGotoAdd EventGotoAdd;
    }
}
