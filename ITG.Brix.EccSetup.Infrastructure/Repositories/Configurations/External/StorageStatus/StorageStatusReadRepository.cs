using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;
using System;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class StorageStatusReadRepository : BaseReadRepository<StorageStatus>, IStorageStatusReadRepository
    {
        private readonly DataContext _dataContext;

        public StorageStatusReadRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected override IMongoCollection<StorageStatus> Collection => _dataContext.Database.GetCollection<StorageStatus>(Consts.Collections.StorageStatusCollectionName);
    }
}
