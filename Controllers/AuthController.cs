using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecondAssignmentApi.Data;
using SecondAssignmentApi.Dtos;
using SecondAssignmentApi.IModels;
using SecondAssignmentApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly IAuthRepo repo;
        private readonly IMapper mapper;
        private readonly IBuyerRepository bRepo;

        public AuthController(IConfiguration _config, IAuthRepo _repo, IMapper _mapper,IBuyerRepository _BRepo)
        {
            config = _config;
            repo = _repo;
            mapper = _mapper;
            bRepo = _BRepo;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(BuyerForRegisterDto buyerinfo)
        {
            buyerinfo.FullName = buyerinfo.FullName.ToLower();
            if (await repo.BuyerExists(buyerinfo.FullName) != 0) return BadRequest();
            var createdBuyer =await repo.Register(buyerinfo);
            
            if (await repo.SaveAll())
            {
                var buyerid = await repo.BuyerExists(createdBuyer.FullName);
                var buyerToreturn = mapper.Map<BuyerToReturn>(createdBuyer);
                buyerToreturn.Id = buyerid;
                return CreatedAtRoute("GetBuyer", new { id = createdBuyer.Id }, buyerToreturn);
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(BuyerForLoginDto buyerinfo)
        {
            var BuyerFromRepo = await repo.Login(buyerinfo);

            if (BuyerFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, BuyerFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, BuyerFromRepo.FullName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));

            var Creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = Creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var Token = tokenHandler.CreateToken(TokenDescriptor);

            return Ok(new
            {
                Token = tokenHandler.WriteToken(Token),
            });
        }
    }
}
