using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions.OperatorActivity;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Dtos;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Application.Services.Json;
using ITG.Brix.EccSetup.Domain.Model.OperatorActivities;
using ITG.Brix.EccSetup.Domain.Repositories.OperatorActivities;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using MediatR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers
{
    public class CreateOperatorActivityCommandHandler : IRequestHandler<CreateOperatorActivityCommand, Result>
    {
        private readonly IOperatorActivityWriteRepository _operatorActivityWriteRepository;
        private readonly IVersionProvider _versionProvider;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IJsonService<object> _jsonService;
        private readonly IMapper _mapper;

        public CreateOperatorActivityCommandHandler(IOperatorActivityWriteRepository operatorActivityWriteRepository,
                                                    IVersionProvider versionProvider,
                                                    IIdentifierProvider identifierProvider,
                                                    IJsonService<object> jsonService,
                                                    IMapper mapper)
        {
            _operatorActivityWriteRepository = operatorActivityWriteRepository ?? throw new ArgumentNullException(nameof(operatorActivityWriteRepository));
            _versionProvider = versionProvider ?? throw new ArgumentNullException(nameof(versionProvider));
            _identifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
            _jsonService = jsonService ?? throw new ArgumentNullException(nameof(jsonService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result> Handle(CreateOperatorActivityCommand request, CancellationToken cancellationToken)
        {
            dynamic jObject = request.OperatorActivities;
            var activitiesArray = JArray.Parse(jObject.Activities.ToString()) as JArray;
            var activities = _jsonService.ToObject<List<OperatorActivityDto>>(activitiesArray);
            var result = Result.Ok();

            try
            {
                foreach (var activityDto in activities)
                {
                    var id = _identifierProvider.Generate();
                    var version = _versionProvider.Generate();
                    var operatorActivity = new OperatorActivity(id);
                    _mapper.Map(activityDto, operatorActivity);
                    operatorActivity.Version = version;

                    await _operatorActivityWriteRepository.CreateAsync(operatorActivity);

                    result = Result.Ok(id, operatorActivity.Version);
                }
            }
            catch (Exception)
            {
                result = Result.Fail(CustomFailures.CreateOperatorActivityFailure);
            }

            return result;
        }
    }
}
