using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.Providers.Bases;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Infrastructure.Providers
{
    public interface IFlowOdataProvider : IBaseOdataProvider<Flow>
    {
        FilterDefinition<Flow> GetFilterDefinition(string filter);
    }
}
