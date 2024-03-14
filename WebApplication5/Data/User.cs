using Microsoft.AspNetCore.Identity;

namespace WebApplication5.Data
{
    public class User : IdentityUser
    {
        public int? PhotoUrlID { get; set; }

    }
}
