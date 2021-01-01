using SecondAssignmentApi.Models;

namespace SecondAssignmentApi.IModels
{
    public interface IOwnedAppartment : IAppartment
    {
        int OwnerId { get; set; }
        Buyer Owner { get; set; }
    }
}
