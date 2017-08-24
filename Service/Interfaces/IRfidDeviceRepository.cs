﻿using RfidSPA.Models.Entities;
using RfidSPA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.Service.Interfaces
{
    public interface IRfidDeviceRepository
    {

        // new

        Task<int> createRfidDevice(RfidDevice device);
        Task<int> UpdateDevice(RfidDevice device);
        Task<int> LogicDeleteDevice(string  deviceID);

        Task<List<RfidDeviceTransaction>> GetDeviceDeviceTransactions(string deviceCode);
        Task<List<RfidDeviceHistory>> GetDeviceDeviceRfidDeviceHistory(string deviceCode);


        //end new 



        IEnumerable<RfidDevice> GetAllRfids();

        RfidDevice RfidByCode(string code);

        RfidDevice RfidByID(long id);

        List<RfidDevice> RfidsByAnagraficaID(long ID);

        bool CeateNewRfid(AnagraficaRfidDeviceModel item);

        bool PaidByRfid(PaidModel paidmodel);

        bool paidOffRfid(string code);

        bool paidOffAllRfids(List<RfidDevice> listRfids);


        List<RfidDeviceTransaction> getAllTransactionsToPaydOff(string code);
         Task<List<RfidDevice>> getDevicesByApplicationUsers();

        UserDetailViewModel getGeatailUserByEmail(string email);
        UserDetailViewModel getGeatailUserByRfidCode(string code);

    }
}
