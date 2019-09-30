using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories.Customers;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;
using System;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class CustomerWriteRepository : BaseWriteRepository<Customer>, ICustomerWriteRepository
    {
        private readonly DataContext _dataContext;

        public CustomerWriteRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected override IMongoCollection<Customer> Collection => _dataContext.Database.GetCollection<Customer>(Consts.Collections.CustomerCollectionName);
    }
}
