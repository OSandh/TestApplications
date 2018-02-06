using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPTest
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name)
        {
            this.Name = name;
        }
        public override string ToString()
        {
            return $"Name: {Name}, {Age} years of age";
        }
    }
}
