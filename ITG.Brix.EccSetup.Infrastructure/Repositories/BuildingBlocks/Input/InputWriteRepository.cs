using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;
using System;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class InputWriteRepository : BaseWriteRepository<Input>, IInputWriteRepository
    {
        private readonly DataContext _dataContext;

        public InputWriteRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }
        protected override IMongoCollection<Input> Collection => _dataContext.Database.GetCollection<Input>(Consts.Collections.InputCollectionName);
    }
}
