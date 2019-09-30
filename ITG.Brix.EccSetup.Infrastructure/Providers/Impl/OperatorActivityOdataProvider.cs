using System;
using System.Linq.Expressions;
using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using StringToExpression.LanguageDefinitions;

namespace ITG.Brix.EccSetup.Infrastructure.Providers.Impl
{
    public class OperatorActivityOdataProvider : IOperatorActivityOdataProvider
    {
        public Expression<Func<OperatorActivityClass, bool>> GetFilterPredicate(string filter)
        {
            Expression<Func<OperatorActivityClass, bool>> result = null;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var language = new ODataFilterLanguage();
                try
                {
                    result = language.Parse<OperatorActivityClass>(filter);
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
