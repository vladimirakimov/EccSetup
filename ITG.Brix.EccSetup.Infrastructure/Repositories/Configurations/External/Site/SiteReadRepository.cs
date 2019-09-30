using System;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class SiteReadRepository : BaseReadRepository<Site>, ISiteReadRepository
    {
        private DataContext _dataContext;

        public SiteReadRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected override IMongoCollection<Site> Collection => _dataContext.Database.GetCollection<Site>(Consts.Collections.SiteCollectionName);
    }
}
