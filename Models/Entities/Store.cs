using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.Models.Entities
{
    public class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long StoreID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public string Telefono { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public string CreatorUser { get; set; }

        public string AdministratorID { get; set; }

        public bool Active { get; set; }

        public virtual  List<StoreUsers> storeUsers { get; set; }

        public  virtual ICollection<RfidDevice> Devices { get; set; }




    }
}
