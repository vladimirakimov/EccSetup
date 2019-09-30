using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;
using System;
using System.Linq.Expressions;

namespace ITG.Brix.EccSetup.Infrastructure.Providers.Impl
{
    public interface ILocationOdataProvider 
    {
        Expression<Func<LocationClass, bool>> GetFilterPredicate(string filter);
    }
}
