using AutoMapper;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Bases;

namespace ITG.Brix.EccSetup.Infrastructure.Converters
{
    public class StringToBlockTypeConverter : ITypeConverter<string, BlockType>
    {
        public BlockType Convert(string source, BlockType destination, ResolutionContext context)
        {
            var result = Enumeration.FromDisplayName<BlockType>(source);

            return result;
        }
    }
}
