using LoadBalancer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadBalancer.Models.ViewModels
{
    public class DataTransformViewModel
    {
        public string Data { get; set; }
        public string FileName { get; set; }
        public string SignalRConnectionId { get; set; }
        public List<ReplaceAction> ToDo { get;set; }
    }
}
