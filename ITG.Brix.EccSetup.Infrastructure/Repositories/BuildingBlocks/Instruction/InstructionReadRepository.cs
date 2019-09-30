using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using MongoDB.Driver;
using System;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class InstructionReadRepository : BaseReadRepository<Instruction>, IInstructionReadRepository
    {
        private readonly DataContext _dataContext;

        public InstructionReadRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }
        protected override IMongoCollection<Instruction> Collection => _dataContext.Database.GetCollection<Instruction>(Consts.Collections.InstructionCollectionName);
    }
}
