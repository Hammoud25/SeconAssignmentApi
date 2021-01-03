using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondAssignmentApi.Data;
using SecondAssignmentApi.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerRepository repo;
        private readonly IMapper mapper;

        public BuyerController(IBuyerRepository _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }

        [HttpGet("{id}", Name = "GetBuyer")]
        public async Task<IActionResult> GetBuyer(int id)
        {
            var buyer = await repo.GetBuyer(id);
            var buyerToReturn = mapper.Map<BuyerToReturn>(buyer);
            return Ok(buyerToReturn);
        }

        [HttpPost("{buyerid}/buy/{appid}")]
        public async Task<IActionResult> Buy([FromRoute] int buyerid, [FromRoute] int appid)
        {
            var response = await repo.Buy(buyerid, appid);
            if (!response.result)
            {
                return BadRequest();
            }
            if (await repo.SaveAll())
            {
                var id = repo.GetId(response.OwnedAppartment.Address, buyerid);
                response.OwnedAppartment.Id = id.Result;
                return CreatedAtRoute("GetOwnedAppartment", new { id = response.OwnedAppartment.OwnerId, appid = response.OwnedAppartment.Id }, response);
            }
            return BadRequest();

        }

        [HttpGet("{id}/{appid}", Name = "GetOwnedAppartment")]
        public async Task<IActionResult> GetAppartmentForBuyer(int id, int appid)
        {
            var buyer = await repo.GetBuyer(id);
            var ownedAppartment = buyer.OwnedAppartments.AsQueryable().Where(d => d.Id == appid);
            return Ok(ownedAppartment);
        }
        [HttpGet]
        public async Task<IActionResult> GetBuyers()
        {
            var buyers = await repo.GetBuyers();
            var buyersListDto = mapper.Map<IEnumerable<BuyersListDto>>(buyers);
            return Ok(buyersListDto);
        }
        [HttpGet("{id}/apps")]
        public async Task<IActionResult> GetAppartments(int id)
        {
            var appartments = await repo.GetOwnedAppartments(id);

            var appartmentsForreturn = mapper.Map<ICollection<AppartmentForListDto>>(appartments);

            return Ok(appartmentsForreturn);
        }
    }
}
