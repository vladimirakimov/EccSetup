using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers
{
    public class CreateStorageStatusCommandHandler : IRequestHandler<CreateStorageStatusCommand, Result>
    {
        private readonly IStorageStatusWriteRepository _storageStatusWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateStorageStatusCommandHandler(IStorageStatusWriteRepository storageStatusWriteRepository, IIdentifierProvider identifierProvider, IVersionProvider versionProvider)
        {
            _storageStatusWriteRepository = storageStatusWriteRepository ?? throw new ArgumentNullException(nameof(storageStatusWriteRepository));
            _identifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw new ArgumentNullException(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateStorageStatusCommand request, CancellationToken cancellationToken)
        {
            Result result;
            var id = _identifierProvider.Generate();

            var storageStatusToCreate = new StorageStatus(id, request.Code, request.Name, bool.Parse(request.Default), request.Source);

            storageStatusToCreate.Version = _versionProvider.Generate();

            try
            {
                await _storageStatusWriteRepository.CreateAsync(storageStatusToCreate);
                result = Result.Ok(id, storageStatusToCreate.Version);
            }
            catch (UniqueKeyException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.Conflict.Name,
                                            Message = HandlerFailures.CodeSourceConflict,
                                            Target = "code-source"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.CreateStorageStatusFailure);
            }

            return result;
        }
    }
}
