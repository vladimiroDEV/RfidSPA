using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RfidSPA.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RfidSPA.ViewModels;
using RfidSPA.Data;
using RfidSPA.Helpers;
using static RfidSPA.Helpers.Constants;
using RfidSPA.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RfidSPA.Controllers.API
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _appDbContext;
        private readonly IStoreRepository _stroeRepository;

        private readonly IAccountRepository _accountRepository;

        public AccountsController(
            IAccountRepository accountRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory,
            IStoreRepository storeRep,
            ApplicationDbContext appDbContext)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
            _logger = loggerFactory.CreateLogger<AccountsController>();
            _stroeRepository = storeRep;
        }


        // POST api/accounts
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ApplicationUser = new ApplicationUser {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(ApplicationUser, model.Password);

            if (!result.Succeeded)
                return 
                    new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await _userManager.AddToRoleAsync(ApplicationUser, model.Role);

          
            await _appDbContext.SaveChangesAsync();

            return new OkResult();
        }
        // POST api/accounts
        [HttpPost("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appUser = await  _userManager.FindByEmailAsync(model.Email);

            if(appUser == null)
            {
                return NotFound();
            }

            appUser.FirstName = model.FirstName;
            appUser.LastName = model.LastName;

            var result = await _userManager.UpdateAsync(appUser); 

            var roles =  await _userManager.GetRolesAsync(appUser);

            if(!roles.Contains(model.Role))
            {
               await   _userManager.RemoveFromRolesAsync(appUser, roles.ToArray());
                await _userManager.AddToRoleAsync(appUser, model.Role);

            }
          
            if (!result.Succeeded)
                return
                    new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await _appDbContext.SaveChangesAsync();

            return new OkResult();
        }

        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody]string email)
        {
          
            var appUser = await _userManager.FindByEmailAsync(email);

            if (appUser == null)
            {
                return NotFound();
            }

            try
            {
                var roles = await _userManager.GetRolesAsync(appUser);
                if (roles != null)
                {
                    await _userManager.RemoveFromRolesAsync(appUser, roles.ToArray());

                }

                await _userManager.DeleteAsync(appUser);

                return Ok();


            }
            catch(Exception e)
            {
                return BadRequest();

            }

        }







        [HttpGet("allusers")]
        public async Task<List<ApplicationUserVM>> getAllApplicationusers()
        {
            List <ApplicationUserVM> listUsers = new List<ApplicationUserVM>();
            var users = _userManager.Users.Include(u => u.Roles).ToList();

            foreach (var user in users)
            {
                var Roles = await _userManager.GetRolesAsync(user);

                var appuser = new ApplicationUserVM
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = Roles.ToList()
                };

                listUsers.Add(appuser);

            }

            return await Task.FromResult(listUsers);
                
        }

        [HttpGet("userDetail/{email}")]
        public async Task<ApplicationUserVM> getAllApplicationuserDetails(string email)
        {
            ApplicationUserVM  appusers = new ApplicationUserVM();
            var user = await _userManager.FindByEmailAsync(email);
            var Roles = await _userManager.GetRolesAsync(user);

            return await Task.FromResult(new ApplicationUserVM
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = Roles.ToList()
            });

        }

        [HttpPost("ChangePassword")]

        public async  Task<IActionResult> ChangePassword([FromBody] ChangepasswordModel model)
        {

            var res = await _accountRepository.ChangePassword(model);
            if(res == ChangePasswordStatus.succces)
            {
                return new OkResult();
            }
            else
            {
                return BadRequest(res);
            }

           
        }


        [HttpPost("createOperator")]
        public async Task<IActionResult> CreateOperator([FromBody] RegistrationOpeatorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ApplicationUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.Name,
               
            };

            var result = await _userManager.CreateAsync(ApplicationUser, model.Password);

            if (!result.Succeeded)
                return
                    new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await _userManager.AddToRoleAsync(ApplicationUser, UserRolesConst.StoreOperator);


            await _appDbContext.SaveChangesAsync();
          
            ApplicationUser appUser = await _userManager.FindByEmailAsync(model.Email);



             await _appDbContext.StoreUsers.AddAsync(new StoreUsers
            {
                StoreID = model.StoreId,
                   ApplicationUserID = appUser.Id,
                UserRole = UserRolesConst.StoreOperator

            });

            await _appDbContext.SaveChangesAsync();

            return new OkResult();
        }

    }
}
