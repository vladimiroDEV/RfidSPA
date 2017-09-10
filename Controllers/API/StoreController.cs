using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RfidSPA.Service;
using RfidSPA.Models.Entities;
using RfidSPA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using static RfidSPA.Helpers.Constants;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RfidSPA.Controllers.API
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class StoreController : Controller
    {

        private readonly IStoreRepository _storeRepository;

        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]Store store)
        {

            var res = await _storeRepository.CreateStore(store);
            if (res < 0) return BadRequest();

            var result = new
            {
                store_id = res
            };

            return new OkObjectResult(result);
        }

        [HttpPost("updateStore")]
        [Authorize(Policy = UserRolesConst.StoreAdministrator)]
        public async Task<IActionResult> Update([FromBody]Store store)
        {
            var l_store = await _storeRepository.UpdateStore(store);

            if (l_store == -1) return NotFound();

            if (l_store == 0) return BadRequest();
            return Ok();

        }

        [HttpPost("deleteStore")]
        public async Task<IActionResult> DeleteStore([FromBody]long storeID)
        {
            var l_store = await _storeRepository.DeleteStore(storeID);

            if (l_store == -1) return NotFound();

            if (l_store == 0) return BadRequest();
            return Ok();

        }

        [HttpPost("GetStoreDetails")]
        public async Task<Store> GetStoreDetails([FromBody]long storeID)
        {
            return await _storeRepository.GetStoreByID(storeID);

        }

        [HttpPost("AddOperator")]
        public async Task<IActionResult> addOperator([FromBody]Store store)
        {
            var l_store = await _storeRepository.AddStoreOperator(store.StoreID, store.storeUsers.First());

            if (l_store == -1) return NotFound();

            if (l_store == 0) return BadRequest();
            return Ok();

        }

        [HttpPost("RemoveOperator")]
        public async Task<IActionResult> RemoveOperator([FromBody]Store store)
        {
            var l_store = await _storeRepository.RemoveStoreOperator(store.StoreID, store.storeUsers.First().ApplicationUserID);

            if (l_store == -1) return NotFound();

            if (l_store == 0) return BadRequest();
            return Ok();

        }

        [HttpPost("GetOperators")]
        public async Task<List<ApplicationUser>> GetOperators([FromBody]long StoreID)
        {
            var users = await _storeRepository.GetStoreUsers(StoreID);

            return await Task.FromResult(users);
        }

        [HttpPost("GetStoreID")]
        public async Task<IActionResult> GetStoreIdByUser([FromBody] string email)
        {
            var res = await _storeRepository.GetstoreIdByUser(email);

            if (res < 0) return BadRequest();
            var result = new
            {
                store_id = res
            };

            return new OkObjectResult(result);
        }

        [HttpGet("GetStoreDevices/{storeID}")]
        [Authorize]
        public async Task<List<RfidDevice>> GetStoreDevices(long storeID)
        {
            var devices = await _storeRepository.GetStoreDevices(storeID);

            return await Task.FromResult(devices);

        }
    }

       
}
