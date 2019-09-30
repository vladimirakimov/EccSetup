using AutoMapper;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Dtos;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks.Validations;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.Flows;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Model.Flows;
using ITG.Brix.EccSetup.Domain.Model.OperatorActivities;
using ITG.Brix.EccSetup.Domain.ValueObjects.Enumerations;
using System;

namespace ITG.Brix.EccSetup.Application.MappingProfiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<SortOrderEnum, SortOrderEnumModel>();

            CreateMap<StorageStatus, StorageStatusModel>();
            CreateMap<DamageCode, DamageCodeModel>();
            CreateMap<Location, LocationModel>();
            CreateMap<Customer, CustomerModel>();

            CreateMap<Tag, string>().ConvertUsing(x => x.Name);
            CreateMap<Operation, OperationModel>();

            CreateMap<Site, SiteModel>();

            CreateMap<OperationalDepartment, OperationalDepartmentModel>();

            CreateMap<ProductionSite, ProductionSiteModel>();

            CreateMap<TransportType, TransportTypeModel>();

            CreateMap<TypePlanning, TypePlanningModel>();

            CreateMap<Source, SourceModel>();
            CreateMap<SourceBusinessUnit, string>().ConvertUsing(x => x.Name);

            CreateMap<Icon, IconDto>();
            CreateMap<Icon, IconModel>();
            CreateMap<ColoredIcon, ColoredIconModel>().ForMember(x => x.DataPath, opt => opt.Ignore());

            CreateMap<BusinessUnit, BusinessUnitModel>();

            CreateMap<Layout, LayoutModel>();

            CreateMap<Tag, TagModel>();
            CreateMap<Checklist, ChecklistModel>();
            CreateMap<Question, QuestionModel>();
            CreateMap<ChecklistAnswer, ChecklistAnswerModel>();

            CreateMap<ImageDto, Image>();
            CreateMap<VideoDto, Video>();
            CreateMap<ImageModel, ImageModel>();
            CreateMap<Video, VideoModel>();
            CreateMap<Input, InputModel>();

            CreateMap<BuildingBlockIconDto, BuildingBlockIcon>();
            CreateMap<Validation, ValidationModel>();

            #region Flow
            CreateMap<BuildingBlockDto, BuildingBlock>().ForMember(x => x.Version, opt => opt.Ignore());
            CreateMap<BuildingBlock, BuildingBlockModel>();

            CreateMap<FlowOperation, string>().ConvertUsing(x => x.Name);
            CreateMap<FlowSource, string>().ConvertUsing(x => x.Name);
            CreateMap<FlowSite, string>().ConvertUsing(x => x.Name);
            CreateMap<FlowOperationalDepartment, string>().ConvertUsing(x => x.Name);
            CreateMap<FlowTypePlanning, string>().ConvertUsing(x => x.Name);
            CreateMap<FlowCustomer, string>().ConvertUsing(x => x.Name);
            CreateMap<FlowProductionSite, string>().ConvertUsing(x => x.Name);
            CreateMap<FlowTransportType, string>().ConvertUsing(x => x.Name);

            CreateMap<FlowFilter, FlowFilterModel>();
            CreateMap<Flow, FlowModel>();
            #endregion

            #region OperatorActivity
            CreateMap<OperatorActivity, OperatorActivityModel>();
            CreateMap<OperatorActivityDto, OperatorActivity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Version, opt => opt.Ignore());
            #endregion

            CreateMap<RemarkIcon, Guid>().ConvertUsing(x => x.IconId);
            CreateMap<Information, InformationModel>();
        }
    }
}
