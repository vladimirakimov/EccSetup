using System;

namespace ITG.Brix.EccSetup.Infrastructure.Providers
{
    public interface IIdentifierProvider
    {
        Guid Generate();
    }
}
