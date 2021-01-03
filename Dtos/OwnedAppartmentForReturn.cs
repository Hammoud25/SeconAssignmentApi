using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Dtos
{
    public class OwnedAppartmentForReturn
    {
        public int OwnerId { get; set; }
        public BuyerToReturn Owner { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public int Rooms { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }
    }
}
