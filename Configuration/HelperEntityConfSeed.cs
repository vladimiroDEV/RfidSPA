using RfidSPA.Data;
using RfidSPA.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.Configuration
{
    public class HelperEntityConfSeed
    {

        private readonly ApplicationDbContext _appDbContext;

        public HelperEntityConfSeed(ApplicationDbContext context )
        {
            _appDbContext = context;
        }
        public  async void Seed()
        {

            List<TypeDeviceHistoryOperation> typeDeviceHistoryOperations = new List<TypeDeviceHistoryOperation>()
            {
                new TypeDeviceHistoryOperation {TypeRfidDeviceOperationID = 1, Description="Creazione Device"},
                new TypeDeviceHistoryOperation {TypeRfidDeviceOperationID = 2, Description="Join To Anagrafica"},
                new TypeDeviceHistoryOperation {TypeRfidDeviceOperationID = 3, Description="Leave Anagrafica"},
                new TypeDeviceHistoryOperation {TypeRfidDeviceOperationID = 4, Description="Modifica Device"},
            };

            foreach(var item in typeDeviceHistoryOperations)
            {
                if (!_appDbContext.TypeDeviceHistoryOperations.Any(i => i.TypeRfidDeviceOperationID == item.TypeRfidDeviceOperationID))
                {
                  await  _appDbContext.TypeDeviceHistoryOperations.AddAsync(item);
                }
            }
          
        }
    }
}
