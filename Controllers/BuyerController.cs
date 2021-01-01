using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecondAssignmentApi.Data;
using SecondAssignmentApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyeRepository repo;
        private readonly Mapper mapper;

        public BuyerController(IBuyeRepository _repo, Mapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateBuyer(BuyerForCreationDto buyerForCreationDto)
        {
            if (await repo.BuyerExists(buyerForCreationDto.FullName))
            {
                return BadRequest("Title already exists!");
            }
            var CreatedBuyer = mapper.Map<Buyer>
        }
    }
}
