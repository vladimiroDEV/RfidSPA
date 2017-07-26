using RfidSPA.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.ViewModels
{
    public class UserDetailViewModel
    {
        public Anagrafica Anagrafica { get; set; }
        public List<RfidDevice> Dispositivi { get; set; }
    }
}
