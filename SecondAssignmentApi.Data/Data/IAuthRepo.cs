using SecondAssignmentApi.Dtos;
using SecondAssignmentApi.Models;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Data
{
    public interface IAuthRepo
    {
        Task<Buyer> Register(BuyerForRegisterDto buyerinfo);
        Task<int> BuyerExists(string fullname);
        Task<Buyer> Login(BuyerForLoginDto buyerinfo);
        Task<bool> SaveAll();
    }
}
