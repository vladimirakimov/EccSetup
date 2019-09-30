using AutoMapper;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.Files;
using ITG.Brix.EccSetup.Infrastructure.Storage.Dtos;

namespace ITG.Brix.EccSetup.Application.MappingProfiles
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<FileDownloadDto, FileDownloadModel>();
        }
    }
}
