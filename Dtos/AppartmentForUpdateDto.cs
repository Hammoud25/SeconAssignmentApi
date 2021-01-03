using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Dtos
{
    public class AppartmentForUpdateDto
    {
        public int Rooms { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
    }
}
