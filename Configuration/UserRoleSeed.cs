using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RfidSPA.Helpers.Constants;

namespace RfidSPA.Configuration
{
    public   class UserRoleSeed
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleSeed(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public UserRoleSeed()
        {

        }

        public   async void Seed(IWebHost host)
        {

          

            var roleM = host.Services.CreateScope().ServiceProvider.GetService<RoleManager<IdentityRole>>();


                List<string> ListRoles = new List<string>(new string[] {
                UserRolesConst.Administrator,
                UserRolesConst.StoreAdministrator,
                UserRolesConst.StoreOperator,
                UserRolesConst.Default,
                UserRolesConst.God
               });

                foreach (var item in ListRoles)
                {

                var RoleExist = await roleM.RoleExistsAsync(item);
                    if (!RoleExist)
                    {
                        await roleM.CreateAsync(new IdentityRole { Name = item });
                    }
                }



            



               


            
        }
    }
}
