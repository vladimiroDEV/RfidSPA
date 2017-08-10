using System.Threading.Tasks;
using RfidSPA.Models.Entities;

namespace RfidSPA.Service
{
    public interface IAccountRepository
    {
        Task<ChangePasswordStatus> ChangePassword(ChangepasswordModel model);
    }
}