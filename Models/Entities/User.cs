﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadBalancer.Models.Entities
{
    public class User : IdentityUser
    {
        public List<DataResult> Jobs { get; set; }
    }
}
