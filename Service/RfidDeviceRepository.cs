using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RfidSPA.Data;
using RfidSPA.Models.Entities;
using RfidSPA.Service.Interfaces;
using RfidSPA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.Service
{
    public class RfidDeviceRepository : IRfidDeviceRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAcessor;
        private readonly string _appCurrentUserID;
        private readonly ILogger _logger;


        public RfidDeviceRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAcessor, ILoggerFactory loggerFactory)
        {
            _appDbContext = context;
            _httpContextAcessor = httpContextAcessor;
            _appCurrentUserID = _httpContextAcessor.HttpContext.User.Claims.Single(c => c.Type == "id").Value;
            _logger = loggerFactory.CreateLogger<RfidDeviceRepository>();
        }



        public IEnumerable<RfidDevice> GetAllRfids()
        {

            return _appDbContext.RfidDevice.ToList();
        }


        public RfidDevice RfidByCode(string code)
        {
            return _appDbContext.RfidDevice.Where(i => i.RfidDeviceCode == code && i.ApplicationUserID == _appCurrentUserID).SingleOrDefault();
        }

        public RfidDevice RfidByID(long id)
        {
            return _appDbContext.RfidDevice.Where(i => i.RfidDeviceID == id).SingleOrDefault();
        }



        public List<RfidDevice> RfidsByAnagraficaID(long ID)
        {
            return _appDbContext.RfidDevice.Where(i => i.AnagraficaID == ID && i.ApplicationUserID == _appCurrentUserID).ToList();
        }

        public bool CeateNewRfid(AnagraficaRfidDeviceModel  item)
        {

            try
            {
                var l_anag = _appDbContext.Anagrafica.Where(i => i.Email == item.anagrafica.Email && i.ApplicationUserID == _appCurrentUserID).SingleOrDefault();

                // check id user exists 
                // se user non esiste crea uno nuovo 
                if (l_anag == null)
                {
                    Anagrafica anag = item.anagrafica;
                    anag.CreationDate = DateTime.Now;
                    anag.ApplicationUserID =  _appCurrentUserID;
                    _appDbContext.Anagrafica.Add(anag);
                    _appDbContext.SaveChanges();

                    l_anag = _appDbContext.Anagrafica.Where(i => i.Email == item.anagrafica.Email && i.ApplicationUserID == _appCurrentUserID).SingleOrDefault();

                }

                item.anagrafica.AnagraficaID = l_anag.AnagraficaID;
               
                var rfid = _appDbContext.RfidDevice
                    .Where(i => i.RfidDeviceCode == item.device.RfidDeviceCode && i.ApplicationUserID == _appCurrentUserID)
                    .SingleOrDefault();


                if (rfid != null)
                {
                    rfid.AnagraficaID = item.anagrafica.AnagraficaID;
                    rfid.ExpirationDate = item.device.ExpirationDate;
                    rfid.LastModifiedDate = DateTime.Now;
                    rfid.Credit = 0;
                    rfid.Active = true;

                    //update
                    _appDbContext.RfidDevice.Update(rfid);
                    _appDbContext.SaveChanges();

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

                    _appDbContext.RfidDevice.AddAsync(l_rfid);
                    _appDbContext.SaveChanges();

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


        // paga con il dispositivo
        public bool PaidByRfid(PaidModel paidModel)
        {

            bool result = true;
            try
            {
                var rfid = RfidByCode(paidModel.RfidCode);
                if (rfid != null)
                {
                    if(rfid.AnagraficaID == null || rfid.AnagraficaID ==0)
                    {
                        return false; // non è associata nessuna anagrafica 
                    }
                    RfidDeviceTransaction trans = new RfidDeviceTransaction();
                    trans.RfidDeviceCode = rfid.RfidDeviceCode;
                    trans.AnagraficaID = rfid.AnagraficaID;
                    trans.ApplicationUserID = rfid.ApplicationUserID;
                    trans.TransactionOperation = (int)TransactionOperation.Pagamento;
                    trans.TransactionDate = DateTime.Now;
                    trans.Importo = paidModel.Price;
                    trans.Descrizione = paidModel.Descrizione;
                    trans.PaydOff = false;

                    rfid.Credit += paidModel.Price;
                    _appDbContext.Update(rfid);
                    _appDbContext.RfidDeviceTransaction.Add(trans);
                    _appDbContext.SaveChanges();
                }
                else result = false;
            }

            catch (Exception e)
            {
                result = false;
            }

            return result;

        }



        // salda il conto e restituisci il dissassocia il dispositivo da l'utente 
        public bool paidOffRfid(string code)
        {

            try
            {
                var listTr = _appDbContext.RfidDeviceTransaction
                .Where(i => i.PaydOff == false
                        && i.RfidDeviceCode == code
                        && i.AnagraficaID != null
                        && i.ApplicationUserID == _appCurrentUserID
                        && i.TransactionOperation == (int)TransactionOperation.Pagamento)
                 .ToList();
                var rfid = _appDbContext.RfidDevice
                    .Where(i => i.RfidDeviceCode == code
                       && i.AnagraficaID != null
                       && i.ApplicationUserID == _appCurrentUserID)
                    .SingleOrDefault();
            
           

                if (listTr != null)
                {
                    foreach (var item in listTr)
                    {
                        item.PaydOff = true;
                        item.PaydOffDate = DateTime.Now;
                        _appDbContext.RfidDeviceTransaction.Update(item);

                    }
                }
                if (rfid != null){


                    _appDbContext.RfidDeviceTransaction.Add(new RfidDeviceTransaction
                    {
                        AnagraficaID = rfid.AnagraficaID,
                        ApplicationUserID = rfid.ApplicationUserID,
                        Descrizione = "Saldo conto",
                        Importo = rfid.Credit,
                        RfidDeviceCode = rfid.RfidDeviceCode,
                        TransactionOperation = (int)TransactionOperation.SaldoDebito,
                        PaydOff = true,
                        PaydOffDate = DateTime.Now,
                        TransactionDate = DateTime.Now
                    });
                    rfid.Credit = 0;
                    rfid.AnagraficaID = null;
                    _appDbContext.Update(rfid);
                    _appDbContext.SaveChanges();
                    updateRfidHistory(rfid, RfidOperations.Restituisci);


                    return true;
                }
                else return false;

            }
           
             catch
            {
                return false;
            }

        }

        public bool paidOffAllRfids(List<RfidDevice> listRfids)
        {

            try
            {
                foreach (var rfid in listRfids)
                {

                    var res = paidOffRfid(rfid.RfidDeviceCode);
                    if (res == false) return false;
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<RfidDeviceTransaction> getAllTransactionsToPaydOff(string code)
        {

            List<RfidDeviceTransaction> listTr = new List<RfidDeviceTransaction>();

            listTr = _appDbContext.RfidDeviceTransaction
                .Where(i => i.PaydOff == false
                        && i.RfidDeviceCode == code
                        && i.AnagraficaID != null
                        && i.ApplicationUserID == _appCurrentUserID
                        && i.TransactionOperation == (int)TransactionOperation.Pagamento)
                 .ToList();

            return listTr;
        }

        public UserDetailViewModel getGeatailUserByEmail(string email)
        {
            var user = _appDbContext.Anagrafica.Where(i => i.Email == email).SingleOrDefault();

            if (user == null) return null;

            return new UserDetailViewModel { Anagrafica = user, Dispositivi = getDeviceByUser(user.AnagraficaID, true) };

        }

        public UserDetailViewModel getGeatailUserByRfidCode(string code)
        {
            var disp = _appDbContext.RfidDevice
                .Where(i => i.RfidDeviceCode == code
                    && i.Active == true
                    && i.AnagraficaID != null
                    && i.ApplicationUserID == _appCurrentUserID)
                .SingleOrDefault();

            if (disp == null) return null;
            var user = _appDbContext.Anagrafica.Where(i => i.AnagraficaID == disp.AnagraficaID).SingleOrDefault();

            if (user == null) return null;
            return new UserDetailViewModel { Anagrafica = user, Dispositivi = getDeviceByUser(user.AnagraficaID, true) };
        }

  


        List<RfidDevice> getDeviceByUser(long id, bool? active = true)
        {
            return _appDbContext.RfidDevice.Where(i => i.AnagraficaID == id && i.Active == active).ToList();
        }


        // new 

        public async  Task<List<RfidDevice>> getDevicesByApplicationUsers()
        {

            var result = await _appDbContext.RfidDevice.Where(i => i.ApplicationUserID == _appCurrentUserID).ToListAsync();
            return result;
        }

        public   Task<int> createRfidDevice(RfidDevice device)
        { 
            try
            {
                var  l_device = _appDbContext.RfidDevice.Where(d => d.RfidDeviceCode == device.RfidDeviceCode).SingleOrDefault();
                

                // se è null crea uno nuovo 
                if(l_device == null)
                { 
                    l_device.RfidDeviceCode = device.RfidDeviceCode;
                    l_device.ExpirationDate =device.ExpirationDate;
                    l_device.CreationDate = DateTime.Now;
                    l_device.LastModifiedDate = DateTime.Now;
                    l_device.Credit = 0;
                    l_device.ApplicationUserID = device.ApplicationUserID;
                    l_device.StoreID = device.StoreID;
                    l_device.Active = true;

                    RfidDeviceHistory history = new RfidDeviceHistory
                    {
                        RfidDevice = l_device,
                        InsertDate = DateTime.Now,
                        TypeOperation = (int)TypeDeviceHistoryOperationEN.CreazioneDevice
                    };
                    _appDbContext.RfidDevice.AddAsync(l_device);
                    _appDbContext.RfidDeviceHistory.AddAsync(history);

                    _appDbContext.SaveChanges();

                    return Task.FromResult(1); 
                }
                else
                {
                    return  Task.FromResult(-1); // device exists
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Task.FromResult(0);
            }
        }

        public Task<int> UpdateDevice(RfidDevice device)
        {
            var l_device = _appDbContext.RfidDevice.Where(i => i.RfidDeviceID == device.RfidDeviceID).SingleOrDefault();
            if (l_device == null) return Task.FromResult(-1);

            try
            {
                l_device.Active = device.Active;
                l_device.AnagraficaID = device.AnagraficaID;
                l_device.Credit = device.Credit;
                l_device.LastModifiedDate = DateTime.Now;
                _appDbContext.RfidDevice.Update(l_device);
                _appDbContext.SaveChangesAsync();
                return Task.FromResult(1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Task.FromResult(0);
            }
        }

        public async  Task<int> JoinDeviseToAnagrafica(RfidDevice device)
        {
            var createRes =  await  createRfidDevice(device);

            var l_anagrafica = _appDbContext.Anagrafica.Where(i => i.AnagraficaID == device.AnagraficaID).SingleOrDefault();
            if(l_anagrafica == null) // crea nuova anagrafica 
            {

                l_anagrafica.CreationDate = DateTime.Now;
                l_anagrafica.ApplicationUserID = device.ApplicationUserID;
                l_anagrafica.
                _appDbContext.Anagrafica.Add(anag);
                _appDbContext.SaveChanges();

            }
            if(createRes == -1 ) // device Esiste 
            {
              

            }
        }

        public Task<int> LogicDeleteDevice(string deviceCode)
        {
            var l_device = _appDbContext.RfidDevice.Where(i => i.RfidDeviceCode == deviceCode).SingleOrDefault();
            if (l_device == null) return Task.FromResult(-1);

            try
            {
                l_device.Active = false;
                l_device.AnagraficaID = null;
                l_device.Credit = 0;
                l_device.LastModifiedDate = DateTime.Now;
                _appDbContext.RfidDevice.Update(l_device);
                _appDbContext.SaveChangesAsync();
                return Task.FromResult(1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Task.FromResult(0);
            }
        }

        public Task<List<RfidDeviceTransaction>> GetDeviceDeviceTransactions(string deviceID)
        {
            throw new NotImplementedException();
        }

        public Task<List<RfidDeviceHistory>> GetDeviceDeviceRfidDeviceHistory(string deviceID)
        {
            throw new NotImplementedException();
        }







     

    }
}
