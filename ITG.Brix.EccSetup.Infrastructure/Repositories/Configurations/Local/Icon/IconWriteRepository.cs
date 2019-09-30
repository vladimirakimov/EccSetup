using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class IconWriteRepository : BaseWriteRepository<Icon>, IIconWriteRepository
    {
        private readonly DataContext _dataContext;

        public IconWriteRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override IMongoCollection<Icon> Collection => _dataContext.Database.GetCollection<Icon>(Consts.Collections.IconCollectionName);
    }
}
