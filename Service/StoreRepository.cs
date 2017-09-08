using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RfidSPA.Data;
using RfidSPA.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using static RfidSPA.Helpers.Constants;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

                Store newStore = new Store
                {
                    Name = stroreModel.Name,
                    Telefono = stroreModel.Telefono,
                    Address = stroreModel.Address,
                    CreationDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    CreatorUser = stroreModel.AdministratorID,
                    AdministratorID = stroreModel.AdministratorID,
                };

                StoreUser storeUser = new StoreUser
                {
                    ApplicationUserID = stroreModel.AdministratorID,
                    UserRole = UserRolesConst.Administrator,
                    Store = newStore

                };

            /// storeUser.Add(storeUser);



                await _appDbContext.StoreUsers.AddAsync(storeUser);
                await _appDbContext.SaveChangesAsync();

                var storeID =await  _appDbContext.Store.Where(i => i.AdministratorID == stroreModel.AdministratorID).Select(i => i.StoreID).SingleAsync();
                
               
                return storeID ;
            }
            catch(Exception ex)
            {
                _logger.LogError("Errore durante la creazione dello store: ", ex);
                return -100;
            }

        }

        public Task<int> DeleteStore(long storeID)
        {
            var l_store = _appDbContext.Store.Where(i => i.StoreID == storeID).SingleOrDefault();
            if (l_store == null) return Task.FromResult(-1);

            try
            {
                l_store.Active = false;
                l_store.LastModifiedDate = DateTime.Now;
                _appDbContext.Store.Update(l_store);
                 _appDbContext.SaveChangesAsync();
                return Task.FromResult(1);
            }
            catch(Exception ex )
            {
                _logger.LogError(ex.ToString());
                return Task.FromResult(0);
            }


        }

        public async Task<Store> GetStoreByID(long StoreID)
        {
            return await   _appDbContext.Store
                .Where(i => i.StoreID == StoreID)
                .Include(op=>op.storeUsers)
                .ThenInclude(appUs =>appUs.ApplicationUser).SingleOrDefaultAsync();
        }

        public async Task<long> GetstoreIdByUser(string userID)
        {
            long id;
            try
            {
              id  = _appDbContext.StoreUsers.FirstOrDefault(i => i.ApplicationUserID == userID).StoreID;
            }
            catch(Exception ex)
            {
                id = -100;
            }

            return  await Task.FromResult<long>(id);

        }

        public Task<int> UpdateStore(Store stroreModel)
        {
            var l_store = _appDbContext.Store.Where(i => i.StoreID == stroreModel.StoreID).SingleOrDefault();
            if (l_store == null) return Task.FromResult(-1);

            try
            {
                l_store.Active = stroreModel.Active;
                l_store.Address = stroreModel.Address;
                l_store.Name = stroreModel.Name;
                l_store.LastModifiedDate = DateTime.Now;
                _appDbContext.Store.Update(l_store);
                _appDbContext.SaveChangesAsync();
                return Task.FromResult(1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Task.FromResult(0);
            }

        }

        public Task<int> AddStoreOperator(long StoreID, StoreUser storeUser)
        {
            var l_store = _appDbContext.Store.Where(i => i.StoreID == StoreID).SingleOrDefault();
            if (l_store == null) return Task.FromResult(-1);
            try
            {
                l_store.storeUsers.Add(storeUser);
                _appDbContext.SaveChangesAsync();
                return Task.FromResult(1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Task.FromResult(0);
            }

        }

        public Task<int> RemoveStoreOperator(long StoreID, string  storeUserID)
        {
            var l_storeUser = _appDbContext.StoreUsers.Where(i => i.StoreID == StoreID && i.ApplicationUserID == storeUserID).SingleOrDefault();
            if (l_storeUser == null) return Task.FromResult(-1);
            try
            {
                _appDbContext.Remove(l_storeUser);
                _appDbContext.SaveChangesAsync();
                return Task.FromResult(1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Task.FromResult(0);
            }

        }

        private async Task<ApplicationUser> GetCurrentUser()
        {

            var userid = _httpContextAcessor.HttpContext.User.Claims.Single(c => c.Type == "id").Value;
            return await _userManager.FindByIdAsync(userid);
        }

        public Task<List<ApplicationUser>> GetStoreUsers(long StoreID)
        {
            var store = _appDbContext.Store.Include(u => u.storeUsers).ThenInclude(apppU=>apppU.ApplicationUser).FirstOrDefault();
            if (store == null) return null;
           

            List<ApplicationUser> users = store.storeUsers.Select(u=>u.ApplicationUser).ToList();
            return Task.FromResult(users);
        }
    }
}
