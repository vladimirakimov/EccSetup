using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ITG.Brix.EccSetup.Domain.Model.OperatorActivities;
using ITG.Brix.EccSetup.Domain.Repositories.OperatorActivities;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories.OperatorActivities
{
    public class OperatorActivityReadRepository : IOperatorActivityReadRepository
    {
        private readonly DataContext _dataContext;
        private readonly IOperatorActivityOdataProvider _odataProvider;
        private readonly IMapper _mapper;

        public OperatorActivityReadRepository(DataContext dataContext,
                                              IOperatorActivityOdataProvider operatorActivityOdataProvider,
                                              IMapper mapper)
        {
            _dataContext = dataContext;
            _odataProvider = operatorActivityOdataProvider ?? throw new ArgumentNullException(nameof(operatorActivityOdataProvider));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected IMongoCollection<OperatorActivityClass> Collection => _dataContext.Database.GetCollection<OperatorActivityClass>(Consts.Collections.OperatorActivityCollectionName);

        public Task<OperatorActivity> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OperatorActivity>> ListAsync(string filter, int? skip, int? limit)
        {
            var filterDefinition = _odataProvider.GetFilterPredicate(filter);

            IFindFluent<OperatorActivityClass, OperatorActivityClass> fluent = null;

            if (filterDefinition == null)
            {
                var filterEmpty = Builders<OperatorActivityClass>.Filter.Empty;
                fluent = Collection.Find(filterEmpty);
            }
            else
            {
                fluent = Collection.Find(filterDefinition);
            }

            fluent = fluent.Skip(skip).Limit(limit);

            var operatorActivityClasses = await fluent.ToListAsync();

            var result = _mapper.Map<IEnumerable<OperatorActivity>>(operatorActivityClasses);

            return result;
        }
    }
}
