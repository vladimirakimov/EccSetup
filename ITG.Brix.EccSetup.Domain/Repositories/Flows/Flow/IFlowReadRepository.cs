using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Domain.Repositories
{
    public interface IFlowReadRepository : IBaseReadRepository<Flow>
    {
        Task<IEnumerable<Flow>> ListAsync(string filter, int? skip, int? limit);
    }
}
