using ITG.Brix.EccSetup.Domain.Bases;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Domain.Repositories
{
    public interface IBaseReadRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> filter, int? skip, int? limit);
        Task<TEntity> GetAsync(Guid id);
    }
}
