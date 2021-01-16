using System.ComponentModel.DataAnnotations;

namespace SecondAssignmentApi.Dtos
{
    public class ApartmentForCreationDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int Rooms { get; set; }
        [Required]
        public string Address { get; set; }
    }
}