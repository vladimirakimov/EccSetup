using ITG.Brix.EccSetup.Domain.Bases;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Domain.Repositories.Bases
{
    public interface IBaseWriteRepository<TEntity> where TEntity : Entity
    {
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id, int version);
    }
}
