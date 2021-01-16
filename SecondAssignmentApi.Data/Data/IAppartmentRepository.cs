using SecondAssignmentApi.Extenions;
using SecondAssignmentApi.Models;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Data
{
    public interface IAppartmentRepository
    {
        Task<Appartment> Create(Appartment appartment);
        Task<bool> AppartmentExists(string title);
        Task<bool> AppartmentExists(int id);
        Task<Appartment> GetAppartment(int id);
        Task<bool> SaveAll();
        Task<PagedList<Appartment>> GetAppartments(UserParams userParams);
        void Delete<T>(T entity) where T : class;
    }
}
