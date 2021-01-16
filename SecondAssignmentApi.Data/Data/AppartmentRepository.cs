using Microsoft.EntityFrameworkCore;
using SecondAssignmentApi.Extenions;
using SecondAssignmentApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Data
{
    public class AppartmentRepository : IAppartmentRepository
    {
        private DataContext context { get; }
        public AppartmentRepository(DataContext _context)
        {
            context = _context;
        }

        public async Task<bool> AppartmentExists(string address)
        {
            if (await context.AppartmentsForSale.AnyAsync(x => x.Address == address)) return true;
            return false;
        }

        public async Task<bool> AppartmentExists(int id)
        {
            if (await context.AppartmentsForSale.AnyAsync(x => x.Id == id)) return true;
            return false;
        }

        public async Task<Appartment> Create(Appartment appartment)
        {
            await context.AppartmentsForSale.AddAsync(appartment);

            return appartment;
        }

        public async Task<Appartment> GetAppartment(int id)
        {
            var appartment = await context.AppartmentsForSale.FirstOrDefaultAsync(d => d.Id == id);
            return appartment;
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

        public async Task<PagedList<Appartment>> GetAppartments(UserParams userParams)
        {
            var appartments = context.AppartmentsForSale.AsQueryable();
            appartments = appartments.Where(opt => opt.Price >= userParams.MinPrice);
            appartments = appartments.Where(opt => opt.Price < userParams.MaxPrice);
            appartments = appartments.Where(opt => opt.Address.Contains(userParams.ProvidedText.ToLower()));
            if (userParams.NbOfRooms != 0)
            {
                appartments = appartments.Where(opt => opt.Rooms == userParams.NbOfRooms);
            }
            appartments = appartments.OrderByDescending(apt => apt.Price);
            return await PagedList<Appartment>.CreateAsync(appartments, userParams.PageNumber, userParams.PageSize);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }
    }
}
