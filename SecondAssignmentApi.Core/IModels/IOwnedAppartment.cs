using SecondAssignmentApi.Models;

namespace SecondAssignmentApi.IModels
{
    public interface IOwnedAppartment : IAppartment
    {
        int OwnerId { get; }
        Buyer Owner { get; set; }
    }
}
