using AutoMapper;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Model.OperatorActivities;
using ITG.Brix.EccSetup.Infrastructure.Converters;
using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;
using System;

namespace ITG.Brix.EccSetup.Infrastructure.MappingProfiles
{
    public class ClassToDomainProfile : Profile
    {
        public ClassToDomainProfile()
        {
            CreateMap<string, BlockType>().ConvertUsing(new StringToBlockTypeConverter());
            CreateMap<OperatorActivityClass, OperatorActivity>();
            CreateMap<LocationClass, Location>()
                .ForMember(dest => dest.Gate, opt => opt.MapFrom(src => src.G))
                .ForMember(dest => dest.IsRack, opt => opt.MapFrom(src => src.Ra))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.P))
                .ForMember(dest => dest.Row, opt => opt.MapFrom(src => src.R))
                .ForMember(dest => dest.Site, opt => opt.MapFrom(src => src.St))
                .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Sc))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.T))
                .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.W))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version));
           
        }
    }
}
