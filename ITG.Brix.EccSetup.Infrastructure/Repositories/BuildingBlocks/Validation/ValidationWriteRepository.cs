using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class ValidationWriteRepository : BaseWriteRepository<Validation>, IValidationWriteRepository
    {
        private readonly DataContext _dataContext;

        public ValidationWriteRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override IMongoCollection<Validation> Collection => _dataContext.Database.GetCollection<Validation>(Consts.Collections.ValidationCollectionName);
    }
}
