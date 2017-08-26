using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.Models.Entities
{
    public class Anagrafica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AnagraficaID { get; set; }

        [Required]
        public string Email { get; set; }

        public string Nome { get; set; }

        public string Cognome { get; set; }

        public string Telefono { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public string ApplicationUserID { get; set; }

        public long StoreID { get; set; }
        public virtual Store Store { get; set; }

        [ForeignKey("ApplicationUserID")]
        public virtual  ApplicationUser ApplicationUser { get; set; }

    }
}
