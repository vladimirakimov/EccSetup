using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class SourceReadRepository : BaseReadRepository<Source>, ISourceReadRepository
    {
        private readonly DataContext _dataContext;

        public SourceReadRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override IMongoCollection<Source> Collection =>
                                _dataContext.Database.GetCollection<Source>(Consts.Collections.SourceCollectionName);
    }
}
