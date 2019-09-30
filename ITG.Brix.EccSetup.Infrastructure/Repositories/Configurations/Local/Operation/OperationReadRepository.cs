using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;
using System;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class OperationReadRepository : BaseReadRepository<Operation>, IOperationReadRepository
    {
        private readonly DataContext _dataContext;

        public OperationReadRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected override IMongoCollection<Operation> Collection => _dataContext.Database.GetCollection<Operation>(Consts.Collections.OperationCollectionName);
    }
}
