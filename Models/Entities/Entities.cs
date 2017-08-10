using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.Models.Entities
{
    public class PaidModel
    {

        public string RfidCode { get; set; }
        public double Price { get; set; }
        public string Descrizione { get; set; }

    }
    public class ChangepasswordModel
    {
        public string newPassword { get; set; }
        public string oldPassword { get; set; }
    }


    public enum RfidOperations
    {
        Assegna = 1,
        Restituisci = 2,


    }
    public enum TransactionOperation
    {
        Pagamento = 1,
        SaldoDebito = 2
    }

    public enum OprationStatus
    {
        Success= 1,
        DeviseIsAssigned=2,
        DeviceNotAssigned=3,
        DeviceNotFound = 4,

    }

    public enum ChangePasswordStatus
    {
        succces =1,
        passwordNotMatch = 2,
        errorChange  =3
    }
}
