﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class User
    {
        public Int32 UserId { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String UserName { get; set; }

        public String UserPass { get; set; }
    }
}
