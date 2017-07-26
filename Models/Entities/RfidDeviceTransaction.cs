using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.Models.Entities
{
    public class RfidDeviceTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RfidDeviceTransactionID { get; set; }

        public string RfidDeviceCode { get; set; }

        public long? AnagraficaID { get; set; }

        public long ApplicationUserID { get; set; }

        public int TransactionOperation { get; set; }

        public DateTime TransactionDate { get; set; }

        public double? Importo { get; set; }

        public string Descrizione { get; set; }

        // indica quando la transazaione è completata
        // viene scritto true quando il pagamento viene pagato il totale
        //public bool Confirmed { get; set; }

        //public DateTime? ConfirmedDate { get; set; }


    }
}
