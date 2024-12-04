using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.Reflection.Metadata;
using UPBus_API.DTOs;
using UPBus_API.Entities;

namespace UPBus_API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
   
            CreateMap<UserForRegistrationDto, IdentityUser>().ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<Bus, BusDto>().ReverseMap();
            CreateMap<Gate, GateDto>().ReverseMap();
            CreateMap<ExpenseType, ExpenseTypeDto>().ReverseMap();
            CreateMap<IncomeType, IncomeTypeDto>().ReverseMap();
            CreateMap<TrackType, TrackTypeDto>().ReverseMap();
            CreateMap<GasStation, GasStationDto>().ReverseMap();

            CreateMap<DailyPlan, DailyPlanDto>().ReverseMap();

        }
    }
}
