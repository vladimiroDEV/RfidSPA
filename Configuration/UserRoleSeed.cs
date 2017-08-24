using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RfidSPA.Helpers.Constants;

namespace RfidSPA.Configuration
{
    public class UserRoleSeed
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleSeed(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async void Seed()
        {
            List<string> ListRoles = new List<string>(new string[] {
                UserRolesConst.Administrator,
                UserRolesConst.StoreAdministrator,
                UserRolesConst.StoreOperator,
                UserRolesConst.Default
            });

            foreach(var item in ListRoles)
            {
                if ((await _roleManager.FindByNameAsync(item)) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = item });
                }
            }


            
        }
    }
}
