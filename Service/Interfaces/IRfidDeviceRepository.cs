using RfidSPA.Models.Entities;
using RfidSPA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.Service.Interfaces
{
    public interface IRfidDeviceRepository
    {
        IEnumerable<RfidDevice> GetAllRfids();

        RfidDevice RfidByCode(string code);

        RfidDevice RfidByID(long id);

        List<RfidDevice> RfidsByAnagraficaID(long ID);

        bool CeateNewRfid(AnagraficaRfidDeviceModel item);

        bool PaidByRfid(PaidModel paidmodel);

        bool paidOffRfid(string code);

        bool paidOffAllRfids(List<RfidDevice> listRfids);


        List<RfidDeviceTransaction> GetAllTransactionsToConfirm(string code);

        UserDetailViewModel getGeatailUserByEmail(string email);
        UserDetailViewModel getGeatailUserByRfidCode(string code);

    }
}
