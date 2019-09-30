using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;
using System;
using System.Linq.Expressions;

namespace ITG.Brix.EccSetup.Infrastructure.Providers
{
    public interface IOperatorActivityOdataProvider
    {
        Expression<Func<OperatorActivityClass, bool>> GetFilterPredicate(string filter);
    }
}
