using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class ScreenElementWriteRepository : BaseWriteRepository<ScreenElement>, IScreenElementWriteRepository
    {
        private readonly DataContext _dataContext;

        public ScreenElementWriteRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override IMongoCollection<ScreenElement> Collection => _dataContext.Database.GetCollection<ScreenElement>(Consts.Collections.ScreenElementCollectionName);
    }
}
