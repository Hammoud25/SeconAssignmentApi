using SecondAssignmentApi.Models;
using System.Collections.Generic;

namespace SecondAssignmentApi.IModels
{
    public interface IBuyer
    {
        string FullName { get; set; }
        int Credit { get; set; }
        int Id { get; set; }
        byte[] PasswordHash { get; set; }
        byte[] PasswordSalt { get; set; }
        ICollection<OwnedAppartment> OwnedAppartments { get; set; } 
    }
}
