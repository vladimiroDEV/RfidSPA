﻿using System.Threading.Tasks;
using RfidSPA.Models.Entities;
using System.Collections.Generic;

namespace RfidSPA.Service
{
    public interface IStoreRepository
    {
        Task<long> CreateStore(Store stroreModel);
        Task<int> UpdateStore(Store stroreModel);
        Task<int> DeleteStore(long  storeID);


        Task<long> GetstoreIdByUser(string userID);

        Store GetstoreByID(long  ID);
        
        Task<int> AddStoreOperator(long StoreID, StoreUsers storeUser);
        Task<int> RemoveStoreOperator(long StoreID, string storeUserID);

        Task <List<ApplicationUser>> GetStoreUsers(long StoreID);
    }
}