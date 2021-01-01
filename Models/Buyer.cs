
using SecondAssignmentApi.IModels;
using System.Collections.Generic;

namespace SecondAssignmentApi.Models
{
    public class Buyer : IBuyer
    {
        public string FullName { get; set; }
        public int Id { get; set; }
        public int Credit { get; set; }
        public ICollection<OwnedAppartment> OwnedAppartments { get; set; }

        public Buyer()
        {

        }
    }
}