using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RfidSPA.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RfidSPA.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Anagrafica")]
   [Authorize]
    public class AnagraficaController : Controller
    {
        private readonly IRfidDeviceRepository _repositoryRfid;
        private readonly IAnagraficaRepository _repositoryAnagrafica;
        public AnagraficaController(IRfidDeviceRepository repositoryRfid, IAnagraficaRepository reposistoryAnagrafica)
        {
            _repositoryRfid = repositoryRfid;
            _repositoryAnagrafica = reposistoryAnagrafica;
        }

        [HttpGet("emailLikes/{storeID}/{email}")]
       
        public string[] emailLikes(long storeID, string email)
        {        
            return _repositoryAnagrafica.SearchEmailLike(storeID, email);
        }
    }
}
