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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers
{
    public class UpdateRemarkCommandHandler : IRequestHandler<UpdateRemarkCommand, Result>
    {
        private readonly IRemarkWriteRepository _remarkWriteRepository;
        private readonly IRemarkReadRepository _remarkReadRepository;
        private readonly IVersionProvider _versionProvider;

        public UpdateRemarkCommandHandler(IRemarkWriteRepository remarkWriteRepository,
                                          IRemarkReadRepository remarkReadRepository,
                                          IVersionProvider versionProvider)
        {
            _remarkWriteRepository = remarkWriteRepository ?? throw Error.ArgumentNull(nameof(remarkWriteRepository));
            _remarkReadRepository = remarkReadRepository ?? throw Error.ArgumentNull(nameof(remarkReadRepository));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(UpdateRemarkCommand request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var icon = await _remarkReadRepository.GetAsync(request.Id);
                if (icon.Version != request.Version)
                {
                    throw new CommandVersionException();
                }

                var updatedRemark = new Remark(request.Id, request.Name, request.NameOnApplication, request.Description, new RemarkIcon(request.Icon));
                request.Tags.ToList().ForEach(x => updatedRemark.AddTag(new Tag(x)));
                request.DefaultRemarks.ToList().ForEach(x => updatedRemark.AddDefaultRemark(new DefaultRemark(x)));

                updatedRemark.Version = _versionProvider.Generate();
                await _remarkWriteRepository.UpdateAsync(updatedRemark);
                result = Result.Ok(updatedRemark.Version);

            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Remark"),
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
                result = Result.Fail(CustomFailures.UpdateRemarkFailure);
            }

            return result;
        }
    }
}
