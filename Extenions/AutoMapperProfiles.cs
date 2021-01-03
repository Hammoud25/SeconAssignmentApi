
using AutoMapper;
using SecondAssignmentApi.Dtos;
using SecondAssignmentApi.Models;

namespace SecondAssignmentApi.Extenions
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ApartmentForCreationDto, Appartment>();
            CreateMap<Appartment, AppartmentoReturn>();
            CreateMap<BuyerForRegisterDto, Buyer>().ForMember(dest => dest.Credit, opt => opt.MapFrom(src => src.InitialCredit))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()).ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());
            //CreateMap<Buyer, BuyerForRegisterDto>();
            CreateMap<Appartment, AppartmentForListDto>();
            CreateMap<Buyer, BuyerToReturn>();
            CreateMap<Appartment, OwnedAppartment>().ForMember(dest => dest.Owner, opt => opt.Ignore())
                .ForMember(dest => dest.OwnerId, opt => opt.Ignore()).ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<OwnedAppartment, OwnedAppartmentForReturn>();
            CreateMap<Buyer, BuyersListDto>().ForMember(dest => dest.PropertyCount, opt => opt.MapFrom(s => s.OwnedAppartments.Count));
            CreateMap<OwnedAppartment, AppartmentForListDto>();
            CreateMap<AppartmentForUpdateDto, Appartment>();
        }
    }
}
