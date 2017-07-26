using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.Models.Entities
{
    public class RfidDeviceHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RfidDeviceHistoryID { get; set; }

        public string RfidDeviceCode { get; set; }

        public DateTime? InsertDate { get; set; }

        public string ApplicationUserID { get; set; }

        public bool Active { get; set; }

        public long? AnagraficaID { get; set; }

        public int RfidDeviceOperation { get; set; }
    }
}
