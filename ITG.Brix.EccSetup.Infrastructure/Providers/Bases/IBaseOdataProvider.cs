using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Bases;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ITG.Brix.EccSetup.Infrastructure.Providers.Bases
{
    public interface IBaseOdataProvider<TEntity> where TEntity : Entity
    {
        string FilterTransform(string filter, IDictionary<string, string> replacements);
        Expression<Func<TEntity, bool>> GetFilterPredicate(string filter);        
    }
}
