﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Dtos
{
    public class BuyersListDto
    {
        public int Credit { get; set; }
        public string FullName { get; set; }
        public int Id { get; set; }
        public int PropertyCount { get; set; }
    }
}
