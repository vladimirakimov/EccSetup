using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions.File;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using ITG.Brix.EccSetup.Infrastructure.Storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers.File
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Result>
    {
        private readonly IBlobStorage _blobStorage;
        private readonly IFileNameProvider _fileNameProvider;

        public UploadFileCommandHandler(IBlobStorage blobStorage,
                                        IFileNameProvider fileNameProvider)
        {
            _blobStorage = blobStorage ?? throw new ArgumentNullException(nameof(blobStorage));
            _fileNameProvider = fileNameProvider ?? throw new ArgumentNullException(nameof(fileNameProvider));
        }

        public async Task<Result> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            Result result;
            var fileName = _fileNameProvider.Generate(request.FileExtension);

            try
            {
                var uri = await _blobStorage.UploadFileToStorage(request.File, fileName);
                result = Result.Ok(uri);
            }
            catch (UniqueFileNameException)
            {
                result = Result.Fail(new List<Failure>()
                {
                    new HandlerFault()
                    {
                        Code = HandlerFaultCode.Conflict.Name,
                        Message = HandlerFailures.Conflict,
                        Target = "name"
                    }
                });
            }
            catch
            {
                result = Result.Fail(CustomFailures.UploadFileFailure);
            }

            return result;
        }
    }
}
