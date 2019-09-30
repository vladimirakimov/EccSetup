using System;

namespace ITG.Brix.EccSetup.Infrastructure.Providers.Impl
{
    public class FileNameProvider : IFileNameProvider
    {
        public string Generate(string extension)
        {
            var name = $"{Guid.NewGuid().ToString()}{extension}";
            return name;
        }
    }
}
