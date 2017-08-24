using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.Models.Entities
{
    public class RfidDevice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RfidDeviceID { get; set; }
        [Required]
        public string RfidDeviceCode { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public double? Credit { get; set; }

        public string  ApplicationUserID { get; set; }

        public bool Active { get; set; }

        public long? AnagraficaID { get; set; }

        public long? StoreID { get; set; }

        public virtual Store Store { get; set; }

        public virtual List<RfidDeviceTransaction> RfidDeviceTransaction { get; set; }
        public virtual List<RfidDeviceHistory> RfidDeviceHistory { get; set; }

        


    }
}
