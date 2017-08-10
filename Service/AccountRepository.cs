using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RfidSPA.Data;
using RfidSPA.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RfidSPA.Service
{
    public class AccountRepository : IAccountRepository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAcessor;
        private string _currentUserID;


        public AccountRepository(
              UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory,
            ApplicationDbContext appDbContext,
            IHttpContextAccessor httpContextAcessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
            _logger = loggerFactory.CreateLogger<AccountRepository>();
            _httpContextAcessor = httpContextAcessor;
            _currentUserID = _httpContextAcessor.HttpContext.User.Claims.Single(c => c.Type == "id").Value;
        }


        public async Task<ChangePasswordStatus> ChangePassword(ChangepasswordModel model)  
        {
            var user = await _userManager.FindByIdAsync(_currentUserID);
            var result = await _userManager.ChangePasswordAsync(user, model.oldPassword, model.newPassword);

            if(result.Succeeded)
            {
                return ChangePasswordStatus.succces;
            }
            else if(result.Errors.Where(i=>i.Code== "PasswordMismatch").Any()) 
            {
                var err = result.Errors;
                return ChangePasswordStatus.passwordNotMatch;
            }
            else
            {
                return ChangePasswordStatus.errorChange;
            }

            
        }


    }
}
