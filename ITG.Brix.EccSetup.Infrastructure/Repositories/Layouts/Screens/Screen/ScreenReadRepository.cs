using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class ScreenReadRepository : BaseReadRepository<Screen>, IScreenReadRepository
    {
        private readonly DataContext _dataContext;

        public ScreenReadRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override IMongoCollection<Screen> Collection => _dataContext.Database.GetCollection<Screen>(Consts.Collections.ScreenCollectionName);
    }
}
