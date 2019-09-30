using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class WorkOrderButtonReadRepository : BaseReadRepository<WorkOrderButton>, IWorkOrderButtonReadRepository
    {
        private readonly DataContext _dataContext;

        public WorkOrderButtonReadRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override IMongoCollection<WorkOrderButton> Collection => _dataContext.Database.GetCollection<WorkOrderButton>(Consts.Collections.WorkOrderCollectionName);
    }
}
