using SecondAssignmentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Data
{
    interface IBuyerRepository
    {
        Task<Buyer> Create(Buyer buyer);
        Task<bool> SaveAll();
        Task<Buyer> GetBuyer(int id);
        Task<bool> BuyerExists(string fullname);
    }
}
