using System;

namespace LoadBalancer.Models.ViewModels
{
    public class DataResultsViewModel
    {
        public string SourceName { get; set; }

        public DateTime DateTime { get; set; }

        public TimeSpan TimeSpan { get; set; }

        public bool InProgress { get; set; }
        public bool Errored { get; set; }
    }
}
