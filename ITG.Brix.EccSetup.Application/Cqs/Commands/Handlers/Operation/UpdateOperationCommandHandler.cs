using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Exceptions;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers
{
    public class UpdateOperationCommandHandler : IRequestHandler<UpdateOperationCommand, Result>
    {
        private readonly IOperationWriteRepository _operationWriteRepository;
        private readonly IOperationReadRepository _operationReadRepository;
        private readonly IVersionProvider _versionProvider;

        public UpdateOperationCommandHandler(IOperationWriteRepository operationWriteRepository,
                                             IOperationReadRepository operationReadRepository,
                                             IVersionProvider versionProvider)
        {
            _operationWriteRepository = operationWriteRepository ?? throw Error.ArgumentNull(nameof(operationWriteRepository));
            _operationReadRepository = operationReadRepository ?? throw Error.ArgumentNull(nameof(operationReadRepository));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(UpdateOperationCommand request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                var operation = await _operationReadRepository.GetAsync(request.Id);
                if (operation.Version != request.Version)
                {
                    throw new CommandVersionException();
                }

                //var coloredIcon = new ColoredIcon(command.Icon.IconId, command.Icon.FillColor);

                if (request.Name.HasValue)
                {
                    operation.ChangeName(request.Name.Value);
                }
                if (request.Description.HasValue)
                {
                    operation.SetDescription(request.Description.Value);
                }

                if (request.Tags.HasValue)
                {
                    var tags = request.Tags.Value ?? new List<string>();
                    operation.ClearTags();
                    foreach (var tag in tags)
                    {
                        operation.AddTag(new Tag(tag));
                    }
                }

                operation.Version = _versionProvider.Generate();
                await _operationWriteRepository.UpdateAsync(operation);
                result = Result.Ok(operation.Version);

            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Operation"),
                                            Target = "id"}
                                        }
                );
            }
            catch (CommandVersionException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotMet.Name,
                                            Message = HandlerFailures.NotMet,
                                            Target = "version"}
                                        }
                );
            }
            catch (UniqueKeyException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.Conflict.Name,
                                            Message = HandlerFailures.Conflict,
                                            Target = "name"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.UpdateOperationFailure);
            }

            return result;
        }
    }
}
