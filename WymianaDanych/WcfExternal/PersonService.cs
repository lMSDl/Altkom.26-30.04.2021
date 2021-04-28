using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfExternal
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class PersonService : IPersonService
    {
        public int CheckPoints(Person person)
        {
            if (new Random().Next(0, 10) == 0)
                throw new Exception("Internal Error");

            return (person.FirstName.Length + person.LastName.Length) % 21;
        }
    }
}
