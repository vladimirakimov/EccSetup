using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;
using System;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class RemarkWriteRepository : BaseWriteRepository<Remark>, IRemarkWriteRepository
    {
        private readonly DataContext _dataContext;

        public RemarkWriteRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected override IMongoCollection<Remark> Collection => _dataContext.Database.GetCollection<Remark>(Consts.Collections.RemarkCollectionName);
    }
}
