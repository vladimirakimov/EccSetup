using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class LayoutWriteRepository : BaseWriteRepository<Layout>, ILayoutWriteRepository
    {
        private readonly DataContext _dataContext;
        public LayoutWriteRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override IMongoCollection<Layout> Collection => _dataContext.Database.GetCollection<Layout>(Consts.Collections.LayoutCollectionName);
    }
}
