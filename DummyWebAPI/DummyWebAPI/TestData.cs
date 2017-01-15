using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DummyWebAPI
{
     [DataContract()]
    public class TestData
    {
        [DataMember()]
        public string Name { get; private set; }
        [DataMember()]
        public int Age { get; private set; }

        public TestData(string name,int age)
        {
            this.Name = name;
            this.Age = age;
        }

    }
}