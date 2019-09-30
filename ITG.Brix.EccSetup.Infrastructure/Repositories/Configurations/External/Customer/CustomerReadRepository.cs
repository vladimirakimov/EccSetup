using System;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class CustomerReadRepository : BaseReadRepository<Customer>, ICustomerReadRepository
    {
        private readonly DataContext _dataContext;

        public CustomerReadRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected override IMongoCollection<Customer> Collection => _dataContext.Database.GetCollection<Customer>(Consts.Collections.CustomerCollectionName);
    }
}
