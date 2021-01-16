using SecondAssignmentApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Data
{
    public interface IBuyerRepository
    {

        Task<bool> SaveAll();
        Task<Buyer> GetBuyer(int id);
        Task<bool> BuyerExists(string fullname);
        Task<bool> BuyerExists(int id);
        Task<OwnedAppartment> Buy(Buyer buyer, OwnedAppartment ownedAppartment, int appid);
        Task<IEnumerable<Buyer>> GetBuyers();
        Task<ICollection<OwnedAppartment>> GetOwnedAppartments(int id);
        Task<int> GetId(string Address, int id);
    }
}
