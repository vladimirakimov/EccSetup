using AutoMapper;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Model.OperatorActivities;
using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;

namespace ITG.Brix.EccSetup.Infrastructure.MappingProfiles
{
    public class DomainToClassProfile : Profile
    {
        public DomainToClassProfile()
        {
            CreateMap<OperatorActivity, OperatorActivityClass>();
           
            CreateMap<Location, LocationClass>()
                .ForMember(dest => dest.G, opt => opt.MapFrom(src => src.Gate))
                .ForMember(dest => dest.P, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.R, opt => opt.MapFrom(src => src.Row))
                .ForMember(dest => dest.Ra, opt => opt.MapFrom(src => src.IsRack))
                .ForMember(dest => dest.Sc, opt => opt.MapFrom(src => src.Source))
                .ForMember(dest => dest.St, opt => opt.MapFrom(src => src.Site))
                .ForMember(dest => dest.T, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.W, opt => opt.MapFrom(src => src.Warehouse))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version));
        }
    }
}
