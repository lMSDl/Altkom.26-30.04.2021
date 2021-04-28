using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfExternal
{
    [ServiceContract]
    public interface IPersonService
    {
        [OperationContract]
        int CheckPoints(Person person);

    }
    
    [DataContract]
    public class Person
    {

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }
    }
}
