using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondAssignmentApi.Dtos;
using SecondAssignmentApi.Models;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Data
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private byte[] passwordHash;
        private byte[] passwordSalt;

        public AuthRepo(DataContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<int> BuyerExists(string fullname)
        {
            if (await context.Buyers.AnyAsync(x => x.FullName == fullname))
            {
                var id = context.Buyers.FirstOrDefaultAsync(x => x.FullName == fullname).Result.Id;
                return id;
            } 
            return 0;
        }

        public async Task<Buyer> Login(BuyerForLoginDto buyerinfo)
        {
            var buyer = await context.Buyers.FirstOrDefaultAsync(x => x.FullName == buyerinfo.FullName);
            if (buyer == null)
            {
                return null;
            }
            if (!VerifyPasswordHash(buyerinfo.Password, buyer.PasswordHash, buyer.PasswordSalt))
            {
                return null;
            }
            return buyer;
        }

        public async Task<Buyer> Register(BuyerForRegisterDto buyerinfo)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(buyerinfo.Password, out passwordHash, out passwordSalt);
            var buyer = mapper.Map<Buyer>(buyerinfo);

            buyer.PasswordHash = passwordHash;
            buyer.PasswordSalt = passwordSalt;

            await context.Buyers.AddAsync(buyer);

            return buyer;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < ComputedHash.Length; i++)
                {
                    if (ComputedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> SaveAll()
        {

            var result = await context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            };
            return false;

        }
    }
}
