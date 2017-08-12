using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        private readonly IAccountRepository _accountRepository;

        public AccountsController(
            IAccountRepository accountRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory,
            ApplicationDbContext appDbContext)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
            _logger = loggerFactory.CreateLogger<AccountsController>();
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



        

        [HttpGet]
        public string get()
        {
            return "fkfòsfsgsdg";
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

    }
}
