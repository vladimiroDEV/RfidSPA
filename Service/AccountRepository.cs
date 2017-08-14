using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RfidSPA.Data;
using RfidSPA.Models.Entities;
using System.Linq;
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
           // var user = _userManager.GetUserAsync(_httpContextAcessor.HttpContext.User); //

            //_currentUserID = _userManager.GetUserId( user);//_httpContextAcessor.HttpContext.User.Claims.Single(c => c.Type == "id").Value;
        }


        public async Task<ChangePasswordStatus> ChangePassword(ChangepasswordModel model)  
        {

            
            var result = await _userManager.ChangePasswordAsync(await GetCurrentUser(), model.oldPassword, model.newPassword);

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

        private async Task<ApplicationUser> GetCurrentUser()
        {

            var userid = _httpContextAcessor.HttpContext.User.Claims.Single(c => c.Type == "id").Value;
           return  await _userManager.FindByIdAsync(userid);
        }


    }
}
