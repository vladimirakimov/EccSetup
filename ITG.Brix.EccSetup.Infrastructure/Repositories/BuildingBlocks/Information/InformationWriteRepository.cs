using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;
using System;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class InformationWriteRepository : BaseWriteRepository<Information>, IInformationWriteRepository
    {
        private readonly DataContext _dataContext;

        public InformationWriteRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected override IMongoCollection<Information> Collection => _dataContext.Database.GetCollection<Information>(Consts.Collections.InformationCollectionName);
    }
}
