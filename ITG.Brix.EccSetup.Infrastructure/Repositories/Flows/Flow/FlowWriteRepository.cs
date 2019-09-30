using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;
using System;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class FlowWriteRepository : BaseWriteRepository<Flow>, IFlowWriteRepository
    {
        private readonly DataContext _dataContext;

        public FlowWriteRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }
        protected override IMongoCollection<Flow> Collection => _dataContext.Database.GetCollection<Flow>(Consts.Collections.FlowCollectionName);
    }
}
