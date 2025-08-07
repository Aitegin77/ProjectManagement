using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DAL.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> Roles { get; set; }
    }
}
