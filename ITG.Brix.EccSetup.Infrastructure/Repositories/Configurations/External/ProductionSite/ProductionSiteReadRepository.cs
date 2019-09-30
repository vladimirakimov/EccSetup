using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class ProductionSiteReadRepository : BaseReadRepository<ProductionSite>, IProductionSiteReadRepository
    {
        private readonly DataContext _dataContext;

        public ProductionSiteReadRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override IMongoCollection<ProductionSite> Collection =>
                                        _dataContext.Database.GetCollection<ProductionSite>(Consts.Collections.ProductionSiteCollectionName);
    }
}
