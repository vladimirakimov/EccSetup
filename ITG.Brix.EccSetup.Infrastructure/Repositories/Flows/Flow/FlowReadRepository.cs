using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class FlowReadRepository : BaseReadRepository<Flow>, IFlowReadRepository
    {
        private readonly DataContext _dataContext;
        private readonly IFlowOdataProvider _flowOdataProvider;

        public FlowReadRepository(DataContext dataContext,
                                  IFlowOdataProvider flowOdataProvider)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _flowOdataProvider = flowOdataProvider ?? throw new ArgumentNullException(nameof(flowOdataProvider));
        }
        protected override IMongoCollection<Flow> Collection => _dataContext.Database.GetCollection<Flow>(Consts.Collections.FlowCollectionName);

        public async Task<IEnumerable<Flow>> ListAsync(string filter, int? skip, int? limit)
        {

            var filterDefinition = _flowOdataProvider.GetFilterDefinition(filter);
            var fluent = Collection.Find(filterDefinition);
            fluent = fluent.Skip(skip).Limit(limit);
            var flows = await fluent.ToListAsync();

            return flows;
        }
    }
}
