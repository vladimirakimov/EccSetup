﻿using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers
{
    public class DeleteInputCommandHandler : IRequestHandler<DeleteInputCommand, Result>
    {
        private readonly IInputWriteRepository _inputWriteRepository;

        public DeleteInputCommandHandler(IInputWriteRepository inputWriteRepository)
        {
            _inputWriteRepository = inputWriteRepository ?? throw Error.ArgumentNull(nameof(inputWriteRepository));
        }

        public async Task<Result> Handle(DeleteInputCommand request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                await _inputWriteRepository.DeleteAsync(request.Id, request.Version);
                result = Result.Ok();
            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Input"),
                                            Target = "id"}
                                        }
                );
            }
            catch (EntityVersionDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotMet.Name,
                                            Message = HandlerFailures.NotMet,
                                            Target = "version"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.DeleteInputFailure);
            }

            return result;
        }
    }
}
