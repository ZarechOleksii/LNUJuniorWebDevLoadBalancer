﻿using LoadBalancer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadBalancer.Models.ViewModels
{
    public class DataTransformViewModel
    {
        public string Data { get; set; }
        public ReplaceAction ToDo { get;set; }
    }
}