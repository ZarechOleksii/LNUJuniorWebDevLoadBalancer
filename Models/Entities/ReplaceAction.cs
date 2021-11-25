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
        public int Order { get; set; }

        public ReplaceAction()
        {
            IsDone = false;
        }

        public ReplaceAction(string action, string parameterFrom, string parameterTo, int order)
        {
            Action = action;
            ParameterFrom = parameterFrom;
            ParameterTo = parameterTo;
            Order = order;
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
