using RfidSPA.Data;
using RfidSPA.Models.Entities;
using RfidSPA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.Service
{
    public class RfidDeviceRepository : IRfidDeviceRepository
    {
        private readonly ApplicationDbContext _context;


        public RfidDeviceRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public IEnumerable<RfidDevice> GetAllRfids()
        {

            return _context.RfidDevice.ToList();
        }


        public RfidDevice RfidByCode(string code)
        {
            return _context.RfidDevice.Where(i => i.RfidDeviceCode == code).SingleOrDefault();
        }

        public RfidDevice RfidByID(long id)
        {
            return _context.RfidDevice.Where(i => i.RfidDeviceID == id).SingleOrDefault();
        }

        //public List<RfidDevice> RfidsByAnagraficaID(long ID)
        //{
        //    return _context.RfidDevice.Where(i => i.AnagraficaID == ID).ToList();
        //}

        public bool CeateNewRfid(AnagraficaRfidDeviceModel  item)
        {

            try
            {
                var l_anag = _context.Anagrafica.Where(i => i.Email == item.anagrafica.Email).SingleOrDefault();

                // check id user exists 
                // se user non esiste crea uno nuovo 
                if (l_anag == null)
                {
                    Anagrafica anag = item.anagrafica;
                    anag.CreationDate = DateTime.Now;
                    _context.Anagrafica.Add(anag);
                    _context.SaveChanges();

                    l_anag = _context.Anagrafica.Where(i => i.Email == item.anagrafica.Email).SingleOrDefault();

                }

                item.anagrafica.AnagraficaID = l_anag.AnagraficaID;
                item.anagrafica
                    .AnagraficaID = l_anag.AnagraficaID;
                var rfid = _context.RfidDevice.Where(i => i.RfidDeviceCode == item.device.RfidDeviceCode).SingleOrDefault();


                if (rfid != null)
                {
                    rfid.AnagraficaID = item.anagrafica.AnagraficaID;
                    rfid.ExpirationDate = item.device.ExpirationDate;
                    rfid.LastModifiedDate = DateTime.Now;
                    rfid.Credit = 0;
                    rfid.Active = true;

                    //update
                    _context.RfidDevice.Update(rfid);
                    _context.SaveChanges();

                    //aggiorna history
                    updateRfidHistory(rfid, RfidOperations.Assegna);
                }
                else
                {
                    // new 

                    RfidDevice l_rfid = new RfidDevice();
                    l_rfid.RfidDeviceCode = item.device.RfidDeviceCode;
                    l_rfid.ExpirationDate = item.device.ExpirationDate;
                    l_rfid.CreationDate = DateTime.Now;
                    l_rfid.LastModifiedDate = DateTime.Now;
                    l_rfid.Credit = 0;
                    l_rfid.ApplicationUserID = item.device.ApplicationUserID;
                    l_rfid.Active = true;
                    l_rfid.AnagraficaID = l_anag.AnagraficaID;

                    _context.RfidDevice.AddAsync(l_rfid);
                    _context.SaveChanges();

                    //aggiorna history
                    updateRfidHistory(l_rfid, RfidOperations.Assegna);
                }

                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }


        public bool PaidByRfid(PaidModel paidModel)
        {

            bool result = true;
            try
            {
                var rfid = RfidByCode(paidModel.RfidCode);
                if (rfid != null)
                {
                    Transaction trans = new Transaction();
                    trans.RfidCode = rfid.RfidCode;
                    trans.AnagraficaID = rfid.AnagraficaID;
                    trans.AppUserID = rfid.AppUserID;
                    trans.OperationID = (int)TransactionOperation.Pagamento;
                    trans.TransactionDate = DateTime.Now;
                    trans.Importo = paidModel.Price;
                    trans.Descrizione = paidModel.Descrizione;


                    _context.Transaction.Add(trans);
                    _context.SaveChanges();
                }
                else result = false;
            }

            catch (Exception e)
            {
                result = false;
            }

            return result;

        }

        public bool paidOffRfid(string code)
        {

            var rfid = _context.Rfids.Where(i => i.RfidCode == code).SingleOrDefault();
            if (rfid == null) return false;
            try
            {

                _context.Transaction.Add(new Transaction
                {
                    AnagraficaID = rfid.AnagraficaID,
                    AppUserID = rfid.AppUserID,
                    Descrizione = "Saldo conto",
                    Importo = rfid.Credit,
                    RfidCode = rfid.RfidCode,
                    OperationID = (int)TransactionOperation.SaldoDebito,
                    Confirmed = true,
                    ConfirmedDate = DateTime.Now,
                    TransactionDate = DateTime.Now
                });
                rfid.Credit = 0;
                rfid.AnagraficaID = null;
                _context.Update(rfid);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool paidOffAllRfids(List<Rfid> listRfids)
        {

            try
            {
                foreach (var rfid in listRfids)
                {

                    var res = paidOffRfid(rfid.RfidCode);
                    if (res == false) return false;
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        #region Methods 

        void updateRfidHistory(RfidDevice rfid, RfidOperations operation)
        {

            RfidDeviceHistory rfidHistory = new RfidDeviceHistory();
            rfidHistory.RfidDeviceID = rfid.RfidDeviceID;
            rfidHistory.RfidDeviceCode = rfid.RfidDeviceCode;
            rfidHistory.InsertDate = DateTime.Now;
            rfidHistory.Rfid = (int)operation;
            rfidHistory.AppUserID = rfid.AppUserID;
            rfidHistory.Active = rfid.Active;
            rfidHistory.AnagraficaID = rfid.AnagraficaID;

            _context.RfidHistory.AddAsync(rfidHistory);
            _context.SaveChanges();


        }

        public List<Transaction> GetAllTransactionsToConfirm(string code)
        {

            List<Transaction> listTr = new List<Transaction>();

            listTr = _context.Transaction
                .Where(i => i.Confirmed == false && i.RfidCode == code).ToList();

            return listTr;
        }

        public UserDetailViewModel getGeatailUserByEmail(string email)
        {
            var user = _context.Anagrafica.Where(i => i.Email == email).SingleOrDefault();

            if (user == null) return null;

            return new UserDetailViewModel { Anagrafica = user, Dispositivi = getDeviceByUser(user.AnagraficaID, true) };

        }

        public UserDetailViewModel getGeatailUserByRfidCode(string code)
        {
            var disp = _context.Rfids.Where(i => i.RfidCode == code).SingleOrDefault();

            if (disp == null) return null;
            var user = _context.Anagrafica.Where(i => i.AnagraficaID == disp.AnagraficaID).SingleOrDefault();

            if (user == null) return null;
            return new UserDetailViewModel { Anagrafica = user, Dispositivi = getDeviceByUser(user.AnagraficaID, true) };
        }
        #endregion

        #region localMethos 

        List<Rfid> getDeviceByUser(long id, bool? active = true)
        {
            return _context.Rfids.Where(i => i.AnagraficaID == id && i.Active == active).ToList();
        }







        #endregion

    }
}
