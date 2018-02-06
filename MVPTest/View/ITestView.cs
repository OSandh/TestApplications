using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPTest.View
{
    public delegate void DelegateGoBack();
    public delegate void DelegateAddPerson();

    interface ITestView
    {
        event DelegateGoBack EventGoBack;
        event DelegateAddPerson EventAddPerson;
    }
}
