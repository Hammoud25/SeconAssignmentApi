
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecondAssignmentApi.Data;
using SecondAssignmentApi.Dtos;
using SecondAssignmentApi.Models;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppartmentController : ControllerBase
    {
        private readonly IAppartmentRepository repo;
        private readonly IMapper mapper;

        public AppartmentController(IAppartmentRepository _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateAppartment(ApartmentForCreationDto appartmentocreate)
        {
            if (await repo.AppartmentExists(appartmentocreate.Address))
            {
                return BadRequest("Appartment Already Exists!");
            }

            Appartment CreatedAppartment = mapper.Map<Appartment>(appartmentocreate);
            await repo.Create(CreatedAppartment);
            if (await repo.SaveAll())
            {
                return CreatedAtRoute("GetAppartment", new { id = CreatedAppartment.Id }, CreatedAppartment);
            }
            return BadRequest();
        }
        [HttpGet("{id}", Name = "GetAppartment")]
        public async Task<IActionResult> GetAppartment([FromRoute] int id)
        {
            var appartment = await repo.GetAppartment(id);
            var appartmenttoreturn = mapper.Map<AppartmentoReturn>(appartment);
            return Ok(appartmenttoreturn);
        }
    }
}
