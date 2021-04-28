using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Request : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Status { get; set; }
        public int RequestRawId { get; set; }
        public RequestRaw RequestRaw { get; set; }
        public int? ErrorId { get; set; }
        public Error Error { get; set; }
    }
}
