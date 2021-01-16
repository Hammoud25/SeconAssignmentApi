using SecondAssignmentApi.IModels;

namespace SecondAssignmentApi.Models
{
    public class OwnedAppartment : IOwnedAppartment
    {
        public int OwnerId { get; set; }
        public Buyer Owner { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public int Rooms { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }

        public OwnedAppartment() { }
    }
}
