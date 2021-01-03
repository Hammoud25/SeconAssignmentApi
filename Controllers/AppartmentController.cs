
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondAssignmentApi.Data;
using SecondAssignmentApi.Dtos;
using SecondAssignmentApi.Extenions;
using SecondAssignmentApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var users = await repo.GetAppartments(userParams);
            var usersToReturn = mapper.Map<IEnumerable<AppartmentForListDto>>(users);
            Response.AddPagination(users.CurrentPage, users.PageSize,users.TotalCount,users.TotalPages);
            return Ok(usersToReturn);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppartment(int id, AppartmentForUpdateDto updateDto)
        {
            var appartmentFromRepo = await repo.GetAppartment(id);
            mapper.Map(updateDto, appartmentFromRepo);
            if (await repo.SaveAll())
            {
                return NoContent();
            }
            return BadRequest("An error occurred on saving appartment n.b: {id}");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppartment(int id)
        {
            var appartmentFromRepo = await repo.GetAppartment(id);
            repo.Delete(appartmentFromRepo);

            if (await repo.SaveAll())
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
