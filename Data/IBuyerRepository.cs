using SecondAssignmentApi.Extenions;
using SecondAssignmentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Data
{
    public interface IBuyerRepository
    {
        
        Task<bool> SaveAll();
        Task<Buyer> GetBuyer(int id);
        Task<bool> BuyerExists(string fullname);
        Task<IBuyOperationResult> Buy(int buyerid, int appid);
        Task<IEnumerable<Buyer>> GetBuyers();
        Task<ICollection<OwnedAppartment>> GetOwnedAppartments(int id);
        Task<int> GetId(string Address, int id);
    }
}
