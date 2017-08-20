using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RfidSPA.ViewModels
{
    public class ApplicationUserVM
    {
        public string  Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreationDate { get; set; }
        //public List<IdentityUserRole<string>> Roles { get; set; }
        public List<string> Roles { get; set; }
    }
}
