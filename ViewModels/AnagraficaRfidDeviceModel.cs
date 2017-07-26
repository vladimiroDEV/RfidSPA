using RfidSPA.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.ViewModels
{
    public class AnagraficaRfidDeviceModel
    {
       public RfidDevice device { get; set; }
       public Anagrafica anagrafica { get; set; }
    }
}
