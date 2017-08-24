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

        public string RfidDeviceID { get; set; }

        public virtual RfidDevice RfidDevice { get; set; }

        public DateTime? InsertDate { get; set; }

        public DateTime OperationDate { get; set; }

        public string ApplicationUserID { get; set; }

        public int TypeOperation { get; set; }
        [ForeignKey("TypeOperation")]
        public virtual  TypeDeviceHistoryOperation TypeDeviceHistoryOperation { get; set; }
    }
}
