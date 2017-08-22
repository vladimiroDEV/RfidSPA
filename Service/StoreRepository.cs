using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RfidSPA.Data;
using RfidSPA.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using static RfidSPA.Helpers.Constants;

namespace RfidSPA.Service
{
    public class StoreRepository : IStoreRepository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAcessor;


        public StoreRepository(
            UserManager<ApplicationUser> userManager,           
            ILoggerFactory loggerFactory,
            ApplicationDbContext appDbContext,
            IHttpContextAccessor httpContextAcessor)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _logger = loggerFactory.CreateLogger<StoreRepository>();
            _httpContextAcessor = httpContextAcessor;

        }

        public async Task<long> CreateStore(Store stroreModel)
        {

            try
            {
                var userid = _httpContextAcessor.HttpContext.User.Claims.Single(c => c.Type == "id").Value;


                Store newStore = new Store
                {
                    Name = stroreModel.Name,
                    Telefono = stroreModel.Telefono,
                    Address = stroreModel.Address,
                    CreationDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    CreatorUser = userid,
                    AdministratorID = userid
                };

                StoreUsers storeUsers = new StoreUsers
                {
                    UserID = userid,
                    UserRole = UserRolesConst.Administrator,
                    Store = newStore

                };



                await _appDbContext.StoreUsers.AddAsync(storeUsers);
                await _appDbContext.SaveChangesAsync();

                var storeID = _appDbContext.StoreUsers.Where(i => i.UserID == userid).Select(i => i.StoreID).SingleOrDefault();
                return storeID;
            }
            catch(Exception ex)
            {
                return -100;
            }

        }


        public async Task<long> GetstoreIdByUser(string userID)
        {
            long id;
            try
            {
              id  = _appDbContext.StoreUsers.FirstOrDefault(i => i.UserID == userID).StoreID;
            }
            catch(Exception ex)
            {
                id = -100;
            }

            return  await Task.FromResult<long>(id);

        }




        private async Task<ApplicationUser> GetCurrentUser()
        {

            var userid = _httpContextAcessor.HttpContext.User.Claims.Single(c => c.Type == "id").Value;
            return await _userManager.FindByIdAsync(userid);
        }


    }
}
