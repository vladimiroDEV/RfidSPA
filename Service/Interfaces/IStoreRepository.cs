using System.Threading.Tasks;
using RfidSPA.Models.Entities;

namespace RfidSPA.Service
{
    public interface IStoreRepository
    {
        Task<bool> CreateStore(Store stroreModel);
    }
}