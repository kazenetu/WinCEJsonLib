using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DummyWebAPI
{
    [DataContract()]
    public class DataCount
    {
        [DataMember]
        public int count;

        public DataCount(int count)
        {
            this.count = count;
        }
    }
}