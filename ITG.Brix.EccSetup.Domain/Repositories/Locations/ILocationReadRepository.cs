using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Domain.Repositories
{
    public interface ILocationReadRepository 
    {
        Task<IEnumerable<Location>> ListAsync(string filter, int? skip, int? limit);
    }
}
