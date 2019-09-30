using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class SourceWriteRepository : BaseWriteRepository<Source>, ISourceWriteRepository
    {
        private readonly DataContext _dataContext;

        public SourceWriteRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override IMongoCollection<Source> Collection =>
                                    _dataContext.Database.GetCollection<Source>(Consts.Collections.SourceCollectionName);

    }
}
