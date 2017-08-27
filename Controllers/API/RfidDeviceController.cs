﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RfidSPA.Service.Interfaces;
using RfidSPA.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using RfidSPA.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RfidSPA.Controllers.API
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class RfidDeviceController : Controller
    {

        private readonly IRfidDeviceRepository _repositoryRfid;
        private readonly IAnagraficaRepository _repositoryAnagrafica;
        public RfidDeviceController(IRfidDeviceRepository repositoryRfid, IAnagraficaRepository reposistoryAnagrafica)
        {
            _repositoryRfid = repositoryRfid;
            _repositoryAnagrafica = reposistoryAnagrafica;
        }
     

    
        [HttpGet("code/{code}")]
        public IActionResult Get(string code)
        {
            var item = _repositoryRfid.RfidByCode(code);
            if (item == null) return NotFound();
            return Ok(item);
        }



        //paid 
        [HttpPost("paidByDevice")]
        public async  Task<IActionResult> PaidAction([FromBody]PaidModel paidmodel)
        {
            var res =  await _repositoryRfid.PaidByRfid(paidmodel);
            if (res == 1) return  Ok();
            if (res == -1) return BadRequest("NoDevice");
            if (res == -2) return BadRequest("Noanagrafica");

            return BadRequest();



        }

        //
        // parametro Aplication UserID  rfidCode
        [HttpGet("getAllTransactionsToPaydOff/{code}")]
        public IActionResult getAllTransactionsToPaydOff(string code)
        {

            var res = _repositoryRfid.getAllTransactionsToPaydOff(code);

            if (res != null)
                if(res.Count >0)
                 return Json(res);

            return NotFound();
        }

        // get user deatil by email 
        [HttpGet("userdetailbymail/{email}")]
        public IActionResult getUserDetailByEmail(string email)
        {

            var res = _repositoryRfid.getGeatailUserByEmail(email);

            if (res != null)
                return Json(res);

            return NotFound();
        }
        // get user deatil by email 
        [HttpGet("userdetailbyrfidcode/{code}")]
        public IActionResult getUserDetailByRfidCode(string code)
        {
            var res = _repositoryRfid.getGeatailUserByRfidCode(code);

            if (res != null)
                return Json(res);

            return NotFound();
        }

        // PAid 
        [HttpPost("paidTotalReset")]
        public IActionResult paidTotalReset([FromBody] string code)
        {

            var res = _repositoryRfid.paidOffRfid(code);
            if (res) return Ok();

            return NotFound();

        }
        // PAid 
        [HttpPost("paidTotal/{email}")]
        public IActionResult paidTotal(string email)
        {
            var anag = _repositoryAnagrafica.AnagraficaByEmail(email);
            if (anag == null) return NotFound();
            var lisRfids = _repositoryRfid.RfidsByAnagraficaID(anag.AnagraficaID);

            var res = _repositoryRfid.paidOffAllRfids(lisRfids);

            if (res) return Ok();

            return NotFound();

        }

        // get DeviceByApllicaionUser 

        [HttpGet("GetByApplicationUser")]
        public async Task<IActionResult>  GetByApplicationUser()
        {
            var resp = await  _repositoryRfid.getDevicesByApplicationUsers();
            if (resp != null) return Json(resp);
            else return NotFound();

        }

        // new 

        [HttpPost("JoinDeviseToAnagrafica")]

        public async  Task<IActionResult> JoinDeviseToAnagrafica([FromBody] RfidDevice device)
        {
            var res = await  _repositoryRfid.JoinDeviseToAnagrafica(device);
            if (res == 1) return Ok();
            if (res == 2) return BadRequest("inUse");
            if (res == -1) return BadRequest();

            return BadRequest();
        }



    }
}
