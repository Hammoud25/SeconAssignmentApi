using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondAssignmentApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Data
{
    public class BuyeRepository : IBuyerRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public BuyeRepository(DataContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public async Task<OwnedAppartment> Buy(Buyer buyer, OwnedAppartment ownedAppartment, int appid)
        {
            var appartment = await context.AppartmentsForSale.FirstOrDefaultAsync(p => p.Id == appid);

            ownedAppartment.Owner = buyer;

            ownedAppartment.OwnerId = buyer.Id;

            context.AppartmentsForSale.Remove(appartment);

            buyer.OwnedAppartments.Add(ownedAppartment);

            buyer.Credit -= ownedAppartment.Price;


            return ownedAppartment;
        }

        public async Task<bool> BuyerExists(string fullname)
        {
            if (await context.Buyers.AnyAsync(x => x.FullName == fullname)) return true;
            return false;
        }
        public async Task<bool> BuyerExists(int id)
        {
            if (await context.Buyers.AnyAsync(x => x.Id == id)) return true;
            return false;
        }
        public async Task<Buyer> GetBuyer(int id)
        {
            var buyer = await context.Buyers.Include(p => p.OwnedAppartments).FirstOrDefaultAsync(d => d.Id == id);
            return buyer;
        }

        public async Task<IEnumerable<Buyer>> GetBuyers()
        {
            return await context.Buyers.Include(p => p.OwnedAppartments).ToListAsync();
        }

        public async Task<int> GetId(string Address, int id)
        {
            var buyer = await context.Buyers.FirstOrDefaultAsync(x => x.Id == id);
            var appartment = buyer.OwnedAppartments.AsQueryable().FirstOrDefault(x => x.Address == Address);
            return appartment.Id;
        }

        public async Task<ICollection<OwnedAppartment>> GetOwnedAppartments(int id)
        {
            var buyerFromRepo = await context.Buyers.Include(p => p.OwnedAppartments).FirstOrDefaultAsync(d => d.Id == id);

            var ownedAppartments = buyerFromRepo.OwnedAppartments;

            return ownedAppartments;
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
