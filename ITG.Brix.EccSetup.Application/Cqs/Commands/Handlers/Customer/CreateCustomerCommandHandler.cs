using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories.Customers;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result>
    {
        private readonly ICustomerWriteRepository _customerWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateCustomerCommandHandler(ICustomerWriteRepository customerWriteRepository, IIdentifierProvider identifierProvider, IVersionProvider versionProvider)
        {
            _customerWriteRepository = customerWriteRepository ?? throw new ArgumentNullException(nameof(customerWriteRepository));
            _identifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw new ArgumentNullException(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            Result result;
            var id = _identifierProvider.Generate();

            var customerToCreate = new Customer(id, request.Code, request.Name, request.Source);

            customerToCreate.Version = _versionProvider.Generate();

            try
            {
                await _customerWriteRepository.CreateAsync(customerToCreate);
                result = Result.Ok(id, customerToCreate.Version);
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
                result = Result.Fail(CustomFailures.CreateCustomerFailure);
            }

            return result;
        }
    }
}
