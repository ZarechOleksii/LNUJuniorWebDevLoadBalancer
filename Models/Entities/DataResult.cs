using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LoadBalancer.Models.Entities
{
    public class DataResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string SourceName { get; set; }

        public string Before { get; set; }

        public string After { get; set; }

        public List<ReplaceAction> Actions { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        [Required]
        [ForeignKey("Id")]
        public string UserId { get; set; }

        public TimeSpan TimeSpan { get; set; }

        public bool InProgress { get; set; }

        public DataResult(string userId)
        {
            SourceName = "";
            Before = "";
            After = "";
            UserId = userId;
            DateTime = DateTime.Now;
            TimeSpan = TimeSpan.Zero;
            InProgress = true;
        }
    }
}
