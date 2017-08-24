using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RfidSPA.Service;
using RfidSPA.Models.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RfidSPA.Controllers.API
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class StoreController : Controller
    {

        private readonly IStoreRepository _storeRepository;

        public StoreController( IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
    }

        [HttpPost("create")]
        public  async Task<IActionResult> Create([FromBody]Store store)
        {

            var res = await _storeRepository.CreateStore(store);
            if (res < 0) return BadRequest();

            var result = new
            {
                store_id = res
            };

            return new  OkObjectResult(result);
        }

          [HttpPost("updateStore")]
        public  async Task<IActionResult> Update([FromBody]Store store)
        {
            var l_store = await _storeRepository.UpdateStore(store);

            if (l_store == -1) return NotFound();

            if (l_store == 0) return BadRequest();
            return Ok();

        }
        [HttpPost("deleteStore")]
        public async Task<IActionResult> DeleteStore([FromBody]long  storeID)
        {
            var l_store = await _storeRepository.DeleteStore(storeID);

            if (l_store == -1) return NotFound();

            if (l_store == 0) return BadRequest();
            return Ok();

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

        [HttpPost("GetStoreID")]
        public async Task<IActionResult> GetStoreIdByUser([FromBody] string email)
        {
            var res =  await _storeRepository.GetstoreIdByUser(email);

            if (res < 0) return BadRequest();
            var result = new
            {
                store_id = res
            };

            return new OkObjectResult(result);
        }


        //create Store

        // updateStore

        // delete store 
    }
}
