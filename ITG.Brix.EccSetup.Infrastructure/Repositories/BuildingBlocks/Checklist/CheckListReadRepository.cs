using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;
using System;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class ChecklistReadRepository : BaseReadRepository<Checklist>, IChecklistReadRepository
    {
        private readonly DataContext _dataContext;
        public ChecklistReadRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }
        protected override IMongoCollection<Checklist> Collection => _dataContext.Database.GetCollection<Checklist>(Consts.Collections.ChecklistCollectionName);
    }
}
