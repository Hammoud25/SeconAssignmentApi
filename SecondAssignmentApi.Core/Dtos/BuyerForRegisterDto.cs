using System.ComponentModel.DataAnnotations;

namespace SecondAssignmentApi.Dtos
{
    public class BuyerForRegisterDto
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        public int InitialCredit { get; set; }
    }
}
