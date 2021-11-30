using LoadBalancer.Models.Entities;
using System.Collections.Generic;

namespace LoadBalancer.Models.ViewModels
{
    public class DataTransformViewModel
    {
        public string Data { get; set; }
        public string FileName { get; set; }
        public string SignalRConnectionId { get; set; }
        public List<ReplaceAction> ToDo { get; set; }
    }
}
