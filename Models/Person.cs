﻿using Models.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Person : Entity
    {
        public DateTime BirthDate { get; set; }
        public string FirstName { get; set; }
        private string _lastName;

        public string LastName
        {
            get
            { 
                return _lastName;
            }
            set
            {
                _lastName = value.ToUpper();
            }
        }

        public string Address { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
