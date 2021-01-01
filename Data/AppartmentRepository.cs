using Microsoft.EntityFrameworkCore;
using SecondAssignmentApi.Models;
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
    }
}
