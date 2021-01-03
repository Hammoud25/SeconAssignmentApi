using SecondAssignmentApi.Dtos;

namespace SecondAssignmentApi.Extenions
{
    public class BuyOperation : IBuyOperationResult
    {
        public OwnedAppartmentForReturn OwnedAppartment { get; set; }
        public bool result { get; set; }

    }
}
