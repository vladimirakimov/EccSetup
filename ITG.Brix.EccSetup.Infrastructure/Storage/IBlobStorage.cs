using ITG.Brix.EccSetup.Infrastructure.Storage.Dtos;
using System.IO;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Infrastructure.Storage
{
    public interface IBlobStorage
    {
        Task<string> UploadFileToStorage(Stream fileStream, string fileName);
        Task<FileDownloadDto> DownloadFileAsync(string fileName);
    }
}
