using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;
using System;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories.Configurations.External
{
    public class DamageCodeWriteRepository : BaseWriteRepository<DamageCode>, IDamageCodeWriteRepository
    {
        private readonly DataContext _dataContext;

        public DamageCodeWriteRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected override IMongoCollection<DamageCode> Collection => _dataContext.Database.GetCollection<DamageCode>(Consts.Collections.DamageCodeCollectionName);
    }
}
