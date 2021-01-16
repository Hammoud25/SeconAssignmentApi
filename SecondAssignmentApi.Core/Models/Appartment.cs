using SecondAssignmentApi.IModels;

namespace SecondAssignmentApi.Models
{
    public class Appartment : IAppartment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Rooms { get; set; }
        public int Price { get => Rooms * 15000; set { } }
        public string Address { get; set; }

        public Appartment() { }

    }
}
