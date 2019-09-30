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
    public class UpdateSourceCommandHandler : IRequestHandler<UpdateSourceCommand, Result>
    {
        private readonly ISourceReadRepository _sourceReadRepository;
        private readonly ISourceWriteRepository _sourceWriteRepository;
        private readonly IVersionProvider _versionProvider;

        public UpdateSourceCommandHandler(ISourceWriteRepository sourceWriteRepository,
                                          ISourceReadRepository sourceReadRepository,
                                          IVersionProvider versionProvider)
        {
            _sourceWriteRepository = sourceWriteRepository ?? throw Error.ArgumentNull(nameof(sourceWriteRepository));
            _sourceReadRepository = sourceReadRepository ?? throw Error.ArgumentNull(nameof(sourceReadRepository));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                var source = await _sourceReadRepository.GetAsync(request.Id);
                if (source.Version != request.Version)
                {
                    throw new CommandVersionException();
                }

                if (request.Name.HasValue)
                {
                    source.ChangeName(request.Name.Value);
                }
                if (request.Description.HasValue)
                {
                    source.ChangeDescription(request.Description.Value);
                }
                if (request.SourceBusinessUnits.HasValue)
                {
                    var sourceBusinessUnits = request.SourceBusinessUnits.Value ?? new List<string>();
                    source.ClearSourceBusinessUnits();
                    foreach (var sourceBusinessUnit in sourceBusinessUnits)
                    {
                        source.AddSourceBusinessUnit(new SourceBusinessUnit(sourceBusinessUnit));
                    }
                }

                source.Version = _versionProvider.Generate();
                await _sourceWriteRepository.UpdateAsync(source);
                result = Result.Ok(source.Version);

            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Source"),
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
                result = Result.Fail(CustomFailures.UpdateSourceFailure);
            }

            return result;
        }
    }
}
