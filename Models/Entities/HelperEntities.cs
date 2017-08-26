using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.Models.Entities
{
    public enum TypeDeviceHistoryOperationEN
    {
        CreazioneDevice = 1,
        JoinToAnagrafica =2,
        LeaveAnagrafica = 3,
        ModificaDevice = 4


    }
    public class TypeDeviceHistoryOperation
    {
        [Key]
        public int TypeRfidDeviceOperationID { get; set; }
        public string  Description { get; set; }

    }
}
