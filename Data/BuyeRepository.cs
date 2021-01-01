using Microsoft.EntityFrameworkCore;
using SecondAssignmentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Data
{
    public class IBuyeRepository : IBuyerRepository
    {
        private readonly DataContext context;

        public IBuyeRepository(DataContext _context)
        {
            context = _context;
        }

        public async Task<bool> BuyerExists(string fullname)
        {
            if (await context.Buyers.AnyAsync(x => x.FullName == fullname)) return true;
            return false;
        }

        public async Task<Buyer> Create(Buyer buyer)
        {
            await context.Buyers.AddAsync(buyer);

            return buyer;
        }

        public async Task<Buyer> GetBuyer(int id)
        {
            var buyer = await context.Buyers.FirstOrDefaultAsync(d => d.Id == id);
            return buyer;
        }

        public async Task<bool> SaveAll()
        {
            var result = await context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            };
            return false;
        }
    }
}
