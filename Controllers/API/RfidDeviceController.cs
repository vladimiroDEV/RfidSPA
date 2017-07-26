using System;
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
    public class RfidDeviceController : Controller
    {

        private readonly IRfidDeviceRepository _repositoryRfid;
        private readonly IAnagraficaRepository _repositoryAnagrafica;
        public RfidDeviceController(IRfidDeviceRepository repositoryRfid, IAnagraficaRepository reposistoryAnagrafica)
        {
            _repositoryRfid = repositoryRfid;
            _repositoryAnagrafica = reposistoryAnagrafica;
        }
        // GET: api/Rfid
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var result = _repositoryRfid.GetAllRfids();
            return Ok(result);
        }

        // GET: api/Rfid/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        [HttpGet("code/{code}")]
        public IActionResult Get(string code)
        {
            var item = _repositoryRfid.RfidByCode(code);
            if (item == null) return NotFound();
            return Ok(item);
        }




        // create 

        // POST: api/Rfid
        [HttpPost("create")]
        public IActionResult Post([FromBody]AnagraficaRfidDeviceModel value)
        {

            var res = _repositoryRfid.CeateNewRfid(value);
            if (res)
                return Ok();

            return BadRequest();
        }

        //paid 
        [HttpPost("paid")]
        public IActionResult PaidAction([FromBody]PaidModel paidmodel)
        {
            var res = _repositoryRfid.PaidByRfid(paidmodel);
            if (res)
                return Ok();

            return NotFound();
        }

        //
        // parametro rfidCode
        [HttpGet("transactionsToConfirmRfidCode/{code}")]
        public IActionResult getAllTransactionsToConfirm(string code)
        {

            var res = _repositoryRfid.GetAllTransactionsToConfirm(code);

            if (res != null)
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
        [HttpPost("paidTotalReset/{code}")]
        public IActionResult paidTotalReset(string code)
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


    }
}
