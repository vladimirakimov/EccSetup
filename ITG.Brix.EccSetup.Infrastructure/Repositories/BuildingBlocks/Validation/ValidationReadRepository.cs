using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;
using System;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class ValidationReadRepository : BaseReadRepository<Validation>, IValidationReadRepository
    {
        private readonly DataContext _dataContext;

        public ValidationReadRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected override IMongoCollection<Validation> Collection => _dataContext.Database.GetCollection<Validation>(Consts.Collections.ValidationCollectionName);
    }
}
