using System.Threading.Tasks;
using RfidSPA.Models.Entities;

namespace RfidSPA.Service
{
    public interface IStoreRepository
    {
        Task<long> CreateStore(Store stroreModel);

        Task<long> GetstoreIdByUser(string userID);
    }
}