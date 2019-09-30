using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class BusinessUnitWriteRepository : BaseWriteRepository<BusinessUnit>, IBusinessUnitWriteRepository
    {
        private readonly DataContext _dataContext;

        public BusinessUnitWriteRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override IMongoCollection<BusinessUnit> Collection => _dataContext.Database.GetCollection<BusinessUnit>(Consts.Collections.BusinessUnitCollectionName);
    }
}
