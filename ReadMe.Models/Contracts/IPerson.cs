﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadMe.Models.Contracts
{
    public interface IPerson
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string Nationality { get; set; }

        int Age { get; set; }
    }
}
