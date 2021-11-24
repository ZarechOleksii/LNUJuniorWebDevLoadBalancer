using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoadBalancer.Models.Entities
{
    public class ReplaceAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Action { get; set; }
        public string ParameterFrom { get; set; }
        public string ParameterTo { get; set; }
        public bool IsDone { get; set; }
        public DataResult DataResult { get; set; }

        [Required]
        public Guid DataResultId { get; set; }
        public int Order { get; set; }

        public ReplaceAction(string from, string to, int order, Guid id)
        {
            ParameterFrom = from;
            ParameterTo = to;
            Order = order;
            DataResultId = id;
            IsDone = false;
        }

        public string DoAction(string data)
        {
            var result = data.Replace(ParameterFrom, ParameterTo);
            IsDone = true;
            return result;
        }
    }
}
