﻿using System;
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
            if (!res) return BadRequest();

            return Ok();
        }
        //gtstore 


        //create Store

        // updateStore

        // delete store 
    }
}