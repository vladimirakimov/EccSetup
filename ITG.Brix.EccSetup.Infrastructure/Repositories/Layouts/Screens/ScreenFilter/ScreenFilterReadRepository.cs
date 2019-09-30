using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class ScreenFilterReadRepository : BaseReadRepository<ScreenFilter>, IScreenFilterReadRepository
    {
        private readonly DataContext _dataContext;

        public ScreenFilterReadRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override IMongoCollection<ScreenFilter> Collection => _dataContext.Database.GetCollection<ScreenFilter>(Consts.Collections.ScreenFilterCollectionName);
    }
}
