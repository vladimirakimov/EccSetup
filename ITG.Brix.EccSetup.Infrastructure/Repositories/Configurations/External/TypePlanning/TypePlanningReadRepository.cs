using System;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class TypePlanningReadRepository : BaseReadRepository<TypePlanning>, ITypePlanningReadRepository
    {
        private readonly DataContext _dataContext;

        public TypePlanningReadRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected override IMongoCollection<TypePlanning> Collection =>
                                    _dataContext.Database.GetCollection<TypePlanning>(Consts.Collections.TypePlanningCollectionName);
    }
}
