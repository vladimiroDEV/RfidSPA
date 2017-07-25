using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RfidSPA.Models.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName {get; set;}
        public string LastName { get; set; }
    }
}