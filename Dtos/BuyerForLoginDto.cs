using System.ComponentModel.DataAnnotations;

namespace SecondAssignmentApi.Dtos
{
    public class BuyerForLoginDto
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
