﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Repositories.Interfaces.User
{
    public class UserUpdate
    {
        public SqlString? FullName { get; set; }
        public SqlString? Password { get; set; }
    }
}
