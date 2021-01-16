using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecondAssignmentApi.Data;
using SecondAssignmentApi.Extenions;

namespace SecondAssignmentApi.Root
{
    public class ServiceConfiguration
    {
        public static void InjectDependencies(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlite
            (Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(PaginationHeader).Assembly);
            services.AddScoped<IAppartmentRepository, AppartmentRepository>();
            services.AddScoped<IBuyerRepository, BuyeRepository>();
            services.AddScoped<IAuthRepo, AuthRepo>();

        }
    }
}
