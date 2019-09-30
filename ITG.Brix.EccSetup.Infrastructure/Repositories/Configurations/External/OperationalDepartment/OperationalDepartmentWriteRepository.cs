using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;
using System;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class OperationalDepartmentWriteRepository : BaseWriteRepository<OperationalDepartment>, IOperationalDepartmentWriteRepository
    {
        private readonly DataContext _dataContext;

        public OperationalDepartmentWriteRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected override IMongoCollection<OperationalDepartment> Collection => _dataContext.Database.GetCollection<OperationalDepartment>(Consts.Collections.OperationalDepartmentCollectionName);
    }
}
