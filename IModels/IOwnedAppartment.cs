using SecondAssignmentApi.Models;

namespace SecondAssignmentApi.IModels
{
    public interface IOwnedAppartment : IAppartment
    {
        int OwnerId { get => Owner.Id;}
        Buyer Owner { get; set; }
    }
}
