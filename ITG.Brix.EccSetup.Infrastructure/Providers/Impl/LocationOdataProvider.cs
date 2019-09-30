using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using StringToExpression.LanguageDefinitions;
using System;
using System.Linq.Expressions;

namespace ITG.Brix.EccSetup.Infrastructure.Providers.Impl
{
    public class LocationOdataProvider : ILocationOdataProvider
    {
        public Expression<Func<LocationClass, bool>> GetFilterPredicate(string filter)
        {
            Expression<Func<LocationClass, bool>> result = null;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var language = new ODataFilterLanguage();
                try
                {
                    result = language.Parse<LocationClass>(filter);
                }
                catch (Exception exception)
                {
                    throw new FilterODataException(exception);
                }
            }

            return result;
        }
    }
}
