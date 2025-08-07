using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DAL.Entities.Identity
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> Users { get; set; }
    }
}
