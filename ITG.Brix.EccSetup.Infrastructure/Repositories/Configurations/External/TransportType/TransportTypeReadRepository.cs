using System;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class TransportTypeReadRepository : BaseReadRepository<TransportType>, ITransportTypeReadRepository
    {
        private readonly DataContext _dataContext;

        public TransportTypeReadRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected override IMongoCollection<TransportType> Collection =>
                                    _dataContext.Database.GetCollection<TransportType>(Consts.Collections.TransportTypeCollectionName);
    }
}
