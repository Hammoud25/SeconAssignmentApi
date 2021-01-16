using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondAssignmentApi.Data;
using SecondAssignmentApi.Dtos;
using SecondAssignmentApi.Models;
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
        private readonly IAppartmentRepository apprepo;

        public BuyerController(IBuyerRepository _repo, IMapper _mapper, IAppartmentRepository _apprepo)
        {
            repo = _repo;
            mapper = _mapper;
            apprepo = _apprepo;
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
            if (!await apprepo.AppartmentExists(appid))
            {
                return BadRequest("Appartment doesnt exist.");
            }

            if (!await repo.BuyerExists(buyerid)) return BadRequest("Buyer doesnt exist.");

            var apprtmentFromRepo = await apprepo.GetAppartment(appid);

            if (apprtmentFromRepo == null)
            {
                return BadRequest();
            }
            var buyerFromRepo =await repo.GetBuyer(buyerid);

            if (buyerFromRepo.Credit < apprtmentFromRepo.Price) return BadRequest("Buyer doesnt have enough credit.");

            var ownedAppartment = mapper.Map<OwnedAppartment>(apprtmentFromRepo);

            ownedAppartment = await repo.Buy(buyerFromRepo, ownedAppartment, appid);

            if (await repo.SaveAll())
            {
                var id =await repo.GetId(ownedAppartment.Address, buyerid);
                ownedAppartment.Id = id;
                return CreatedAtRoute("GetOwnedAppartment", new { id = ownedAppartment.OwnerId, appid = ownedAppartment.Id }, ownedAppartment);
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
