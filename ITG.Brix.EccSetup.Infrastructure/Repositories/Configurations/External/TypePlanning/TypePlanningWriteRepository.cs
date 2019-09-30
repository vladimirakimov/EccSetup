using System;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class TypePlanningWriteRepository : BaseWriteRepository<TypePlanning>, ITypePlanningWriteRepository
    {
        private readonly DataContext _dataContext;

        public TypePlanningWriteRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected override IMongoCollection<TypePlanning> Collection =>
                                    _dataContext.Database.GetCollection<TypePlanning>(Consts.Collections.TypePlanningCollectionName);
    }
}
