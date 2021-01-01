
using AutoMapper;
using SecondAssignmentApi.Dtos;
using SecondAssignmentApi.IModels;
using SecondAssignmentApi.Models;

namespace SecondAssignmentApi.Extenions
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ApartmentForCreationDto, Appartment>();
            CreateMap<Appartment, AppartmentoReturn>();
            CreateMap<BuyerForCreationDto, Buyer>().ForMember(dest => dest.Credit, opt => opt.MapFrom(src => src.InitialCredit));
        }
    }
}
