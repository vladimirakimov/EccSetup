using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions.File;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.Files;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers.File
{
    public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, Result>
    {
        private readonly IBlobStorage _blobStorage;
        private readonly IMapper _mapper;

        public DownloadFileQueryHandler(IBlobStorage blobStorage,
                                        IMapper mapper)
        {
            _blobStorage = blobStorage ?? throw new ArgumentNullException(nameof(blobStorage));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var fileDto = await _blobStorage.DownloadFileAsync(request.File);
                var fileModel = _mapper.Map<FileDownloadModel>(fileDto);

                result = Result.Ok(fileModel);
            }
            catch (BlobNotFoundException)
            {
                result = Result.Fail(new List<Failure>()
                {
                    new HandlerFault(){
                        Code = HandlerFaultCode.NotFound.Name,
                        Message =string.Format(HandlerFailures.NotFound, "File"),
                        Target = "file"
                    }
                });
            }
            catch
            {
                result = Result.Fail(CustomFailures.DownloadFileFailure);
            }

            return result;
        }
    }
}
