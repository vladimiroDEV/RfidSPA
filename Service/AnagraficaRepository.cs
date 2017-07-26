using RfidSPA.Data;
using RfidSPA.Models.Entities;
using RfidSPA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.Service
{
    public class AnagraficaRepository: IAnagraficaRepository
    {
        private readonly ApplicationDbContext _context;


        public AnagraficaRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public Anagrafica AnagraficaByEmail(string email)
        {
            return _context.Anagrafica.Where(i => i.Email == email).SingleOrDefault();
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
            return _context.Anagrafica
                .Where(i => i.Email.Contains(email))
                .Select(i => i.Email)
                .ToArray();
        }
    }
}
