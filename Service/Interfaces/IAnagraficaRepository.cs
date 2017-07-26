using RfidSPA.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.Service.Interfaces
{
    public interface IAnagraficaRepository
    {
        IEnumerable<Anagrafica> GetAllAnagrafics();

        Anagrafica AnagraficaByID(long ID);

        Anagrafica AnagraficaByEmail(string email);

        string[] SearchEmailLike(string email);
    }
}
