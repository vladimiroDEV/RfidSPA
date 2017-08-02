
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RfidSPA.Data;
using RfidSPA.Models.Entities;
using RfidSPA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace RfidSPA.Service
{
    public class AnagraficaRepository: IAnagraficaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAcessor;
        private readonly string _currentUserID;


        public AnagraficaRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor HttpContextAcessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAcessor = HttpContextAcessor;
            _currentUserID = _httpContextAcessor.HttpContext.User.Claims.Single(c => c.Type == "id").Value;


        }

        public object User { get; private set; }

        public Anagrafica AnagraficaByEmail(string email)
        {
            return _context.Anagrafica
                .Where(i => i.Email == email)
                .SingleOrDefault();
        }

        public Anagrafica AnagraficaByID(long ID)
        {
            return _context.Anagrafica.Where(i => i.AnagraficaID == ID).SingleOrDefault();
        }

        public IEnumerable<Anagrafica> GetAllAnagrafics()
        {
            return _context.Anagrafica.ToList();
        }

        public string[] SearchEmailLike(string email)
        {
            

            var value =_context.Anagrafica
                .Where(i => i.Email.Contains(email) && i.ApplicationUserID ==_currentUserID)
                .Select(i => i.Email)
                .ToArray();
            return value;
        }
    }
}
