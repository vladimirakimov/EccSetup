using ITG.Brix.EccSetup.Domain.Model.OperatorActivities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Domain.Repositories.OperatorActivities
{
    public interface IOperatorActivityReadRepository
    {
        Task<IEnumerable<OperatorActivity>> ListAsync(string filter, int? skip, int? limit);
    }
}
