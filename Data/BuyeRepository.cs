using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondAssignmentApi.Dtos;
using SecondAssignmentApi.Extenions;
using SecondAssignmentApi.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public async Task<IBuyOperationResult> Buy(int buyerid, int appid)
        {
            if (!await context.AppartmentsForSale.AnyAsync(d => d.Id == appid))
            {
                return new BuyOperation() { result = false };
            }

            if (!await BuyerExists(buyerid)) return new BuyOperation() { result = false };

            Appartment appartmentFromRepo = await context.AppartmentsForSale.FirstOrDefaultAsync(d => d.Id == appid);

            Buyer buyerFromRepo = context.Buyers.Include(p => p.OwnedAppartments).FirstOrDefault(d => d.Id == buyerid);
            if (appartmentFromRepo.Price > buyerFromRepo.Credit)
            {
                return new BuyOperation() { result = false };
            }

            OwnedAppartment ownedAppartment = mapper.Map<OwnedAppartment>(appartmentFromRepo);

            ownedAppartment.Owner = buyerFromRepo;

            ownedAppartment.OwnerId = buyerid;

            context.AppartmentsForSale.Remove(appartmentFromRepo);


            buyerFromRepo.OwnedAppartments.Add(ownedAppartment);

            IBuyOperationResult buyOperationResult = new BuyOperation();

            buyOperationResult.OwnedAppartment = mapper.Map<OwnedAppartmentForReturn>(ownedAppartment);

            buyOperationResult.result = true;

            buyerFromRepo.Credit -= ownedAppartment.Price;

            buyOperationResult.OwnedAppartment.Owner.Credit = buyerFromRepo.Credit;

            return buyOperationResult;
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
