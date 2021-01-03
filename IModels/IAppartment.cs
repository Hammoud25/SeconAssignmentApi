namespace SecondAssignmentApi.IModels
{
    public interface IAppartment
    {
        int Id { get; set; }
        string Title { get; set; }
        int Rooms { get; set; }
        int Price { get; set; }
        string Address { get; set; }
    }
}
