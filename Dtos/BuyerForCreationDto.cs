using System.ComponentModel.DataAnnotations;

namespace SecondAssignmentApi.Dtos
{
    public class BuyerForCreationDto
    {
        [Required]
        public string FullName { get; set; }
        public int InitialCredit { get; set; }
    }
}
