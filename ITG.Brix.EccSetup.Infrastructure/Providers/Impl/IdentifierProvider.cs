using System;

namespace ITG.Brix.EccSetup.Infrastructure.Providers.Impl
{
    public class IdentifierProvider : IIdentifierProvider
    {
        public Guid Generate()
        {
            var result = Guid.NewGuid();

            return result;
        }
    }
}
