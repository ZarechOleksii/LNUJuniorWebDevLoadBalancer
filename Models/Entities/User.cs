using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LoadBalancer.Models.Entities
{
    public class User : IdentityUser
    {
        public List<DataResult> Jobs { get; set; }
    }
}
