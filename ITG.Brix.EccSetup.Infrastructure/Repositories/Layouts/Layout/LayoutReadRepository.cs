using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class LayoutReadRepository : BaseReadRepository<Layout>, ILayoutReadRepository
    {
        private readonly DataContext _dataContext;

        public LayoutReadRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override IMongoCollection<Layout> Collection => _dataContext.Database.GetCollection<Layout>(Consts.Collections.LayoutCollectionName);
    }
}
