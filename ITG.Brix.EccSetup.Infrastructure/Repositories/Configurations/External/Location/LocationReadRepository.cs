using AutoMapper;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;
using ITG.Brix.EccSetup.Infrastructure.Providers.Impl;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories.Configurations.External
{
    public class LocationReadRepository : ILocationReadRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILocationOdataProvider _odataProvider;
        private readonly IMapper _mapper;

        public LocationReadRepository(DataContext dataContext, ILocationOdataProvider odataProvider, IMapper mapper)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _odataProvider = odataProvider ?? throw new ArgumentNullException(nameof(odataProvider));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected IMongoCollection<LocationClass> Collection => _dataContext.Database.GetCollection<LocationClass>(Consts.Collections.LocationCollectionName);

        public Task<Location> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Location>> ListAsync(string filter, int? skip, int? limit)
        {
            var filterDefinition = _odataProvider.GetFilterPredicate(filter);

            IFindFluent<LocationClass, LocationClass> fluent = null;

            if (filterDefinition == null)
            {
                var filterEmpty = Builders<LocationClass>.Filter.Empty;
                fluent = Collection.Find(filterEmpty);
            }
            else
            {
                fluent = Collection.Find(filterDefinition);
            }

            fluent = fluent.Skip(skip).Limit(limit);

            var locationClasses = await fluent.ToListAsync();

            var result = _mapper.Map<IEnumerable<Location>>(locationClasses);

            return result;
        }
    }
}
