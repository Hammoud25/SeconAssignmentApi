using SecondAssignmentApi.Dtos;

namespace SecondAssignmentApi.Extenions
{
    public interface IBuyOperationResult
    {
        OwnedAppartmentForReturn OwnedAppartment { get; set; }
        bool result { get; set; }
    }
}
