
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RfidSPA.Data;
using RfidSPA.Models.Entities;
using RfidSPA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
           // _currentUserID = _httpContextAcessor.HttpContext.User.Claims.Single(c => c.Type == "id").Value;


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

        public Task<int> CreateAnagrafica(Anagrafica anag)
        {
            try
            {

               
                var l_anagrafica = _context.Anagrafica.Where(i => i.Email == anag.Email).SingleOrDefault();
                var storeid = _context.StoreUsers.Where(i => i.ApplicationUserID == anag.ApplicationUserID).Select(i => i.StoreID).FirstOrDefault();

                if (l_anagrafica != null) return Task.FromResult(-1);
                l_anagrafica = new Anagrafica();
                l_anagrafica = anag;
                l_anagrafica.StoreID = storeid;
                l_anagrafica.CreationDate = DateTime.Now;
                _context.Anagrafica.Add(l_anagrafica);
                _context.SaveChanges();

               return  Task.FromResult(1);
            }
            catch (Exception ex)
            {
                return Task.FromResult(0);
            }
        }

        public Task<int> DeleteAnagrafica(Anagrafica anag)
        {
            try
            {
                var l_anagrafica = _context.Anagrafica.Where(i => i.Email == anag.Email).SingleOrDefault();

                if (l_anagrafica == null) return Task.FromResult(-1);

                _context.Anagrafica.Remove(l_anagrafica);
                _context.SaveChangesAsync();

                return Task.FromResult(1);
            }
            catch (Exception ex)
            {
                return Task.FromResult(0);
            }
        }

        public IEnumerable<Anagrafica> GetAllAnagrafics()
        {
            return _context.Anagrafica.ToList();
        }




        public string[] SearchEmailLike(long storeID, string email)
        {
            

            var value =_context.Anagrafica
                .Where(i => i.Email.Contains(email) && i.StoreID ==storeID)
                .Select(i => i.Email)
                .ToArray();
            return value;
        }

        public Task<int> UpdateAnagrafica(Anagrafica anag)
        {
         try
            {
                var l_anagrafica = _context.Anagrafica.Where(i => i.Email == anag.Email).SingleOrDefault();

                if (l_anagrafica == null) return Task.FromResult(-1);

                l_anagrafica.Cognome = anag.Cognome;
                l_anagrafica.Nome = anag.Nome;
                l_anagrafica.LastModifiedDate = DateTime.Now;
                l_anagrafica.Telefono = anag.Telefono;

                _context.Anagrafica.Update(l_anagrafica);
                _context.SaveChangesAsync();

                return Task.FromResult(1);
            }
            catch (Exception ex)
            {
                return Task.FromResult(0);
            }
        }
    }
}
