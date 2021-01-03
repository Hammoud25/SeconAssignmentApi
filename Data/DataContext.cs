using Microsoft.EntityFrameworkCore;
using SecondAssignmentApi.Models;

namespace SecondAssignmentApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Appartment> AppartmentsForSale { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<OwnedAppartment> OwnedAppartments { get; set; }
    }
}